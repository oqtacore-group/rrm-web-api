using Oqtacore.Rrm.Application.Commands;

namespace Oqtacore.Rrm.Application.Queries
{
    public class ListQueryResult<T> : Result
    {
        public long TotalCount { get; set; }
        public List<T>? Data { get; set; }
    }
    public class SingleQueryResult<T> : Result
    {
        public T? Data { get; set; }
    }
}