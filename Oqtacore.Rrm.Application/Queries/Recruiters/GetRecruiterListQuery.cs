
namespace Oqtacore.Rrm.Application.Queries.Recruiters
{
    public class GetRecruiterListQuery : ListQuery<GetRecruiterListQueryResult>
    {
    }
    public class GetRecruiterListQueryResult : ListQueryResult<GetRecruiterListQueryResultItem>
    {
    }
    public class GetRecruiterListQueryResultItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AdminState { get; set; }
        public string AuthId { get; set; }
        public string Email { get; set; }
        public bool ServiceAccount { get; set; }
    }
}