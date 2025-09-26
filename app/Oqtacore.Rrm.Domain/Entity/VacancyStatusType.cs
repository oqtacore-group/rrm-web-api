
namespace Oqtacore.Rrm.Domain.Entity
{
    public class VacancyStatusType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool CountSuccess { get; set; }
        public int OrderId { get; set; }
        public bool CountFail { get; set; }
    }
}
