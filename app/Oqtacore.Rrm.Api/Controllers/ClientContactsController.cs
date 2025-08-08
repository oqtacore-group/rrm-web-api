using MediatR;
using Microsoft.AspNetCore.Mvc;
using Oqtacore.Rrm.Application.Commands.Clients;

namespace Oqtacore.Rrm.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClientContactsController : ApiControllerBase
    {
        public ClientContactsController(IMediator mediator) : base(mediator)
        {            
        }
        [HttpPost]
        public async Task<ActionResult> PostContact([FromBody] AddClientContactCommand request) => Ok(await Mediator.Send(request));

        [HttpPut("{id}")]
        public async Task<ActionResult> PutContact(int id, [FromBody] UpdateClientContactCommand request)
        {
            request.Id = id;
            var result = await Mediator.Send(request);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContact(int id) => Ok(await Mediator.Send(new DeleteClientContactCommand { Id = id }));
    }
}