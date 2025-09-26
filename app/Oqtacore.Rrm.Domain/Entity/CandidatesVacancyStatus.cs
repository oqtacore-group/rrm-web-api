using System;

namespace Oqtacore.Rrm.Domain.Entity
{
    public class CandidatesVacancyStatus
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int VacancyId { get; set; }
        public int VacancyStatusId { get; set; }
        public System.DateTime DateAdded { get; set; }
        public string? Note { get; set; }
        public int? CreatedBy { get; set; }
    }
}