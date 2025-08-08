
namespace Oqtacore.Rrm.Domain.Entity
{
    public class RecruiterStatisticInfo
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int VacancyId { get; set; }
        public int VacancyStatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime DateAdded { get; set; }
        public int? CreatedBy { get; set; }
        public bool? Success { get; set; }
        public bool? Fail { get; set; }
        public string AdminName { get; set; }
    }
}