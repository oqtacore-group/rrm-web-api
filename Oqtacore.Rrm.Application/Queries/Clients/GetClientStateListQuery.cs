
namespace Oqtacore.Rrm.Application.Queries.Clients
{
    public class GetClientStateListQuery : ListQuery<GetClientStateListQueryResult>
    {
    }
    public class GetClientStateListQueryResult : ListQueryResult<GetClientStateListQueryResultItem>
    {
    }
    public class GetClientStateListQueryResultItem
    {
        public string Name { get; set; }
        public int ClientCount { get; set; }
    }
}