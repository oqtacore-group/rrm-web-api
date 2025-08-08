using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oqtacore.Rrm.Application.Queries.Recruiters
{
    public class GetCurrentRecruiterQuery : SingleQuery<GetCurrentRecruiterQueryResult>
    {
    }
    public class GetCurrentRecruiterQueryResult : SingleQueryResult<GetCurrentRecruiterQueryResultItem>
    {

    }
    public class GetCurrentRecruiterQueryResultItem
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
