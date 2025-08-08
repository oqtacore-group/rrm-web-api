using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Oqtacore.Rrm.Domain.Models
{


    public class AspNetUser : IdentityUser
    {
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
    }

    public partial class AspNetRole : IdentityRole
    {
    }


    public partial class AspNetUserClaims<TKey> : IdentityUserClaim<string>
    {
        public override int Id { get; set; }
        public override string UserId { get; set; }
        public override string ClaimType { get; set; }
        public override string ClaimValue { get; set; }
    }


    public partial class AspNetUserLogins<TKey> : IdentityUserLogin<string>
    {
        public override string LoginProvider { get; set; }
        public override string ProviderKey { get; set; }
        public override string ProviderDisplayName { get; set; }
        public override string UserId { get; set; }
    }



    public partial class AspNetUserRoles<TKey> : IdentityUserRole<string>
    {
        public override string UserId { get; set; }
        public override string RoleId { get; set; }
    }



    public partial class AspNetRoleClaim<TKey> : IdentityRoleClaim<string>
    {

        public override int Id { get; set; }

        public override string RoleId { get; set; }

        public override string ClaimType { get; set; }

        public override string ClaimValue { get; set; }

    }



    public partial class AspNetUserToken<TKey> : IdentityUserToken<string>
    {


        public override string UserId { get; set; }

        public override string LoginProvider { get; set; }

        public override string Name { get; set; }

        public override string Value { get; set; }
    }






    public partial class Admin
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AdminState { get; set; }
        public string AuthId { get; set; }
        public string Email { get; set; }
        public bool ServiceAccount { get; set; }

    }

    public partial class AllCandidateEventViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public DateTime? Date { get; set; }
        public int? CandidateId { get; set; }
        public string? Caption { get; set; }
        public int TypeId { get; set; }
        public string? Location { get; set; }
        public string? EventType { get; set; }
        public bool? Completed { get; set; }
        public string? ZoomLink { get; set; }
        public string? CandidateName { get; set; }
    }


    public partial class AllVacancyStatusListingViewModel
    {
        public int StatusId { get; set; }
        public string Name { get; set; }
        public Nullable<bool> CountSuccess { get; set; }
        public int OrderId { get; set; }
        public int CandidateCount { get; set; }
    }

    public partial class Amocrm_Linkedin_UserListViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public System.DateTime Date { get; set; }
        public string LinkedinProfile { get; set; }
        public string AdminName { get; set; }
        public bool Amocrm_added { get; set; }
        public int Amocrm_Id { get; set; }
        public int AdminId { get; set; }
    }

    public partial class AmocrmFilter
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string AmocrmName { get; set; }
        public string AmocrmTagId { get; set; }
        public string AmocrmUrlTag { get; set; }
        public string AmocrmUrlTagId { get; set; }
        public string FilterPhrase { get; set; }
        public string PipelineId { get; set; }
    }

    public partial class AmocrmFilterUserConnection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public int AmocrmId { get; set; }
        public int FilterId { get; set; }
        public System.DateTime Date { get; set; }

        public virtual AmocrmFilter AmocrmFilter { get; set; }
    }

    public partial class CandidateEvent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public DateTime Date { get; set; }
        public int CandidateId { get; set; }
        public string? Caption { get; set; }
        public int TypeId { get; set; }
        public bool? Completed { get; set; }
        public int CreatedBy { get; set; }
        public string? ZoomLink { get; set; }
        public bool? ReminderSent { get; set; }
        public bool? ReminderEarlySent { get; set; }
        public string? HashCode { get; set; }

    }

    public partial class CandidateEventType
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }

    public partial class CandidateFile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public int candidateId { get; set; }
        public string fileName { get; set; }
        public string fileUrl { get; set; }
        public System.DateTime DateAdded { get; set; }
    }

    public partial class CandidateRelocationCity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public int CandidateId { get; set; }
        public string CityName { get; set; }


    }

    public partial class CandidatesVacancyComment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public int CandidateId { get; set; }
        public int VacancyId { get; set; }
        public System.DateTime DateAdded { get; set; }
        public string Note { get; set; }


    }

    public partial class CandidatesVacancyStatu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public int CandidateId { get; set; }
        public int VacancyId { get; set; }
        public int VacancyStatusId { get; set; }
        public DateTime DateAdded { get; set; }
        public string? Note { get; set; }
        public int? CreatedBy { get; set; }

    }

    public partial class ClientContact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public int ClientId { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int ContactDataId { get; set; }
        public Nullable<int> CreatedBy { get; set; }

    }

    public partial class ContactData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]

        public int id { get; set; }
        public string? Linkedin { get; set; }
        public string? Vkontakte { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SecondPhoneNumber { get; set; }
        public string? Telegram { get; set; }
        public string? Skype { get; set; }
        public string? Email { get; set; }
        public string? Location { get; set; }
        public int? CreatedBy { get; set; }

    }

    public partial class Currency
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public string CurrencyName { get; set; }
    }

    public partial class CurrentVacancyStatusListViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public int CandidateId { get; set; }
        public int VacancyId { get; set; }
        public int VacancyStatusId { get; set; }
        public string StatusName { get; set; }
        public System.DateTime DateAdded { get; set; }
        public string Note { get; set; }
        public string VacancyName { get; set; }
        public string ClientName { get; set; }
        public int ClientId { get; set; }
        public bool CountSuccess { get; set; }
        public string CandidateName { get; set; }
    }


    public partial class GoogleCalendarEvent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public string status { get; set; }
        public string kind { get; set; }
        public Nullable<System.DateTime> starttime { get; set; }
        public Nullable<System.DateTime> endtime { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public Nullable<System.DateTime> updated { get; set; }
        public string location { get; set; }
        public string UIID { get; set; }
        public string summary { get; set; }
        public string user_email { get; set; }
        public string event_id { get; set; }
        public Nullable<bool> completed { get; set; }
    }

    public partial class GoogleCalendarSetting
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public partial class HeadHunterData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public int CandidateId { get; set; }
        public string Location { get; set; }
        public string Skills { get; set; }
        public string About { get; set; }
        public string Employment { get; set; }
        public string Relocation { get; set; }
        public string Position { get; set; }
        public string totalExperience { get; set; }
        public int CreatedBy { get; set; }
        public string ProfileUrl { get; set; }

    }


    public partial class HeadHunterJobExperienceData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public int HhId { get; set; }
        public string? startData { get; set; }
        public string? endData { get; set; }
        public string? companyName { get; set; }
        public string? position { get; set; }
        public string? description { get; set; }
        public string? companyAreaTitle { get; set; }
        public string? startDate { get; set; }
        public string? endDate { get; set; }
    }

    public partial class LinkedinAdmin
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AdminKey { get; set; }
        public string ProfileUrl { get; set; }
        public System.DateTime LastUpdate { get; set; }
    }

    public partial class LinkedinConnectionAction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public System.DateTime Date { get; set; }
        public int ActionId { get; set; }
        public string ProfileUrl { get; set; }
        public int AdminId { get; set; }
        public string OccupationText { get; set; }
        public string MessageText { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }

    }

    public partial class LinkedinConnectionActionType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public partial class LinkedinMessage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public int UserId { get; set; }
        public System.DateTime Date { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public string ProfileUrl { get; set; }
        public int OrderId { get; set; }
        public Nullable<bool> Amocrm_added { get; set; }
    }

    public partial class LinkedinUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public string DialogUrl { get; set; }
        public string ProfileUrl { get; set; }
        public string Name { get; set; }
        public System.DateTime Date { get; set; }
        public bool Connected { get; set; }
        public Nullable<System.DateTime> DateDisconnect { get; set; }
        public int AdminId { get; set; }
        public Nullable<bool> Amocrm_added { get; set; }
        public Nullable<int> Amocrm_Id { get; set; }
    }

    public partial class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public System.DateTime Date { get; set; }
        public string Text { get; set; }
    }

    public partial class MeetupMember
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public string member_id { get; set; }
        public System.DateTime Date { get; set; }
        public string Name { get; set; }
    }

    public partial class ParsedLinkedinCandidateData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public int CandidateId { get; set; }
        public string? DateBirth { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Position { get; set; }
        public string? About { get; set; }
        public string? LocationName { get; set; }
        public string? LanguageSummary { get; set; }
        public string? SkillSummary { get; set; }
        public string? IndustrySummary { get; set; }
        public DateTime DateParsed { get; set; }
        public bool Active { get; set; }
        public string? Profile_url { get; set; }
    }

    public partial class ParsedLinkedinCandidateEducation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public int ParsedId { get; set; }
        public int? YearStart { get; set; }
        public int? YearEnd { get; set; }
        public string? schoolName { get; set; }
        public string? fieldOfStudy { get; set; }
        public string? degreeName { get; set; }
        public string? grade { get; set; }
    }

    public partial class ParsedLinkedinCandidateResume
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public int ParsedId { get; set; }
        public string? Title { get; set; }
        public string? CompanyName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? LocationName { get; set; }
    }

    public partial class RecruiterStatisticViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public int CandidateId { get; set; }
        public int VacancyId { get; set; }
        public int VacancyStatusId { get; set; }
        public string StatusName { get; set; }
        public System.DateTime DateAdded { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<bool> Success { get; set; }
        public Nullable<bool> Fail { get; set; }
        public string AdminName { get; set; }
    }

    public partial class RegisteredChat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public string ChatId { get; set; }
        public string Name { get; set; }
        public bool Approved { get; set; }
        public DateTime DateTime { get; set; }
        public string? Description { get; set; }
        public string TelegramBotName { get; set; }
        public long? TelegramBotId { get; set; }
    }

    public partial class ResumeExperience
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public int CandidateId { get; set; }
        public string PositionName { get; set; }
        public string CompanyName { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public bool CurrentJob { get; set; }
        public string Description { get; set; }
    }

    public partial class SiteSetting
    {

        public string Name { get; set; }
        public string Value { get; set; }
    }

    public partial class VacancyStateListViewModel
    {
        public int VacancyId { get; set; }
        public string Name { get; set; }
        public string ClientName { get; set; }
        public Nullable<int> SuccessCount { get; set; }
        public Nullable<int> WorkplaceNumber { get; set; }
        public Nullable<int> CandidateCount { get; set; }
        public Nullable<int> ClientId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    }

    public partial class VacancyStatusListingViewModel
    {
        public int VacancyId { get; set; }
        public int StatusId { get; set; }
        public string Name { get; set; }
        public Nullable<bool> CountSuccess { get; set; }
        public int OrderId { get; set; }
        public int CandidateCount { get; set; }
    }

    public partial class VacancyStatusListViewModel
    {
        public int id { get; set; }
        public int CandidateId { get; set; }
        public int VacancyId { get; set; }
        public int VacancyStatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime DateAdded { get; set; }
        public string? Note { get; set; }
        public string VacancyName { get; set; }
        public string ClientName { get; set; }
        public int ClientId { get; set; }
        public string rowType { get; set; }
        public string CandidateName { get; set; }
    }

    public partial class VacancyStatusType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool CountSuccess { get; set; }
        public int OrderId { get; set; }
        public bool CountFail { get; set; }
    }

    public partial class VacancyTag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
