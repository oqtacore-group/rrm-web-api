using System;

namespace Oqtacore.Rrm.Application.Queries.Clients
{
    public class GetClientQuery : SingleQuery<GetClientQueryResult>
    {
        public int Id { get; set; }
    }
    public class GetClientQueryResult : SingleQueryResult<GetClientQueryResultItem>
    {
    }
    public class GetClientQueryResultItem
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SiteUrl { get; set; }
    }
}