using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Infrastructure.Data;
using System;
using TimeZoneConverter;

namespace Oqtacore.Rrm.Application.Queries
{
    public class GetCandidateEventListQueryHandler : IRequestHandler<GetCandidateEventListQuery, GetCandidateEventListQueryResult>
    {
        private ApplicationContext _dataContext;
        public GetCandidateEventListQueryHandler(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<GetCandidateEventListQueryResult> Handle(GetCandidateEventListQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new GetCandidateEventListQueryResult();

            var eventList = await _dataContext.AllCandidateEventViewModel.ToListAsync();
            var eventTypeList = await _dataContext.CandidateEventType.ToListAsync();
            
            var result = eventList.Select(x =>
            {
                var item = new GetCandidateEventListQueryResultItem
                {
                    id = x.id,
                    Date = x.Date,
                    CandidateId = x.CandidateId,
                    Caption = x.Caption,
                    TypeId = x.TypeId,
                    Location = x.Location,
                    EventType = x.EventType,
                    Completed = x.Completed,
                    ZoomLink = x.ZoomLink,
                    CandidateName = x.CandidateName,
                };
                if (x.EventType != "rrm" && x.Date.HasValue)
                    item.Date = TimeZoneInfo.ConvertTimeFromUtc(x.Date.Value, TZConvert.GetTimeZoneInfo(TZConvert.IanaToWindows("Europe/Moscow")));

                return item;
            })
                .ToList();

            queryResult.Data = result;
            queryResult.TotalCount = result.Count;
            queryResult.eventTypeList = eventTypeList;

            return queryResult;
        }
    }
}