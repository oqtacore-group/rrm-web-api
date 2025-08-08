using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oqtacore.Rrm.Domain.Models
{

    public class Vacancy
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public int WorkplaceNumber { get; set; }
        public string? Status { get; set; }
        public int? SalaryLowerEnd { get; set; }
        public int? SalaryHighEnd { get; set; }
        public string? SalaryCurrency { get; set; }
        public decimal? Experience { get; set; }
        public string? Location { get; set; }
        public string? LocalTime { get; set; }
        public bool? RelocationHelp { get; set; }
        public bool? RemoteWorkPlace { get; set; }
        public int CreatedBy { get; set; }
        public bool Opened { get; set; }
        public string? Responsibility { get; set; }
        public string? Skills { get; set; }
        public string? PersonalQuality { get; set; }
        public string? Languages { get; set; }
        public string? Notes { get; set; }
        /// <summary>
        /// IsDeleted is used to mark the record as deleted without actually deleting it from the database.
        /// </summary>
        public bool? IsDeleted { get; set; }

        public void editProfile(Vacancy edited)
        {
            this.ClientId = edited.ClientId;
            this.Name = edited.Name;
            this.Experience = edited.Experience;
            this.LocalTime = edited.LocalTime;
            this.Location = edited.Location;
            this.Notes = edited.Notes;
            this.RelocationHelp = edited.RelocationHelp;
            this.RemoteWorkPlace = edited.RemoteWorkPlace;
            this.SalaryCurrency = edited.SalaryCurrency;
            this.SalaryLowerEnd = edited.SalaryLowerEnd;
            this.SalaryHighEnd = edited.SalaryHighEnd;
            this.Status = edited.Status;
            this.WorkplaceNumber = edited.WorkplaceNumber;
            this.Responsibility = edited.Responsibility;
            this.Skills = edited.Skills;
            this.PersonalQuality = edited.PersonalQuality;
            this.Languages = edited.Languages;

        }
    }
    public class VacancyArchive
    {
        public VacancyArchive() { }
        public VacancyArchive(Vacancy _vacancy, Admin _admin, string _actionType)
        {
            this.ActionBy = _admin.id;
            this.ActionDate = DateTime.UtcNow;
            this.ActionType = _actionType;
            this.ClientId = _vacancy.ClientId;
            this.CreatedBy = _vacancy.CreatedBy;
            this.Experience = _vacancy.Experience;
            this.id = _vacancy.id;
            this.Languages = _vacancy.Languages;
            this.LocalTime = _vacancy.LocalTime;
            this.Location = _vacancy.Location;
            this.Name = _vacancy.Name;
            this.Notes = _vacancy.Notes;
            this.Opened = _vacancy.Opened;
            this.PersonalQuality = _vacancy.PersonalQuality;
            this.RelocationHelp = _vacancy.RelocationHelp;
            this.RemoteWorkPlace = _vacancy.RemoteWorkPlace;
            this.Responsibility = _vacancy.Responsibility;
            this.SalaryCurrency = _vacancy.SalaryCurrency;
            this.SalaryHighEnd = _vacancy.SalaryHighEnd;
            this.SalaryLowerEnd = _vacancy.SalaryLowerEnd;
            this.Skills = _vacancy.Skills;
            this.Status = _vacancy.Status;
            this.WorkplaceNumber = _vacancy.WorkplaceNumber;
        }
        public int id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public int WorkplaceNumber { get; set; }
        public string Status { get; set; }
        public Nullable<int> SalaryLowerEnd { get; set; }
        public Nullable<int> SalaryHighEnd { get; set; }
        public string SalaryCurrency { get; set; }
        public Nullable<decimal> Experience { get; set; }
        public string Location { get; set; }
        public string LocalTime { get; set; }
        public Nullable<bool> RelocationHelp { get; set; }
        public Nullable<bool> RemoteWorkPlace { get; set; }
        public int CreatedBy { get; set; }
        public bool Opened { get; set; }
        public string Responsibility { get; set; }
        public string Skills { get; set; }
        public string PersonalQuality { get; set; }
        public string Languages { get; set; }
        public string Notes { get; set; }
        public System.DateTime ActionDate { get; set; }
        public string ActionType { get; set; }
        public int ActionBy { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int ActionId { get; set; }
    }

}
