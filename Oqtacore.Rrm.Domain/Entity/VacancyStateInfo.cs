
namespace Oqtacore.Rrm.Domain.Entity
{
    public class VacancyStateInfo
    {
        public int VacancyId { get; set; }
        public string Name { get; set; }
        public string ClientName { get; set; }
        public int? SuccessCount { get; set; }
        public int? WorkplaceNumber { get; set; }
        public int? CandidateCount { get; set; }
        public int? ClientId { get; set; }
        public int? CreatedBy { get; set; }
    }
}
