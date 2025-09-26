using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oqtacore.Rrm.Domain.Models
{

    public class Candidate
    {
        public int id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? profileUrl { get; set; }
        public int ContactDataId { get; set; }
        public string? Note { get; set; }
        public string? Location { get; set; }
        public string? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? RelocationReady { get; set; }
        public bool? RemoteWorkplaceReady { get; set; }
        public int? SalaryWish { get; set; }
        public string? SalaryCurrency { get; set; }
        public string? PhotoUrl { get; set; }
        public string? MainSkill { get; set; }
        public bool? Leader { get; set; }
        public string? HowKnowAboutVacancy { get; set; }
        public int? CreatedBy { get; set; }
        public string? HHurl { get; set; }
        public string? lastjob_position { get; set; }
        public string? lastjob_company { get; set; }
        public string? salary { get; set; }
        public string? resume_text { get; set; }
        public bool? favorite { get; set; }
        /// <summary>
        /// The IsDeleted property is used to mark the entity as deleted without actually removing it from the database.
        /// </summary>
        public bool? IsDeleted { get; set; }
        public void editProfile(Candidate edited)
        {
            this.Email = edited.Email;
            this.Name = edited.Name;
            this.PhoneNumber = edited.PhoneNumber;
            this.ContactDataId = edited.ContactDataId;
            this.DateOfBirth = edited.DateOfBirth;
            this.HowKnowAboutVacancy = edited.HowKnowAboutVacancy;
            this.Leader = edited.Leader;
            this.Location = edited.Location;
            this.MainSkill = edited.MainSkill;
            this.Note = edited.Note;
            this.PhotoUrl = edited.PhotoUrl;
            this.RelocationReady = edited.RelocationReady;
            this.RemoteWorkplaceReady = edited.RemoteWorkplaceReady;
            //TODO:this.ResumeExperiences = edited.ResumeExperiences;
            this.SalaryCurrency = edited.SalaryCurrency;
            this.SalaryWish = edited.SalaryWish;
            this.Sex = edited.Sex;
            this.HHurl = edited.HHurl;
            this.lastjob_company = edited.lastjob_company;
            this.lastjob_position = edited.lastjob_position;
            this.salary = edited.salary;
            this.resume_text = edited.resume_text;
            this.favorite = edited.favorite;
        }
    }

    public partial class CandidateArchive
    {
        public CandidateArchive()
        {
            
        }
        public CandidateArchive(Candidate _candidate, Admin _admin, string _actionType)
        {
            this.ActionBy = _admin.id;
            this.ActionDate = DateTime.UtcNow;
            this.ActionType = _actionType;
            this.ContactDataId = _candidate.ContactDataId;
            this.CreatedBy = _candidate.CreatedBy;
            this.DateOfBirth = _candidate.DateOfBirth;
            this.Email = _candidate.Email;
            this.favorite = _candidate.favorite;
            this.HHurl = _candidate.HHurl;
            this.HowKnowAboutVacancy = _candidate.HowKnowAboutVacancy;
            this.id = _candidate.id;
            this.lastjob_company = _candidate.lastjob_company;
            this.lastjob_position = _candidate.lastjob_position;
            this.Leader = _candidate.Leader;
            this.Location = _candidate.Location;
            this.MainSkill = _candidate.MainSkill;
            this.Name = _candidate.Name;
            this.Note = _candidate.Note;
            this.PhoneNumber = _candidate.PhoneNumber;
            this.PhotoUrl = _candidate.PhotoUrl;
            this.profileUrl = _candidate.profileUrl;
            this.RelocationReady = _candidate.RelocationReady;
            this.RemoteWorkplaceReady = _candidate.RemoteWorkplaceReady;
            this.resume_text = _candidate.resume_text;
            this.salary = _candidate.salary;
            this.SalaryCurrency = _candidate.SalaryCurrency;
            this.SalaryWish = _candidate.SalaryWish;
            this.Sex = _candidate.Sex;
        }
        public int id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string profileUrl { get; set; }
        public Nullable<int> ContactDataId { get; set; }
        public string Note { get; set; }
        public string Location { get; set; }
        public string Sex { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<bool> RelocationReady { get; set; }
        public Nullable<bool> RemoteWorkplaceReady { get; set; }
        public Nullable<int> SalaryWish { get; set; }
        public string SalaryCurrency { get; set; }
        public string PhotoUrl { get; set; }
        public string MainSkill { get; set; }
        public Nullable<bool> Leader { get; set; }
        public string HowKnowAboutVacancy { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string HHurl { get; set; }
        public string lastjob_position { get; set; }
        public string lastjob_company { get; set; }
        public string salary { get; set; }
        public string resume_text { get; set; }
        public Nullable<bool> favorite { get; set; }
        public System.DateTime ActionDate { get; set; }
        public string ActionType { get; set; }
        public int ActionBy { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int ActionId { get; set; }

    }

}
