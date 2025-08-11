namespace Oqtacore.Rrm.Application.Queries.Zoom
{
    public class GetCandidateEventByZoomLinkQuery : SingleQuery<GetCandidateEventByZoomLinkQueryResult> //GetCandidateEventByZoomLinkQuery
    {
        public string? ZoomLink { get; set; }
    }

    public class GetCandidateEventByZoomLinkQueryResult : SingleQueryResult<GetUserByZoomResultId>
    {
    }

    public class GetUserByZoomResultId
    {
        public int Id { get; set; }
    }
}

