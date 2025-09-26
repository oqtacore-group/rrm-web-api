using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Application.Queries
{
    public class GetCandidateEventQueryHandler : IRequestHandler<GetCandidateEventQuery, GetCandidateEventQueryResult>
    {
        private readonly ApplicationContext _dataContext;
        public GetCandidateEventQueryHandler(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<GetCandidateEventQueryResult> Handle(GetCandidateEventQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new GetCandidateEventQueryResult();

            var queryable = _dataContext.CandidateEvent.Where(x => x.id == request.Id);

            var candidateList = await _dataContext.Candidate.ToListAsync();
            var eventTypeList = await _dataContext.CandidateEventType.ToListAsync();

            var result = await queryable.Select(x => new GetCandidateEventQueryResultItem
            {
                Id = x.id,
                CandidateId = x.CandidateId,
                TypeId = x.TypeId,
                Date = x.Date,
                Caption = x.Caption,
                Completed = x.Completed,
                CreatedBy = x.CreatedBy,
                ZoomLink = x.ZoomLink,
                ReminderSent = x.ReminderSent,
                ReminderEarlySent = x.ReminderEarlySent,
                HashCode = x.HashCode,
            })
                .FirstOrDefaultAsync();

            if (result == null)
                result = new GetCandidateEventQueryResultItem();

            queryResult.Data = result;
            queryResult.CandidateList = candidateList;
            queryResult.EventTypeList = eventTypeList;


            return queryResult;
        }
    }
}
