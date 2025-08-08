using MediatR;
using Microsoft.AspNetCore.Mvc;
using Oqtacore.Rrm.Application.Queries.Clients;
using Oqtacore.Rrm.Application.Queries.SiteSettings;

namespace Oqtacore.Rrm.Api.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : ApiControllerBase
    {
        public SettingsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult> GetSettings(string name = "")
        {
            var queryResult = await Mediator.Send(new GetSiteSettingQuery { Name = name });

            return Ok(queryResult);
        }
    }
}
