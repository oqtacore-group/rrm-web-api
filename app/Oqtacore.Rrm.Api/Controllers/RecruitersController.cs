using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Application.Commands;
using Oqtacore.Rrm.Application.Queries.Recruiters;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Api.Controllers
{
    [Route("api/[controller]")]
    public class RecruitersController : ApiControllerBase
    {
        private readonly ApplicationContext _dataContext;
        private readonly IHttpService _httpService;
        public RecruitersController(ApplicationContext dataContext, IHttpService httpService, IMediator mediator) : base(mediator)
        {
            _dataContext = dataContext;
            _httpService = httpService;
        }
        [HttpGet]
        public async Task<ActionResult> GetRecruiters()
        {
            var queryResult = await Mediator.Send(new GetRecruiterListQuery());
            
            return Ok(queryResult);
        }
        [HttpGet("Current")]
        public async Task<ActionResult> GetRecruiter()
        {
            var user = _httpService.LogonUserEmail;
            var admin = await _dataContext.Admin.FirstOrDefaultAsync(x => x.Email == _httpService.LogonUserEmail);
            
            return Ok(new { data = admin, success = true });
        }
        [HttpPost("Statistics")]
        public async Task<ActionResult> GetRecruiterStatistics(GetRecruiterStatisticQuery request)
        {
            var recruiters = await Mediator.Send(request);
            
            return Ok(recruiters);
        }
    }
}