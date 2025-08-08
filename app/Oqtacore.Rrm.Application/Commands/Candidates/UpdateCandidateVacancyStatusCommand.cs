using System;

namespace Oqtacore.Rrm.Application.Commands.Candidates
{
    public class UpdateCandidateVacancyStatusCommand : ICommand<UpdateCandidateVacancyStatusResult>
    {
        public int VacancyId { get; set; }
        public int CandidateId { get; set; }
        public int StatusId { get; set; }
        public string? Note { get; set; }
    }
    public class UpdateCandidateVacancyStatusResult : Result
    {
    }
}
