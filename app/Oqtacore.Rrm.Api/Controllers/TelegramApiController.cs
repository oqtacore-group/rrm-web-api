using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Oqtacore.Rrm.Api.Models;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TelegramApiController : ControllerBase
    {

        private readonly ApplicationContext thql;
        private readonly IConfiguration _configuration;
        Admin activeAdmin;
        public TelegramApiController(IConfiguration configuration, ApplicationContext context)
        {
            thql = context;
            _configuration = configuration;
        }

        public class ChangeStatusViewmodel
        {
            public Vacancy vacancy { get; set; }

            public List<VacancyStatusType> status_list { get; set; }

            public CandidatesVacancyStatu vacancyStatus { get; set; }

            public CandidateEvent candEvent { get; set; }
            public Candidate candidate { get; set; }
        }

        public class ChangeStatusInputModel
        {
        }

        public string GetChangeStatusData(string? hash)
        {


            if (hash == null)
            {
                return null;
            }
            ChangeStatusViewmodel model = new ChangeStatusViewmodel();
            model.candEvent = thql.CandidateEvent.SingleOrDefault(t => t.HashCode == hash);
            if (model.candEvent == null)
            {
                return null;
            }
            model.vacancyStatus = thql.CandidatesVacancyStatus.Where(t => t.CandidateId == model.candEvent.CandidateId && t.VacancyStatusId != 9 && t.DateAdded < model.candEvent.Date).OrderByDescending(t => t.DateAdded).FirstOrDefault();
            if (model.vacancyStatus == null)
            {
                return null;
            }
            model.vacancy = model.vacancyStatus != null ? thql.Vacancy.SingleOrDefault(t => t.id == model.vacancyStatus.VacancyId) : null;
            if (model.vacancy == null)
            {
                return null;
            }
            model.candidate = thql.Candidate.SingleOrDefault(t => t.id == model.candEvent.CandidateId);
            model.status_list = thql.VacancyStatusType.OrderBy(t => t.OrderId).ToList();
            return JsonConvert.SerializeObject(model);

        }

        public class postChangeStatusModel
        {
            public int vacancyId { get; set; }
            public int candidateId { get; set; }
            public int statusid { get; set; }
            public string hash { get; set; }
            public string note { get; set; }
        }

        public String postChangeStatus([FromBody] postChangeStatusModel model)
        {
            if (thql.Vacancy.Any(t => t.id == model.vacancyId) && thql.Candidate.Any(t => t.id == model.candidateId) &&
                thql.CandidateEvent.Any(t => t.CandidateId == model.candidateId && t.HashCode == model.hash))
            {
                CandidatesVacancyStatu newConnect = new CandidatesVacancyStatu()
                {
                    CandidateId = model.candidateId,
                    DateAdded = DateTime.UtcNow,
                    Note = model.note,
                    VacancyId = model.vacancyId,
                    VacancyStatusId = model.statusid,
                    CreatedBy = 18//Service account for TelegramBot
                };
                CandidateEvent eventt = thql.CandidateEvent.SingleOrDefault(t => t.HashCode == model.hash && t.CandidateId == model.candidateId);
                eventt.HashCode = Guid.NewGuid().ToString();//To reset ability to change vacancy status
                thql.CandidatesVacancyStatus.Add(newConnect);
                thql.SaveChanges();
                var modelGoodResult = new { result = true };
                return JsonConvert.SerializeObject(modelGoodResult);
            }
            var modelResult = new { result = false, error = "Нужно заполнить ссылку на Zoom или поставить NA" };
            return JsonConvert.SerializeObject(modelResult);
        }
    }
}