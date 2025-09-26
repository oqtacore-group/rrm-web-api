
namespace Oqtacore.Rrm.Application.Commands.Candidates
{
    public class UpdateCandidateEventCommand : ICommand<UpdateCandidateEventResult>
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int TypeId { get; set; }
        public DateTime Date { get; set; }
        public string Caption { get; set; }
        public string ZoomLink { get; set; }
    }
    public class UpdateCandidateEventResult : Result
    {
    }
}
