using MediatR;

namespace Oqtacore.Rrm.Application.Commands
{
    public interface ICommand<TResult> : IRequest<TResult>
    {
    }
    public interface IResult
    {
        bool Success { get; set; }
        string Message { get; set; }
    }
    public class Result : IResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}