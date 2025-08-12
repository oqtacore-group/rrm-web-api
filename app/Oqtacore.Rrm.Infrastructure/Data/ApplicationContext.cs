using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Infrastructure.Data.Configurations;

namespace Oqtacore.Rrm.Infrastructure.Data
{
    public class ApplicationContext : IdentityDbContext<AspNetUser,AspNetRole,string, IdentityUserClaim<string>, IdentityUserRole<string>, 
        IdentityUserLogin<string>,IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

        public virtual DbSet<IdentityUserClaim<string>> IdentityUserClaim { get; set; }
        public virtual DbSet<IdentityUserLogin<string>> IdentityUserLogin { get; set; }
        public virtual DbSet<IdentityUserRole<string>> IdentityUserRole { get; set; }
        public virtual DbSet<IdentityRoleClaim<string>> IdentityRoleClaim { get; set; }
        public virtual DbSet<IdentityUserToken<string>> IdentityUserToken { get; set; }

        public virtual DbSet<CandidateRelocationCity> CandidateRelocationCity { get; set; }
        public virtual DbSet<CandidatesVacancyStatu> CandidatesVacancyStatus { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientContact> ClientContact { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<LinkedinAdmin> LinkedinAdmins { get; set; }
        public virtual DbSet<LinkedinConnectionAction> LinkedinConnectionAction { get; set; }
        public virtual DbSet<LinkedinConnectionActionType> LinkedinConnectionActionType { get; set; }
        public virtual DbSet<LinkedinMessage> LinkedinMessage { get; set; }
        public virtual DbSet<LinkedinUser> LinkedinUser { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<RegisteredChat> RegisteredChat { get; set; }
        public virtual DbSet<ResumeExperience> ResumeExperience { get; set; }
        public virtual DbSet<VacancyStatusType> VacancyStatusType { get; set; }
        public virtual DbSet<VacancyTag> VacancyTag { get; set; }
        public virtual DbSet<ContactData> ContactData { get; set; }
        public virtual DbSet<VacancyStatusListViewModel> VacancyStatusListViewModel { get; set; }
        public virtual DbSet<CurrentVacancyStatusListViewModel> CurrentVacancyStatusListViewModel { get; set; }
        public virtual DbSet<VacancyStatusListingViewModel> VacancyStatusListingViewModel { get; set; }
        public virtual DbSet<AllVacancyStatusListingViewModel> AllVacancyStatusListingViewModel { get; set; }
        public virtual DbSet<VacancyStateListViewModel> VacancyStateListViewModel { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<CandidateArchive> CandidateArchive { get; set; }
        public virtual DbSet<VacancyArchive> VacancyArchive { get; set; }
        public virtual DbSet<HeadHunterData> HeadHunterData { get; set; }
        public virtual DbSet<HeadHunterJobExperienceData> HeadHunterJobExperienceData { get; set; }
        public virtual DbSet<ParsedLinkedinCandidateData> ParsedLinkedinCandidateData { get; set; }
        public virtual DbSet<ParsedLinkedinCandidateEducation> ParsedLinkedinCandidateEducation { get; set; }
        public virtual DbSet<ParsedLinkedinCandidateResume> ParsedLinkedinCandidateResume { get; set; }
        public virtual DbSet<Candidate> Candidate { get; set; }
        public virtual DbSet<Vacancy> Vacancy { get; set; }
        public virtual DbSet<VacancyInfo> VacancyInfo { get; set; }
        public virtual DbSet<RecruiterStatisticViewModel> RecruiterStatisticViewModel { get; set; }
        public virtual DbSet<CandidateEventType> CandidateEventType { get; set; }
        public virtual DbSet<CandidatesVacancyComment> CandidatesVacancyComment { get; set; }
        public virtual DbSet<GoogleCalendarEvent> GoogleCalendarEvent { get; set; }
        public virtual DbSet<GoogleCalendarSetting> GoogleCalendarSetting { get; set; }
        public virtual DbSet<AllCandidateEventViewModel> AllCandidateEventViewModel { get; set; }
        public virtual DbSet<CandidateFile> CandidateFile { get; set; }
        public virtual DbSet<ClientArchive> ClientArchive { get; set; }
        public virtual DbSet<SiteSetting> SiteSetting { get; set; }
        public virtual DbSet<CandidateEvent> CandidateEvent { get; set; }
        public virtual DbSet<MeetupMember> MeetupMember { get; set; }
        public virtual DbSet<Amocrm_Linkedin_UserListViewModel> Amocrm_Linkedin_UserListViewModel { get; set; }
        public virtual DbSet<AmocrmFilter> AmocrmFilter { get; set; }
        public virtual DbSet<AmocrmFilterUserConnection> AmocrmFilterUserConnection { get; set; }

        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserClaim<string>>(eb => {
                eb.HasKey(e => e.Id);
                eb.HasNoDiscriminator();
                eb.ToTable("AspNetUserClaims");
            });
          
            modelBuilder.Entity<IdentityUserLogin<string>>(eb => {
                eb.HasKey(e => new { e.UserId, e.LoginProvider });
                eb.HasNoDiscriminator();
                eb.ToTable("AspNetUserLogins");
            });
            modelBuilder.Entity<IdentityUserRole<string>>(eb => {
                eb.HasKey(e =>new { e.UserId,e.RoleId });
                eb.HasNoDiscriminator();
                eb.ToTable("AspNetUserRoles");
            });
            modelBuilder.Entity<IdentityRoleClaim<string>>(eb => {
                eb.HasKey(e => new { e.Id, e.RoleId });
                eb.HasNoDiscriminator();
                eb.ToTable("AspNetRoleClaim");
            });
            modelBuilder.Entity<IdentityUserToken<string>>(eb => {
                eb.HasKey(e => new { e.UserId, e.LoginProvider });
                eb.HasNoDiscriminator();
                eb.ToTable("AspNetUserToken");
            });



            modelBuilder.Entity<VacancyStatusListViewModel>(eb =>
            {
                eb.HasNoKey();
                eb.ToView("VacancyStatusListViewModel");
            });
            modelBuilder.Entity<CurrentVacancyStatusListViewModel>(eb =>
            {
                eb.HasNoKey();
                eb.ToView("CurrentVacancyStatusListViewModel");
            });
            modelBuilder.Entity<VacancyStatusListingViewModel>(eb =>
            {
                eb.HasNoKey();
                eb.ToView("VacancyStatusListingViewModel");
            });
            modelBuilder.Entity<AllVacancyStatusListingViewModel>(eb =>
            {
                eb.HasNoKey();
                eb.ToView("AllVacancyStatusListingViewModel");
            });
            modelBuilder.Entity<VacancyStateListViewModel>(eb =>
            {
                eb.HasNoKey();
                eb.ToView("VacancyStateListViewModel");
            });
            modelBuilder.Entity<RecruiterStatisticViewModel>(eb =>
            {
                eb.HasNoKey();
                eb.ToView("RecruiterStatisticViewModel");
            });
            modelBuilder.Entity<AllCandidateEventViewModel>(eb =>
            {
                eb.HasNoKey();
                eb.ToView("AllCandidateEventViewModel");
            });
            modelBuilder.Entity<Amocrm_Linkedin_UserListViewModel>(eb =>
            {
                eb.HasNoKey();
                eb.ToView("Amocrm_Linkedin_UserListViewModel");
            });

            modelBuilder.Entity<GoogleCalendarSetting>().HasKey(ba => new { ba.Name });
            modelBuilder.Entity<SiteSetting>().HasKey(ba => new { ba.Name });

            //Creating Unique keys
            modelBuilder.Entity<GoogleCalendarSetting>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });
            modelBuilder.Entity<SiteSetting>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasIndex(e => e.CurrencyName).IsUnique();
            });
            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.ToTable("AspNetUsers");
                entity.HasIndex(e => e.Id).IsUnique();
                entity.HasNoDiscriminator();
            });

            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new VacancyConfiguration());
            modelBuilder.ApplyConfiguration(new VacancyInfoConfiguration());
            modelBuilder.ApplyConfiguration(new CandidateConfiguration());
            modelBuilder.ApplyConfiguration(new ErrorLogConfiguration());
        }
    }
}
