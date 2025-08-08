
namespace Oqtacore.Rrm.Application.Queries.Clients
{
    public class GetClientListQuery : ListQuery<GetClientListQueryResult>
    {
        public string StateName { get; set; }
    }
    public class GetClientListQueryResult : ListQueryResult<GetClientListQueryResultItem>
    {
    }
    public class GetClientListQueryResultItem
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SiteUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
    }
}