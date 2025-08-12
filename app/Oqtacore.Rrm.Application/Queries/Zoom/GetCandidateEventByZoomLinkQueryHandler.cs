using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Domain.Repository;
using Oqtacore.Rrm.Infrastructure.Data;
using System.Text.RegularExpressions;

namespace Oqtacore.Rrm.Application.Queries.Zoom
{
    public class GetCandidateEventByZoomLinkQueryHandler : IRequestHandler<GetCandidateEventByZoomLinkQuery, GetCandidateEventByZoomLinkQueryResult>
    {
        private readonly ApplicationContext _dataContext;
        public GetCandidateEventByZoomLinkQueryHandler(ICandidateRepository candidateRepository, ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<GetCandidateEventByZoomLinkQueryResult> Handle(GetCandidateEventByZoomLinkQuery request, CancellationToken cancellationToken)
        {
            var meetingId = ExtractZoomMeetingId(request.ZoomLink);

            if (string.IsNullOrWhiteSpace(meetingId))
            {
                return null;
            }

            var user = await _dataContext.CandidateEvent.FirstOrDefaultAsync(x => x.ZoomLink == request.ZoomLink, cancellationToken);

            return new GetCandidateEventByZoomLinkQueryResult
            {
                Data = new GetUserByZoomResultId
                {
                    Id = user.CandidateId
                }
            };
        }

        private string ExtractZoomMeetingId(string link)
        {
            var match = Regex.Match(link, @"(?<=/j/|^)\d{9,11}");
            return match.Success ? match.Value : link.Trim();
        }

    }
}
