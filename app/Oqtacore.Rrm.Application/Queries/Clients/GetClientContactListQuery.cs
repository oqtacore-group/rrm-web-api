using System;

namespace Oqtacore.Rrm.Application.Queries.Clients
{
    public class GetClientContactListQuery : ListQuery<GetClientContactListQueryResult>
    {
        public int ClientId { get; set; }
    }
    public class GetClientContactListQueryResult : ListQueryResult<GetClientContactListQueryResultItem>
    {
    }
    public class GetClientContactListQueryResultItem
    {
        public int id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}