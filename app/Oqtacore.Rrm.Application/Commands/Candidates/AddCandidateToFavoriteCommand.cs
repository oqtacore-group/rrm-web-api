using System;

namespace Oqtacore.Rrm.Application.Commands.Candidates
{
    public class AddCandidateToFavoriteCommand : ICommand<AddCandidateToFavoriteResult>
    {
        public int Id { get; set; }
        public bool IsFavorite { get; set; }
    }
    public class AddCandidateToFavoriteResult : Result
    {
    }
}
