
namespace Oqtacore.Rrm.Application.Commands.Candidates
{
    public class AddCandidateEventCommand : ICommand<AddCandidateEventResult>
    {
        public int CandidateId { get; set; }
        public int TypeId { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Caption { get; set; }
        public string ZoomLink { get; set; }
    }
    public class AddCandidateEventResult : Result
    {
    }
}
