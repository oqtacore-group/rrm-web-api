using DotNetEnv;
using System.Text;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;
using Oqtacore.Rrm.Api.Helpers;
using Oqtacore.Rrm.Api.Middleware;
using Oqtacore.Rrm.Api.Models;
using Oqtacore.Rrm.Application.Commands;
using Oqtacore.Rrm.Application.Commands.Clients;
using Oqtacore.Rrm.Application.ServiceAgent;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Domain.Repository;
using Oqtacore.Rrm.Infrastructure.Data;
using Oqtacore.Rrm.Infrastructure.Repository;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

try
{
    logger.Debug("init main");

    // Load .env file
    Env.Load();

    var builder = WebApplication.CreateBuilder(args);

    var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

    var appConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    var logConnectionString = Environment.GetEnvironmentVariable("DB_LOG_CONNECTION_STRING");

    if (string.IsNullOrEmpty(appConnectionString) || string.IsNullOrEmpty(logConnectionString))
        throw new Exception("DB_CONNECTION_STRING or DB_LOG_CONNECTION_STRING is missing in .env");

    // Configure EF Core and Identity
    builder.Services.AddDbContext<ApplicationContext>(options =>
        options.UseSqlServer(appConnectionString));

    builder.Services.AddIdentity<AspNetUser, AspNetRole>()
        .AddEntityFrameworkStores<ApplicationContext>()
        .AddDefaultTokenProviders();

    // JWT config
    var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? throw new Exception("JWT_SECRET_KEY not found in .env");
    var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "rrm-app";

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

    // NLog config
    var config = new LoggingConfiguration();

    var dbTarget = new DatabaseTarget("database")
    {
        ConnectionString = logConnectionString,
        CommandText = @"
            INSERT INTO Log (Application, Logged, LogonUserEmail, logger, Level, Message, Exception)
            VALUES ('RRM', @time_stamp, @logonUserEmail, @logger, @level, @message, @exception)"
    };

    dbTarget.Parameters.Add(new DatabaseParameterInfo("@time_stamp", "${longdate}"));
    dbTarget.Parameters.Add(new DatabaseParameterInfo("@logonUserEmail", "${mdlc:logonUserEmail}"));
    dbTarget.Parameters.Add(new DatabaseParameterInfo("@logger", "${logger}"));
    dbTarget.Parameters.Add(new DatabaseParameterInfo("@level", "${level}"));
    dbTarget.Parameters.Add(new DatabaseParameterInfo("@message", "${message}"));
    dbTarget.Parameters.Add(new DatabaseParameterInfo("@exception", "${exception:format=toString}"));

    config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, dbTarget);
    LogManager.Configuration = config;

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Platrum client config
    var platrumApiUrl = Environment.GetEnvironmentVariable("PLATRUM_API_URL");
    var platrumApiKey = Environment.GetEnvironmentVariable("PLATRUM_API_KEY");

    if (string.IsNullOrWhiteSpace(platrumApiUrl))
        throw new Exception("PLATRUM_API_URL is not set in .env");

    builder.Services.AddHttpClient<PlatrumApiClient>(client =>
    {
        client.BaseAddress = new Uri(platrumApiUrl);
        if (!string.IsNullOrWhiteSpace(platrumApiKey))
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {platrumApiKey}");
        }
    });

    // AWS config (optional)
    var awsAccessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY");
    var awsSecretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY");
    var awsRegion = Environment.GetEnvironmentVariable("AWS_REGION");

    if (!string.IsNullOrEmpty(awsAccessKey) && !string.IsNullOrEmpty(awsSecretKey) && !string.IsNullOrEmpty(awsRegion))
    {
        builder.Services.AddAWSService<IAmazonS3>();
        builder.Services.AddDefaultAWSOptions(new AWSOptions
        {
            Credentials = new BasicAWSCredentials(awsAccessKey, awsSecretKey),
            Region = RegionEndpoint.GetBySystemName(awsRegion)
        });
    }

    // Services and middleware
    builder.Services.AddControllers().AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssemblies(typeof(AddClientCommandHandler).Assembly, typeof(Program).Assembly));

    builder.Services.AddScoped<IRepository, GenericRepository>();
    builder.Services.AddScoped<IClientRepository, ClientRepository>();
    builder.Services.AddScoped<IAdminRepository, AdminRepository>();
    builder.Services.AddScoped<IVacancyRepository, VacancyRepository>();
    builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();

    var app = builder.Build();

    // Always enable Swagger
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}
