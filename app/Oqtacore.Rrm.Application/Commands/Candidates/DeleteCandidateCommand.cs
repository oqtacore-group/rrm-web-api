using System;

namespace Oqtacore.Rrm.Application.Commands.Candidates
{
    public class DeleteCandidateCommand : ICommand<DeleteCandidateResult>
    {
        public int Id { get; set; }
    }
    public class DeleteCandidateResult : Result
    {
    }
}