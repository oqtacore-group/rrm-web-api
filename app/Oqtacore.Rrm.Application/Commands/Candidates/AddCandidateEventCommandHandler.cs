using Oqtacore.Rrm.Domain.Entity;
using Oqtacore.Rrm.Domain.Exceptions;
using Oqtacore.Rrm.Domain.Repository;
using System.Globalization;

namespace Oqtacore.Rrm.Application.Commands.Candidates
{
    public class AddCandidateEventCommandHandler : RequestHandler<AddCandidateEventCommand, AddCandidateEventResult>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IAdminRepository _adminRepository;
        public AddCandidateEventCommandHandler(ICandidateRepository candidateRepository, IAdminRepository adminRepository, IRepository repository, IHttpService httpService) : base(repository, httpService)
        {
            _candidateRepository = candidateRepository;
            _adminRepository = adminRepository;
        }
        protected override async Task<AddCandidateEventResult> Execute(AddCandidateEventCommand request, CancellationToken cancellationToken)
        {
            var result = new AddCandidateEventResult();

            var admin = await _adminRepository.GetByEmail(HttpService.LogonUserEmail);
            if(admin == null)
                throw new AuthorizationFailException("Authorization Failed.");

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


            var newCandidateEvent = new CandidateEvent
            {
                CandidateId = request.CandidateId,
                CreatedBy = admin.id,
                //TODO hamid
                TypeId = 2,
                HashCode = Guid.NewGuid().ToString(),
                Date = request.Date.ToUniversalTime(),
                Caption = request.Caption,
                ZoomLink = request.ZoomLink,
                Completed = false,
            };

            // Parse the time string into a TimeSpan
            if (TimeSpan.TryParseExact(request.Time, @"hh\:mm", CultureInfo.InvariantCulture, out TimeSpan parsedTime))
            {
                // Combine the date with the parsed time
                DateTime dateTimeWithTime = request.Date.Date.Add(parsedTime);
                newCandidateEvent.Date = dateTimeWithTime.ToUniversalTime();
            }

            await Repository.Add(newCandidateEvent);

            result.Success = true;

            return result;
        }
    }
}