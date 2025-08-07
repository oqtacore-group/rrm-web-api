using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Oqtacore.Rrm.Application.Commands.Vacancies;
using Oqtacore.Rrm.Application.Queries.Vacancies;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Api.Controllers
{
    [Route("api/[controller]")]
    public class VacanciesController : ApiControllerBase
    {
        private ApplicationContext _dataContext;
        public VacanciesController(ApplicationContext dataContext, IMediator mediator) : base(mediator)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetVacancies()
        {
            var queryResult = await Mediator.Send(new GetVacancyListQuery { });

            return Ok(queryResult);
        }

        [HttpGet("ByCandidate")]
        public ActionResult GetVacancyList(int candidateId = 0)
        {
            var clients = new List<Client>();
            var vacancies = new List<Vacancy>();

            if (candidateId == 0 || !_dataContext.Candidate.Any(t => t.id == candidateId))
            {
                vacancies = _dataContext.Vacancy.ToList();
                clients = _dataContext.Client.Where(t => _dataContext.Vacancy.Any(x => x.ClientId == t.id)).ToList();
            }
            else
            {
                vacancies = _dataContext.Vacancy.Where(t => !_dataContext.CandidatesVacancyStatus.Any(x => x.CandidateId == candidateId && x.VacancyId == t.id)).ToList();
                clients = _dataContext.Client.Where(t => _dataContext.Vacancy.Any(x => x.ClientId == t.id && !_dataContext.CandidatesVacancyStatus.Any(f => f.CandidateId == candidateId && f.VacancyId == x.id))).ToList();
            }

            return Ok(new { list = vacancies, client_list = clients, success = true });
        }

        [HttpGet("States")]
        public async Task<ActionResult> GetVacancyStates(int clientId)
        {
            var queryResult = await Mediator.Send(new GetVacancyStateListQuery { ClientId = clientId });

            return Ok(queryResult);
        }

        [HttpGet("StatusList")]
        public ActionResult GetVacancyStatusList(int vacancyId)
        {
            if (vacancyId == 0 || !_dataContext.Vacancy.Any(t => t.id == vacancyId))
            {

                var list = _dataContext.AllVacancyStatusListingViewModel.OrderBy(t => t.OrderId).ToList();
                return Ok(new { data = list, success = true });
            }
            else
            {
                var list = _dataContext.VacancyStatusListingViewModel.Where(t => t.VacancyId == vacancyId).OrderBy(t => t.OrderId).ToList();
                return Ok(new { data = list, success = true });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetVacancy(int id)
        {
            var queryResult = await Mediator.Send(new GetVacancyQuery { Id = id });

            return Ok(queryResult);
        }

        [HttpPost]
        public async Task<ActionResult> PostVacancy([FromBody] AddVacancyCommand request)
        {
            var result = await Mediator.Send(request);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutVacancy(int id, [FromBody] UpdateVacancyCommand request)
        {
            request.Id = id;
            var result = await Mediator.Send(request);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteVacancyCommand { Id = id });

            return Ok(result);
        }
    }
}