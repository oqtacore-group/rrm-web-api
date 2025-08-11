using Oqtacore.Rrm.Domain.Repository;

namespace Oqtacore.Rrm.Application.Commands.Candidates
{
    public class AddCandidateToFavoriteCommandHandler : RequestHandler<AddCandidateToFavoriteCommand, AddCandidateToFavoriteResult>
    {
        private readonly ICandidateRepository _candidateRepository;
        public AddCandidateToFavoriteCommandHandler(ICandidateRepository candidateRepository, IRepository repository, IHttpService httpService) : base(repository, httpService)
        {
            _candidateRepository = candidateRepository;
        }
        protected override async Task<AddCandidateToFavoriteResult> Execute(AddCandidateToFavoriteCommand request, CancellationToken cancellationToken)
        {
            var result = new AddCandidateToFavoriteResult();

            var candidate = await _candidateRepository.Get(request.Id);
            if (candidate == null)
            {
                result.Message = "Candidate not found.";
                return result;
            }

            candidate.favorite = request.IsFavorite;

            await Repository.Update(candidate);

            result.Success = true;
            result.Message = "The candidate updated successfully.";

            return result;
        }
    }
}