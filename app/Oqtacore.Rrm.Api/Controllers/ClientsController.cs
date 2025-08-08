using MediatR;
using Microsoft.AspNetCore.Mvc;
using Oqtacore.Rrm.Application.Commands.Clients;
using Oqtacore.Rrm.Application.Queries.Clients;
using Oqtacore.Rrm.Domain.Entity;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClientsController : ApiControllerBase
    {
        public ClientsController(IMediator mediator) : base(mediator)
        {
        }
 
        [HttpGet]
        public async Task<ActionResult> GetClients(string stateName = "")
        {
            var queryResult = await Mediator.Send(new GetClientListQuery { StateName = stateName });

            return Ok(queryResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetClient(int id) => Ok(await Mediator.Send(new GetClientQuery { Id = id }));
        
        [HttpGet("Contacts/{clientId}")]
        public async Task<ActionResult> GetClientContacts(int clientId) => Ok(await Mediator.Send(new GetClientContactListQuery { ClientId = clientId }));
        
        [HttpGet("StateSummary")]
        public async Task<ActionResult> GetClientStates()
        {
            var clients = await Mediator.Send(new GetClientStateListQuery());

            return Ok(clients);
        }

        [HttpPost]
        public async Task<ActionResult> PostClient([FromBody] AddClientCommand request)
        {
            var result = await Mediator.Send(request);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutClient(int id, [FromBody] UpdateClientCommand request)
        {
            request.Id = id;
            var result = await Mediator.Send(request);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            var result = await Mediator.Send(new DeleteClientCommand { Id = id });

            return Ok(result);
        }
    }
}