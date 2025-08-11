using MediatR;
using Microsoft.AspNetCore.Mvc;
using Oqtacore.Rrm.Application.Queries;

namespace Oqtacore.Rrm.Api.Controllers
{
    [Route("api/[controller]")]
    public class CalendarsController : ApiControllerBase
    {
        public CalendarsController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet]
        public async Task<ActionResult> GetCalendars()
        {
            var queryResult = await Mediator.Send(new GetCandidateEventListQuery { });

            return Ok(queryResult);
        }
    }
}