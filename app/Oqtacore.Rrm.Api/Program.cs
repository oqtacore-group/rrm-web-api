using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;
using Oqtacore.Rrm.Api.Health;
using Oqtacore.Rrm.Api.Helpers;
using Oqtacore.Rrm.Api.Middleware;
using Oqtacore.Rrm.Api.Models;
using Oqtacore.Rrm.Api.ServiceWorkers;
using Oqtacore.Rrm.Application.Commands;
using Oqtacore.Rrm.Application.Commands.Clients;
using Oqtacore.Rrm.Application.Configs;
using Oqtacore.Rrm.Application.ServiceAgent;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Domain.Repository;
using Oqtacore.Rrm.Infrastructure.Data;
using Oqtacore.Rrm.Infrastructure.Repository;
using System.Text;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
try
{
    logger.Debug("init main");
    var builder = WebApplication.CreateBuilder(args);

    var env = EnvironmentManager.GetEnvironmentName(builder.Environment);
    var appName = "rrm";
    var appConnectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
    var logConnectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
    var telegramBotToken = builder.Configuration["Telegram:id"];
    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
    builder.Services.Configure<PlatrumMetricsSettings>(builder.Configuration.GetSection("PlatrumMetricsSettings"));

    if (env != "hes" && env != "prod")
    {
        // AWS Secrets Manager
        var secretManagerHelper = new SecretsManagerHelper();
        var secretName = $"{env}/{appName}/rds/connection-strings__SqlServerConnection";
        var secret = await secretManagerHelper.GetSecretValueAsync(secretName);

        (appConnectionString, logConnectionString) = GetConnectionString(env, secret);
        var telegramBotSecret = await secretManagerHelper.GetSecretValueAsync($"{env}/{appName}/telegram-bot/token");
        telegramBotToken = JObject.Parse(telegramBotSecret)["telegram_bot_token"].ToString();
    }

    // Configure EF Core and Identity
    builder.Services.AddDbContext<ApplicationContext>(options =>
        options.UseSqlServer(appConnectionString));

    builder.Services.AddIdentity<AspNetUser, AspNetRole>()
        .AddEntityFrameworkStores<ApplicationContext>()
        .AddDefaultTokenProviders();

    // Register MediatR
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AddClientCommandHandler).Assembly, typeof(Program).Assembly));

    // Configure NLog programmatically
    var config = new LoggingConfiguration();
    var databaseTarget = new DatabaseTarget("database")
    {
        ConnectionString = logConnectionString,
        CommandText = @"
            INSERT INTO Log (Application, Logged, LogonUserEmail, logger, Level, Message, Exception)
			VALUES ('RRM', @time_stamp, @logonUserEmail, @logger, @level, @message, @exception)"
    };

    databaseTarget.Parameters.Add(new DatabaseParameterInfo("@time_stamp", "${longdate}"));
    databaseTarget.Parameters.Add(new DatabaseParameterInfo("@logonUserEmail", "${mdlc:logonUserEmail}"));
    databaseTarget.Parameters.Add(new DatabaseParameterInfo("@logger", "${logger}"));
    databaseTarget.Parameters.Add(new DatabaseParameterInfo("@level", "${level}"));
    databaseTarget.Parameters.Add(new DatabaseParameterInfo("@message", "${message}"));
    databaseTarget.Parameters.Add(new DatabaseParameterInfo("@exception", "${exception:format=toString}"));

    config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, databaseTarget);
    LogManager.Configuration = config;
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Configure CORS to allow all origins (for development/testing purposes)
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins", policy =>
        {
            policy.WithOrigins("https://rrm.oqtacore.com", "https://staging-rrm.oqtacore.com", "https://www.staging-rrm.oqtacore.com", "http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });

    // Configure health checks
    builder.Services.AddHealthChecks()
        .AddCheck<AthenaHealthCheck>("Athena");

    // Add services to the container.
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            // Use PascalCase for JSON property names
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });

    builder.Services.AddAWSService<IAmazonS3>();
    builder.Services.AddDefaultAWSOptions(new AWSOptions
        {
            Credentials = new BasicAWSCredentials(
            builder.Configuration["AWS:AccessKey"],
            builder.Configuration["AWS:SecretKey"]
        ),
        Region = RegionEndpoint.GetBySystemName(builder.Configuration["AWS:Region"])
    });
    //builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());

    // Configure JWT Authentication
    var jwtKey = builder.Configuration["Jwt:Key"];
    var jwtIssuer = builder.Configuration["Jwt:Issuer"];

    // Add JWT authentication
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
        };
    });

    builder.Services.AddAuthentication();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<IRepository, GenericRepository>();
    builder.Services.AddScoped<IClientRepository, ClientRepository>();
    builder.Services.AddScoped<IAdminRepository, AdminRepository>();
    builder.Services.AddScoped<IVacancyRepository, VacancyRepository>();
    builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();

    if (!builder.Configuration.GetSection("Environment").Exists() || builder.Configuration.GetSection("Environment")["Services"] == "true")
    {
        builder.Services.AddHostedService<TelegramReminderService>();
    }
    var platrumSettings = builder.Configuration.GetSection("PlatrumMetricsSettings").Get<PlatrumMetricsSettings>();
    builder.Services.AddHttpClient<PlatrumApiClient>(options =>
    {
        options.BaseAddress = new Uri(platrumSettings.PlatrumApiUrl);
    });
    
    builder.Services.AddHostedService<MetricsService>();

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<IHttpService, HttpService>();

    var app = builder.Build();

    // Start the Telegram bot
    try
    {
        var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
        var s3client = app.Services.GetRequiredService<IAmazonS3>();
        if (!string.IsNullOrEmpty(telegramBotToken))
        {
            TelegramBotReminder.telegramBotClient.Start(telegramBotToken, scopeFactory, s3client, builder.Configuration, builder.Environment);
        }
    }
    catch (Exception ex)
    {
        using (var scope = app.Services.CreateScope())
        {
            ApplicationContext thql = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            thql.Logs.Add(new Log()
            {
                Date = DateTime.UtcNow,
                Text = "TelegramBot start: \nError:" + Convert.ToString(ex)
            });
            thql.SaveChanges();
        }
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        //app.UseSwagger();
        //app.UseSwaggerUI();
    }

    app.UseSwagger();
    app.UseSwaggerUI();

    // Use the CORS policy in the app
    app.UseCors("AllowAllOrigins");

    app.MapHealthChecks("/_health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    // Enforce HTTPS
    //app.UseHttpsRedirection();

    // Enable authentication and authorization middleware
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.UseMiddleware<ExceptionMiddleware>();
    app.UseMiddleware<UserLoggingMiddleware>();

    app.Run();
}
catch (Exception ex)
{
    // NLog: catch setup errors
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}

(string appConnection, string logConnection) GetConnectionString(string env, string secretString)
{
    var secret = JObject.Parse(secretString);

    var username = secret["username"].ToString();
    var password = secret["password"].ToString();
    var host = secret["host"].ToString();   
    var port = secret["port"].ToString();

    var appDatabaseName = env == "staging" ? "BrainHunter" : "RRM";
    var logDatabaseName = env == "staging" ? "BrainHunter" : "RRM";

    var appConnection = $"Server={host},{port};Database={appDatabaseName};User Id={username};Password={password};MultipleActiveResultSets=true;TrustServerCertificate=True;";
    var logConnection = $"Server={host},{port};Database={logDatabaseName};User Id={username};Password={password};MultipleActiveResultSets=true;TrustServerCertificate=True;";
    
    return (appConnection, logConnection);
}