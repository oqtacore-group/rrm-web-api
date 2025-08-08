using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Oqtacore.Rrm.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class ApiControllerBase : ControllerBase
    {
        protected IMediator Mediator { get; private set; }
        public ApiControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}