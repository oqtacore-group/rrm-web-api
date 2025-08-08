using MediatR;
using Microsoft.AspNetCore.Mvc;
using Oqtacore.Rrm.Application.Commands.Candidates;
using Oqtacore.Rrm.Application.Queries;
using Oqtacore.Rrm.Application.Queries.Candidates;
using Oqtacore.Rrm.Domain.Models;

namespace Oqtacore.Rrm.Api.Controllers
{
    [Route("api/[controller]")]
    public class CandidatesController : ApiControllerBase
    {
        public CandidatesController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet]
        public async Task<ActionResult> GetCandidates(int page, int pageSize, string sort, bool descending = false, int? vacancyId = null, int? vacancyStatusId = null)
        {
            var query = new GetCandidateListQuery { Page = page, PageSize = pageSize, Sort = sort, Descending = descending, VacancyId = vacancyId, VacancyStatusId = vacancyStatusId };
            var queryResult = await Mediator.Send(query);

            return Ok(queryResult);
        }
        
        [HttpGet("Events/{eventId}")]
        public async Task<ActionResult> GetCandidateEvents(int eventId)
        {
            var query = new GetCandidateEventQuery { Id = eventId };
            var queryResult = await Mediator.Send(query);

            return Ok(queryResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> GetCandidate(int id)
        {
            var queryResult = await Mediator.Send(new GetCandidateQuery { Id = id });

            return Ok(queryResult);
        }

        [HttpPost]
        public async Task<ActionResult> PostCandidate([FromBody] AddCandidateCommand request)
        {
            var result = await Mediator.Send(request);

            return Ok(result);
        }
        
        [HttpPost("Events")]
        public async Task<ActionResult> PostCandidateEvent([FromBody] AddCandidateEventCommand request)
        {
            var result = await Mediator.Send(request);

            return Ok(result);
        }

        [HttpPost("ChangeVacancyStatus")]
        public async Task<ActionResult> PostCandidateVacancyStatus([FromBody] UpdateCandidateVacancyStatusCommand request)
        {
            var result = await Mediator.Send(request);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCandidate(int id, [FromBody] UpdateCandidateCommand request)
        {
            request.Id = id;
            var result = await Mediator.Send(request);

            return Ok(result);
        }
        
        [HttpPut("AddToFavorite/{id}")]
        public async Task<ActionResult> PutCandidateToFavorite(int id, [FromBody] AddCandidateToFavoriteCommand request)
        {
            request.Id = id;
            var result = await Mediator.Send(request);

            return Ok(result);
        }

        [HttpPut("Events/{eventId}")]
        public async Task<ActionResult> PutCandidateEvent(int eventId, [FromBody] UpdateCandidateEventCommand request)
        {
            request.Id = eventId;
            var result = await Mediator.Send(request);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteCandidateCommand { Id = id });

            return Ok(result);
        }

    }
}