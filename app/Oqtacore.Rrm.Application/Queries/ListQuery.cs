using MediatR;

namespace Oqtacore.Rrm.Application.Queries
{
    public interface IQuery<TResult> : IRequest<TResult>
    {
    }
    public interface IListQuery<TResult> : IQuery<TResult>
    {
        int Page { get; set; }
        int PageSize { get; set; }
        string? Sort { get; set; }
    }
    public interface ISingleQuery<TResult> : IQuery<TResult>
    {
    }
    public class ListQuery<TResult> : IListQuery<TResult>
    {
        private int _page;
        private int _pageSize;
        public int Page { get { return _page; }
            set
            {
                if(value > 0)
                    _page = value;
                else
                    _page = 0;
            } 
        }
        public int PageSize 
        { 
            get { return _pageSize; } 
            set 
            { 
                if (value == 0) 
                    _pageSize = int.MaxValue; 
                else _pageSize = value; 
            } 
        }
        public string? Sort { get; set; }
        public bool Descending { get; set; }
    }
    public class SingleQuery<TResult> : ISingleQuery<TResult>
    {
    }
}
