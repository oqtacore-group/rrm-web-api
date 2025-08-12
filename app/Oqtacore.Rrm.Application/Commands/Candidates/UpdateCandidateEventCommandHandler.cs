using MediatR;
using Oqtacore.Rrm.Domain.Exceptions;
using Oqtacore.Rrm.Domain.Repository;

namespace Oqtacore.Rrm.Application.Commands.Candidates
{
    public class UpdateCandidateEventCommandHandler : RequestHandler<UpdateCandidateEventCommand, UpdateCandidateEventResult>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IAdminRepository _adminRepository;
        public UpdateCandidateEventCommandHandler(ICandidateRepository candidateRepository, IAdminRepository adminRepository, IRepository repository, IHttpService httpService) : base(repository, httpService)
        {
            _candidateRepository = candidateRepository;
            _adminRepository = adminRepository;
        }
        protected override async Task<UpdateCandidateEventResult> Execute(UpdateCandidateEventCommand request, CancellationToken cancellationToken)
        {
            var result = new UpdateCandidateEventResult();

            var admin = await _adminRepository.GetByEmail(HttpService.LogonUserEmail);
            if (admin == null)
                throw new AuthorizationFailException("Authorization Failed.");

            var candidateEvent = await _candidateRepository.GetEvent(request.Id);

            if (candidateEvent == null)
            {
                result.Message = "Candidate Event not found.";
                return result;
            }

            if (request.ZoomLink == null || (!request.ZoomLink.Contains("zoom.us") && request.ZoomLink != "NA"))
            {
                result.Message = "Нужно заполнить ссылку на Zoom или поставить NA";
                
                return result;
            }

            var candidate = await _candidateRepository.Get(request.CandidateId);
            if (candidate == null) 
            {
                result.Message = "Candidate not found.";
                return result;
            }

            candidateEvent.CandidateId = request.CandidateId;
            candidateEvent.TypeId = request.TypeId;
            candidateEvent.Date = request.Date;
            candidateEvent.Caption = request.Caption;
            candidateEvent.ZoomLink = request.ZoomLink;

            await Repository.Update(candidateEvent);

            result.Success = true;

            return result;
        }
    }
}