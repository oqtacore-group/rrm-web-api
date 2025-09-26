using MediatR;
using Microsoft.AspNetCore.Mvc;
using Oqtacore.Rrm.Application.Queries;

namespace Oqtacore.Rrm.Api.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : ApiControllerBase
    {
        public SearchController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet]
        public async Task<ActionResult> Search(string txt)
        {
            var queryResult = await Mediator.Send(new SearchQuery { SearchText = txt });

            return Ok(queryResult);
        }
    }
}