using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oqtacore.Rrm.Domain.Common
{
    public class QueryResult<T> : QueryResult
    {
        public List<T> Data { get; set; }
    }
    public class QueryResult
    {
        public int Total { get; set; }
    }
}
