using MediatR;
using Oqtacore.Rrm.Domain.Repository;

namespace Oqtacore.Rrm.Application.Commands
{
    public abstract class RequestHandlerBase<T, TOut> : IRequestHandler<T, TOut>
        where T : IRequest<TOut>
        where TOut : Result, new()
    {
        protected readonly IRepository Repository;
        protected readonly IHttpService HttpService;
        protected int LogonUserId { get; private set; }
        protected string LogonUserEmail { get; private set; }
        protected List<int> LogonUserRoles { get; private set; }
        public RequestHandlerBase(IRepository repository, IHttpService httpService)
        {
            Repository = repository;
            HttpService = httpService;
            LogonUserId = HttpService.LogonUserId;
            LogonUserEmail = HttpService.LogonUserEmail;
            LogonUserRoles = httpService.LogonUserRoles;
        }
        public async Task<TOut> Handle(T request, CancellationToken cancellationToken)
        {
            if (!await HasAccess(request))
            {
                return
                    new TOut()
                    {
                        Message = "AuthorizationFailException_NotAuthorized",
                        Success = false
                    };
            }

            LogonUserId = HttpService.LogonUserId;
            LogonUserEmail = HttpService.LogonUserEmail;

            var result = await Execute(request, cancellationToken);

            await DoAuditLog(request, result);

            return result;
        }
        public virtual async Task DoAuditLog(T request, TOut result)
        {
        }
        protected abstract Task<bool> HasAccess(T request);
        protected abstract Task<TOut> Execute(T request, CancellationToken cancellationToken);
    }
}