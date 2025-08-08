using MediatR;
using Oqtacore.Rrm.Domain.Entity;
using Oqtacore.Rrm.Domain.Repository;
using System.Text.Json;

namespace Oqtacore.Rrm.Application.Commands
{
    public class RequestHandler<T, TOut> : RequestHandlerBase<T, TOut>
        where T : IRequest<TOut>
    where TOut : Result, new()
    {
        public RequestHandler(IRepository repository, IHttpService httpService) : base(repository, httpService)
        {
        }
        protected async override Task<TOut> Execute(T request, CancellationToken cancellationToken)
        {
            //Do something
            var result = await Execute(request, cancellationToken);

            return result;
        }

        protected override Task<bool> HasAccess(T request)
        {
            return Task.FromResult(true);
        }
        public override async Task DoAuditLog(T request, TOut result)
        {
            //Do Audit Log
            var auditLog = new AuditLog
            {
                UserId = HttpService.LogonUserId > 0 ? HttpService.LogonUserId : null,
                Action = typeof(T).Name,
                Method = HttpService.Method,
                IPAddress = HttpService.IPAddress,
                UserAgent = HttpService.UserAgent,
                RequestBody = JsonSerializer.Serialize(request),
                ResponseBody = JsonSerializer.Serialize(result),
                ErrorMessage = result.Message,
                Success = result.Success,
                Client = HttpService.ClientId,
                CreatedOn = DateTime.Now
            };
            
            //await Repository.Add(auditLog);
        }
    }
}