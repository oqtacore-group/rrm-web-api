
namespace Oqtacore.Rrm.Domain.Entity
{

    public partial class AllVacancyStatusInfo
    {
        public int StatusId { get; set; }
        public string Name { get; set; }
        public bool? CountSuccess { get; set; }
        public int OrderId { get; set; }
        public int CandidateCount { get; set; }
    }
    public class VacancyStatusInfo
    {
        public int VacancyId { get; set; }
        public int StatusId { get; set; }
        public string Name { get; set; }
        public bool? CountSuccess { get; set; }
        public int OrderId { get; set; }
        public int CandidateCount { get; set; }
    }
}