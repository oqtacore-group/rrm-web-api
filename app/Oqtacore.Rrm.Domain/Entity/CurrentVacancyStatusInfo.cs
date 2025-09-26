using System;

namespace Oqtacore.Rrm.Domain.Entity
{
    public class CurrentVacancyStatusInfo
    {
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
}