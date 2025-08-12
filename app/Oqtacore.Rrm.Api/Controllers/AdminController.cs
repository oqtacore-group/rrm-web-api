using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oqtacore.Rrm.Api.Helpers;
using Oqtacore.Rrm.Api.Models;
using Oqtacore.Rrm.Application.Commands;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Infrastructure.Data;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using TimeZoneConverter;

namespace Oqtacore.Rrm.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationContext thql;
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;
        private readonly IAmazonS3 _s3Client;
        private readonly string bucketName;
        public AdminController(IConfiguration configuration, IAmazonS3 s3Client, ApplicationContext context, IHttpService httpService, IWebHostEnvironment environment)
        {
            thql = context;
            _httpService = httpService;
            _configuration = configuration;
            _s3Client = s3Client;

            string env = EnvironmentManager.GetEnvironmentName(environment);
            bucketName = $"{env}-rrm-candidate-files";
        }

        public string BoolToJsonResponce(bool result)
        {
            var model = new { result = result };
            return JsonConvert.SerializeObject(model);
        }
        public string GetList(string list_name)
        {

            switch (list_name)
            {
                case "client":
                    return GetClientList();
                case "Candidate":
                    return GetCandidateList();
                case "Vacancy":
                    return GetVacancyList();
                default:
                    return null;
            }

        }

        //For left menu only
        public string getRecruiterList()
        {

            List<Admin> model = thql.Admin.Where(t => !t.ServiceAccount && (thql.Vacancy.Any(x => x.CreatedBy == t.id) || thql.CandidatesVacancyStatus.Any(x => x.CreatedBy == t.id))).ToList();

            return JsonConvert.SerializeObject(model);
        }

        
        public string getCurrentRecruiter()
        {
            //Admin model = thql.Admins.Single(t => t.id== activeAdmin.id);
            Admin model = thql.Admin.Single(t => t.Email == _httpService.LogonUserEmail);

            return JsonConvert.SerializeObject(model);
        }


        public string getCurrentRecruiterId()
        {
            var activeAdmin = thql.Admin.FirstOrDefault(t => t.Email == _httpService.LogonUserEmail);

            int model = activeAdmin.id;
            return JsonConvert.SerializeObject(model);
        }

        public string GetClientList()
        {
            Client_listModel model = new Client_listModel();
            model.list = thql.Client.ToList();

            return JsonConvert.SerializeObject(model);

        }

        [HttpGet]
        public string GetVacancyStatusTypeList(int vacancy_id = 0)
        {


            if (vacancy_id == 0 || !thql.Vacancy.Any(t => t.id == vacancy_id))
            {

                AllVacancyStatus_listModel model = new AllVacancyStatus_listModel();
                model.list = thql.AllVacancyStatusListingViewModel.OrderBy(t => t.OrderId).ToList();
                return JsonConvert.SerializeObject(model);
            }
            else
            {
                VacancyStatus_listModel model = new VacancyStatus_listModel();
                model.list = thql.VacancyStatusListingViewModel.Where(t => t.VacancyId == vacancy_id).OrderBy(t => t.OrderId).ToList();
                return JsonConvert.SerializeObject(model);

            }

        }

        [HttpGet]
        public string GetCandidateList(int vacancy_id = 0, int vacancy_status_id = 0)
        {
            Candidate_listModel model = new Candidate_listModel();
            if (vacancy_id == 0 || !thql.Vacancy.Any(t => t.id == vacancy_id))
            {
                if (vacancy_status_id == 0 || !thql.VacancyStatusType.Any(t => t.id == vacancy_status_id))
                {
                    model.list = thql.Candidate.ToList();
                }
                else
                {
                    model.list = thql.Candidate.Where(t => thql.CurrentVacancyStatusListViewModel.Any(x => x.CandidateId == t.id && x.VacancyStatusId == vacancy_status_id)).ToList();
                }

            }
            else
            {
                if (vacancy_status_id == 0 || !thql.VacancyStatusType.Any(t => t.id == vacancy_status_id))
                {
                    model.list = thql.Candidate.Where(t => thql.CurrentVacancyStatusListViewModel.Any(x => x.CandidateId == t.id && x.VacancyId == vacancy_id)).ToList();
                }
                else
                {
                    model.list = thql.Candidate.Where(t => thql.CurrentVacancyStatusListViewModel.Any(x => x.CandidateId == t.id && x.VacancyId == vacancy_id && x.VacancyStatusId == vacancy_status_id)).ToList();
                }
            }


            return JsonConvert.SerializeObject(model);

        }

        public string GetClientStateSummary()
        {
            List<ClientStateModel> model = new List<ClientStateModel>();
            model.Add(new ClientStateModel()
            {
                name = "all",
                clientCount = thql.Client.Count()
            });
            model.Add(new ClientStateModel()
            {
                name = "new",
                clientCount = thql.Client.Where(t => !thql.Vacancy.Any(x => x.ClientId == t.id)).Count()
            });
            model.Add(new ClientStateModel()
            {
                name = "working",
                clientCount = thql.Client.Where(t => thql.Vacancy.Any(x => x.ClientId == t.id && x.Opened)).Count()
            });
            model.Add(new ClientStateModel()
            {
                name = "closed",
                clientCount = thql.Client.Where(t => !thql.Vacancy.Any(x => x.ClientId == t.id && x.Opened) && thql.Vacancy.Any(x => x.ClientId == t.id && !x.Opened)).Count()
            });

            return JsonConvert.SerializeObject(model);
        }

        [HttpGet]
        public string GetClientStateList(string name = "all")
        {
            List<Client> model = new List<Client>();
            switch (name)
            {
                case "all":
                    model = thql.Client.ToList();
                    break;
                case "new":
                    model = thql.Client.Where(t => !thql.Vacancy.Any(x => x.ClientId == t.id)).ToList();
                    break;
                case "working":
                    model = thql.Client.Where(t => thql.Vacancy.Any(x => x.ClientId == t.id && x.Opened)).ToList();
                    break;
                case "closed":
                    model = thql.Client.Where(t => !thql.Vacancy.Any(x => x.ClientId == t.id && x.Opened) && thql.Vacancy.Any(x => x.ClientId == t.id && !x.Opened)).ToList();
                    break;
                default:
                    break;
            }
            //ClientStateListModel model = new ClientStateListModel();
            //model.fullList = thql.Clients.ToList();
            //model.listWithoutVacancy = thql.Clients.Where(t => !thql.Vacancies.Any(x => x.ClientId == t.id)).ToList();
            //model.listWithActiveVacancy = thql.Clients.Where(t => thql.Vacancies.Any(x => x.ClientId == t.id && x.Opened)).ToList();
            //model.listWithActiveVacancy = thql.Clients.Where(t => !thql.Vacancies.Any(x => x.ClientId == t.id && x.Opened) && thql.Vacancies.Any(x => x.ClientId == t.id && !x.Opened)).ToList();
            return JsonConvert.SerializeObject(model);
        }
        [HttpGet]
        public string GetVacancyStateList(int clientid = 0)
        {
            List<VacancyStateListViewModel> model = new List<VacancyStateListViewModel>();

            if (clientid == 0 || !thql.Client.Any(t => t.id == clientid))
            {

                model = thql.VacancyStateListViewModel.ToList();
            }
            else
            {
                model = thql.VacancyStateListViewModel.Where(t => t.ClientId == clientid).ToList();

            }
            return JsonConvert.SerializeObject(model);
        }

        [HttpGet]
        public string GetVacancyList(int candidateId = 0)
        {
            Vacancy_listModel model = new Vacancy_listModel();

            if (candidateId == 0 || !thql.Candidate.Any(t => t.id == candidateId))
            {
                model.list = thql.Vacancy.ToList();
                model.client_list = thql.Client.Where(t => thql.Vacancy.Any(x => x.ClientId == t.id)).ToList();
            }
            else
            {
                model.list = thql.Vacancy.Where(t => !thql.CandidatesVacancyStatus.Any(x => x.CandidateId == candidateId && x.VacancyId == t.id)).ToList();
                model.client_list = thql.Client.Where(t => thql.Vacancy.Any(x => x.ClientId == t.id && !thql.CandidatesVacancyStatus.Any(f => f.CandidateId == candidateId && f.VacancyId == x.id))).ToList();
            }
            return JsonConvert.SerializeObject(model);

        }

        [HttpGet]
        public string GetClientContactList(int id)
        {
            ClientContact_listModel model = new ClientContact_listModel();
            model.list = thql.ClientContact.Where(t => t.ClientId == id).ToList();

            return JsonConvert.SerializeObject(model);

        }

        [HttpGet]
        public string GetClientProfile(int id = 0)
        {
            ClientProfileModel model = new ClientProfileModel();
            if (thql.Client.Any(t => t.id == id))
            {
                model.data = thql.Client.Single(t => t.id == id);
                model.vacancy_list = thql.Vacancy.Where(t => t.ClientId == model.data.id).ToList();
            }
            else
            {
                model.data = new Client();
                model.vacancy_list = new List<Vacancy>();
            }
            return JsonConvert.SerializeObject(model);

        }

        [HttpGet]
        public string GetSettingValue(string settingName)
        {
            if (thql.SiteSetting.Any(t => t.Name == settingName))
            {
                var model = new
                {
                    result = true,
                    stringData = thql.SiteSetting.Single(t => t.Name == settingName).Value
                };
                return JsonConvert.SerializeObject(model);
            }
            else
            {
                var model = new
                {
                    result = false
                };
                return JsonConvert.SerializeObject(model);
            }

        }

        [HttpPost]
        public string GetCandidateProfile(int id)
        {
            var domainUrl = _configuration["DomainUrl"];
            CandidateProfileModel model = new CandidateProfileModel();
            if (thql.Candidate.Any(t => t.id == id))
            {
                model.data = thql.Candidate.Single(t => t.id == id);
                model.contactData = thql.ContactData.Single(t => t.id == model.data.ContactDataId);
                model.vacancy_list = thql.Vacancy.Where(t => thql.CandidatesVacancyStatus.Any(x => x.VacancyId == t.id && x.CandidateId == model.data.id)).ToList();
                model.event_list = thql.AllCandidateEventViewModel.Where(t => t.CandidateId == id).OrderByDescending(t => t.Date).ToList();
                model.vacancy_status_state_list = thql.VacancyStatusListViewModel.Where(t => t.CandidateId == model.data.id).OrderBy(t => t.DateAdded).ToList();

                if (thql.HeadHunterData.Any(t => t.CandidateId == id))
                {
                    model.headHunterData = thql.HeadHunterData.Single(t => t.CandidateId == id);
                    if (thql.HeadHunterJobExperienceData.Any(t => t.HhId == model.headHunterData.id))
                    {
                        model.headHunterJobExperienceDatas = thql.HeadHunterJobExperienceData.Where(t => t.HhId == model.headHunterData.id).OrderByDescending(t => t.id).ToList();
                    }
                }
                if (thql.ParsedLinkedinCandidateData.Any(t => t.CandidateId == id && t.Active))
                {
                    model.linkedin_data = thql.ParsedLinkedinCandidateData.Single(t => t.CandidateId == id && t.Active);
                    if (thql.ParsedLinkedinCandidateEducation.Any(t => t.ParsedId == model.linkedin_data.id))
                    {
                        model.linkedin_edu_list = thql.ParsedLinkedinCandidateEducation.Where(t => t.ParsedId == model.linkedin_data.id).OrderByDescending(t => t.id).ToList();

                    }
                    if (thql.ParsedLinkedinCandidateResume.Any(t => t.ParsedId == model.linkedin_data.id))
                    {
                        model.linkedin_resume_list = thql.ParsedLinkedinCandidateResume.Where(t => t.ParsedId == model.linkedin_data.id).OrderByDescending(t => t.id).ToList();

                    }
                }
                model.file_list =
                    thql.CandidateFile
                    .Where(t => t.candidateId == model.data.id)
                    .Select(x => new CandidateFileDataModel
                    {
                        id = x.id,
                        candidateId = x.candidateId,
                        fileName = x.fileName,
                        fileUrl = $"{domainUrl}/api/CandidateFiles/Download/{x.fileUrl}",
                        DateAdded = x.DateAdded
                    })
                    .ToList();

            }
            model.currencyList = thql.Currency.Select(t => t.CurrencyName).ToList();
            model.status_list = thql.VacancyStatusType.OrderBy(t => t.OrderId).ToList();
            return JsonConvert.SerializeObject(model);

        }

        [HttpPost]
        public string GetVacancyProfile(int id = 0)
        {
            VacancyProfileModel model = new VacancyProfileModel();
            if (thql.Vacancy.Any(t => t.id == id))
            {
                model.data = thql.Vacancy.Single(t => t.id == id);
            }
            else
            {
                model.data = new Vacancy();

            }
            model.clientList = thql.Client.ToList();
            model.currencyList = thql.Currency.Select(t => t.CurrencyName).ToList();
            return JsonConvert.SerializeObject(model);


        }

        [HttpGet]
        public string GetClientContactProfile(int id, int client_id)
        {
            ClientContactProfileModel model = new ClientContactProfileModel();
            if (thql.ClientContact.Any(t => t.id == id && t.ClientId == client_id))
            {
                model.data = thql.ClientContact.Single(t => t.id == id);
            }
            else
            {
                model.data = new ClientContact();
                //TODO: model.data.ContactData = new ContactData();
                model.data.ClientId = client_id;
            }

            return JsonConvert.SerializeObject(model);

        }

        [HttpGet]
        //Search in DB and return results.
        public string Search(string? search_text)
        {
            SearchResultViewModel model = new SearchResultViewModel();
            if (thql.Candidate.Any(t => t.Name.Contains(search_text)))
            {
                model.candidateOutput = thql.Candidate.Where(t => t.Name.Contains(search_text)).Select(t => new SearchResult() { id = t.id, Name = t.Name }).Distinct().OrderByDescending(t => t.id).Take(5).ToList();
            }
            if (thql.Client.Any(t => t.Name.Contains(search_text)))
            {
                model.clientOutput = thql.Client.Where(t => t.Name.Contains(search_text)).Select(t => new SearchResult() { id = t.id, Name = t.Name }).Distinct().OrderByDescending(t => t.id).Take(5).ToList();
            }
            if (thql.Vacancy.Any(t => t.Name.Contains(search_text)))
            {
                model.vacancyOutput = thql.Vacancy.Where(t => t.Name.Contains(search_text)).Select(t => new SearchResult() { id = t.id, Name = t.Name }).Distinct().OrderByDescending(t => t.id).Take(5).ToList();
            }
            return JsonConvert.SerializeObject(model);
        }

        [HttpGet]
        //Search in concrete place in DB based on type
        public string SearchAdvanced(string search_text, string search_type)
        {
            SearchAdvancedResultViewModel model = new SearchAdvancedResultViewModel();
            model.type = search_type;
            switch (search_type)
            {
                case "candidate":
                    if (thql.Candidate.Any(t => t.Name.Contains(search_text)))
                    {
                        model.searchOutput = thql.Candidate.Where(t => t.Name.Contains(search_text)).Select(t => new SearchResult() { id = t.id, Name = t.Name }).Distinct().OrderByDescending(t => t.id).Take(5).ToList();
                    }
                    break;
                case "client":
                    if (thql.Client.Any(t => t.Name.Contains(search_text)))
                    {
                        model.searchOutput = thql.Client.Where(t => t.Name.Contains(search_text)).Select(t => new SearchResult() { id = t.id, Name = t.Name }).Distinct().OrderByDescending(t => t.id).Take(5).ToList();
                    }
                    break;
                case "vacancy":
                    if (thql.Vacancy.Any(t => t.Name.Contains(search_text)))
                    {
                        model.searchOutput = thql.Vacancy.Where(t => t.Name.Contains(search_text)).Select(t => new SearchResult() { id = t.id, Name = t.Name }).Distinct().OrderByDescending(t => t.id).Take(5).ToList();
                    }
                    break;
                default:
                    break;

            }
            return JsonConvert.SerializeObject(model);
        }

        [HttpPost]
        public string ChangeVacancyState(bool open, int vacancy_id)
        {
            if (thql.Vacancy.Any(t => t.id == vacancy_id))
            {
                thql.Vacancy.Single(t => t.id == vacancy_id).Opened = open;
                thql.SaveChanges();
                return BoolToJsonResponce(true);
            }
            return BoolToJsonResponce(false);
        }



        [HttpPost]
        public async Task<string> deleteFileToCandidate(int candidateid, int file_id)
        {
            if (thql.CandidateFile.Any(t => t.candidateId == candidateid && t.id == file_id))
            {
                CandidateFile file_to_delete = await thql.CandidateFile.SingleAsync(t => t.candidateId == candidateid && t.id == file_id);
                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = file_to_delete.fileUrl
                };

                await _s3Client.DeleteObjectAsync(deleteObjectRequest);

                thql.CandidateFile.Remove(file_to_delete);
                thql.SaveChanges();

                return BoolToJsonResponce(true);
            }
            return BoolToJsonResponce(false);
        }

        public class addFileToCandidateInputData
        {
            public int candidateid { get; set; }
            public IFormFile file { get; set; }
            public string? _file_name { get; set; }
        }

        [HttpPost]
        public async Task<string> addFileToCandidate([FromForm] addFileToCandidateInputData input)
        {
            if (input.file != null && input.file.Length < 100000000)
            {
                string fileName = Path.GetFileName(input.file.FileName).Replace(" ", "");
                string newFileName = Guid.NewGuid().ToString() + "_" + fileName;

                using (var newMemoryStream = new MemoryStream())
                {
                    input.file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = newFileName,
                        BucketName = bucketName,
                    };

                    var fileTransferUtility = new TransferUtility(_s3Client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }

                string resultUrl = $"{_configuration["DomainUrl"]}/api/CandidateFiles/Download/{newFileName}";

                CandidateFile new_file = new CandidateFile()
                {
                    candidateId = input.candidateid,
                    DateAdded = DateTime.UtcNow,
                    fileName = input._file_name,
                    fileUrl = newFileName

                };
                thql.CandidateFile.Add(new_file);
                thql.SaveChanges();
                var model = new
                {
                    result = true,
                    id = new_file.id,
                    stringData = resultUrl
                };
                return JsonConvert.SerializeObject(model);
            }
            return BoolToJsonResponce(false);
        }

        [HttpPost]
        public string editFavoriteState(int candidateId, bool favorite)
        {
            if (thql.Candidate.Any(t => t.id == candidateId && t.favorite != favorite))
            {
                thql.Candidate.Single(t => t.id == candidateId).favorite = favorite;
                thql.SaveChanges();
            }

            return BoolToJsonResponce(false);
        }

        public class editCandidateProfileInputData
        {
            public int id { get; set; }
            public CandidateProfileModel _data { get; set; }
            public AddVacancyDataModel[] vacancy_data { get; set; }
            public string resumeLink { get; set; }
            public IFormFile file { get; set; }
            public string file_name { get; set; }
        }

        [HttpPost]
        public async Task<string> editCandidateProfile([FromForm] IFormCollection form)
        {
            var input = new editCandidateProfileInputData
            {
                id = int.Parse(form["id"]),
                _data = JsonConvert.DeserializeObject<CandidateProfileModel>(form["_data"]),
                vacancy_data = JsonConvert.DeserializeObject<AddVacancyDataModel[]>(form["vacancy_data"]),
                resumeLink = form["resumeLink"],
                file = form.Files["file"],
                file_name = form["file_name"]
            };
            var activeAdmin = thql.Admin.FirstOrDefault(t => t.Email == _httpService.LogonUserEmail);

            //CandidateProfileModel data = JsonConvert.DeserializeObject<CandidateProfileModel>(dataString);
            //List<AddVacancyDataModel> vacancy_data = JsonConvert.DeserializeObject<List<AddVacancyDataModel>>(vacancyString);
            if (input.id == 0)
            {
                //means this is new Client profile
                input._data.contactData.CreatedBy = activeAdmin.id;
                thql.ContactData.Add(input._data.contactData);
                thql.SaveChanges();
                input._data.data.ContactDataId = input._data.contactData.id;
                input._data.data.CreatedBy = activeAdmin.id;
                thql.Candidate.Add(input._data.data);
                thql.SaveChanges();
                if (input.vacancy_data != null)
                {
                    foreach (AddVacancyDataModel vacancy in input.vacancy_data)
                    {
                        addCandidateToVacancy(new addCandidateToVacancyInputData()
                        {
                            candidateId = input._data.data.id,
                            note = vacancy.note,
                            vacancyId = vacancy.vacancyId
                        });
                    }
                }
                if (input.resumeLink != null && input.resumeLink != "")
                {
                    ParseHeadHunterProfile(input._data.data.id, input.resumeLink);
                }

                if (input.file != null && input.file.Length < 100000000)
                {
                    string fileName = Path.GetFileName(input.file.FileName).Replace(" ", "");
                    string newFileName = Guid.NewGuid().ToString() + "_" + fileName;

                    using (var newMemoryStream = new MemoryStream())
                    {
                        input.file.CopyTo(newMemoryStream);

                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            InputStream = newMemoryStream,
                            Key = newFileName,
                            BucketName = bucketName,
                        };

                        var fileTransferUtility = new TransferUtility(_s3Client);
                        await fileTransferUtility.UploadAsync(uploadRequest);
                    }

                    var resultUrl = $"{_configuration["DomainUrl"]}/api/CandidateFiles/Download/{newFileName}";
                    var new_file = new CandidateFile
                    {
                        candidateId = input._data.data.id,
                        DateAdded = DateTime.UtcNow,
                        fileName = input.file_name,
                        fileUrl = newFileName
                    };
                    thql.CandidateFile.Add(new_file);
                    thql.SaveChanges();
                }

                var model = new
                {
                    result = true,
                    candidateId = input._data.data.id
                };

                return JsonConvert.SerializeObject(model);
            }
            else
            {
                if (thql.Candidate.Any(t => t.id == input.id))
                {
                    Candidate currentProfile = thql.Candidate.Single(t => t.id == input.id);
                    CandidateArchive candidateArchive = new CandidateArchive(currentProfile, activeAdmin, "edit");
                    currentProfile.editProfile(input._data.data);
                    ContactData contactData = thql.ContactData.Single(t => t.id == currentProfile.ContactDataId);
                    contactData.editContactData(input._data.contactData);
                    thql.CandidateArchive.Add(candidateArchive);
                    thql.SaveChanges();
                    if (input.vacancy_data != null)
                    {
                        foreach (AddVacancyDataModel vacancy in input.vacancy_data)
                        {
                            addCandidateToVacancy(new addCandidateToVacancyInputData()
                            {
                                candidateId = currentProfile.id,
                                note = vacancy.note,
                                vacancyId = vacancy.vacancyId
                            });
                        }
                    }
                    if (input.resumeLink != null && input.resumeLink != "" && !thql.HeadHunterData.Any(t => t.CandidateId == input._data.data.id && t.ProfileUrl == input.resumeLink))
                    {
                        ParseHeadHunterProfile(input._data.data.id, input.resumeLink);
                    }
                    return BoolToJsonResponce(true);
                }
                //else that just invalid request
            }



            return BoolToJsonResponce(false);
        }
        public class editClientContactsInputData
        {
            public int client_id { get; set; }
            public int id { get; set; }
            public ClientContact data { get; set; }
            public ContactData? contactData { get; set; }
        }

        [HttpPost]
        public string editClientContacts([FromBody] editClientContactsInputData input)
        {
            var activeAdmin = thql.Admin.FirstOrDefault(t => t.Email == _httpService.LogonUserEmail);

            if (!thql.Client.Any(t => t.id == input.client_id))
            {
                return BoolToJsonResponce(false);
            }
            if (input.id == 0)
            {
                //means this is new Client profile
                input.contactData.CreatedBy = activeAdmin.id;
                thql.ContactData.Add(input.contactData);
                thql.SaveChanges();
                input.data.CreatedBy = activeAdmin.id;
                input.data.ContactDataId = input.contactData.id;

                input.data.ClientId = input.client_id;
                thql.ClientContact.Add(input.data);
                thql.SaveChanges();

                return BoolToJsonResponce(true);
            }
            else
            {
                if (thql.ClientContact.Any(t => t.id == input.id))
                {
                    ClientContact currentContact = thql.ClientContact.Single(t => t.id == input.id);
                    currentContact.editContactData(input.data);
                    ContactData curentContactData = thql.ContactData.Single(t => t.id == currentContact.ContactDataId);
                    curentContactData.editContactData(input.contactData);
                    thql.SaveChanges();
                    return BoolToJsonResponce(true);
                }
                //else that just invalid request
            }



            return BoolToJsonResponce(false);
        }


        public class addCandidateToVacancyInputData
        {
            public int vacancyId { get; set; }
            public int candidateId { get; set; }
            public string? note { get; set; }
        }


        [HttpPost]
        public string addCandidateToVacancy([FromBody] addCandidateToVacancyInputData input)
        {
            if (thql.Vacancy.Any(t => t.id == input.vacancyId) && thql.Candidate.Any(t => t.id == input.candidateId) &&
                !thql.CandidatesVacancyStatus.Any(t => t.VacancyId == input.vacancyId && t.CandidateId == input.candidateId))
            {
                var activeAdmin = thql.Admin.FirstOrDefault(t => t.Email == _httpService.LogonUserEmail);

                var newConnect = new CandidatesVacancyStatu()
                {
                    CandidateId = input.candidateId,
                    DateAdded = DateTime.UtcNow,
                    Note = input.note,
                    VacancyId = input.vacancyId,
                    VacancyStatusId = thql.VacancyStatusType.Single(t => t.OrderId == 1).id,
                    CreatedBy = activeAdmin.id

                };

                thql.CandidatesVacancyStatus.Add(newConnect);
                thql.SaveChanges();
                return BoolToJsonResponce(true);
            }

            return BoolToJsonResponce(false);
        }

        [HttpPost]
        public string editCandidateCommentToVacancy([FromBody] addCandidateToVacancyInputData input)
        {
            if (thql.Vacancy.Any(t => t.id == input.vacancyId) && thql.Candidate.Any(t => t.id == input.candidateId))
            {
                CandidatesVacancyComment newComment = new CandidatesVacancyComment()
                {
                    CandidateId = input.candidateId,
                    DateAdded = DateTime.UtcNow,
                    Note = input.note,
                    VacancyId = input.vacancyId,


                };
                thql.CandidatesVacancyComment.Add(newComment);
                thql.SaveChanges();
                return BoolToJsonResponce(true);
            }

            return BoolToJsonResponce(false);
        }


        public class getRecruiterStatisticInputData
        {
            public int recruiterId { get; set; }
            public DateTime startTime { get; set; }
            public DateTime endTime { get; set; }
        }


        [HttpPost]
        //Return Data For Statistic pages
        public string getRecruiterStatistic([FromBody] getRecruiterStatisticInputData input)
        {
            RecruiterStatisticModel model = new RecruiterStatisticModel();
            if (input.recruiterId == 0)
            {
                model.list = thql.RecruiterStatisticViewModel.Where(t => t.DateAdded >= input.startTime && t.DateAdded <= input.endTime).ToList();
            }
            else if (thql.Admin.Any(t => t.id == input.recruiterId))
            {
                model.list = thql.RecruiterStatisticViewModel.Where(t => t.CreatedBy == input.recruiterId && t.DateAdded >= input.startTime && t.DateAdded <= input.endTime).ToList();
            }
            model.statusList = thql.VacancyStatusType.OrderBy(t => t.OrderId).ToList();
            model.distinctStat = new RecruiterTotalDistinctStat()
            {
                totalCount = model.list.Select(t => new { t.VacancyId, t.CandidateId }).Distinct().Count(),
                failCount = model.list.Where(t => t.Fail == true).Select(t => new { t.VacancyId, t.CandidateId }).Distinct().Count(),
                successCount = model.list.Where(t => t.Success == true).Select(t => new { t.VacancyId, t.CandidateId }).Distinct().Count()
            };
            return JsonConvert.SerializeObject(model);
        }

        [HttpGet]
        public string getEventData(int id)
        {
            EventProfileViewModel model = new EventProfileViewModel();
            if (thql.CandidateEvent.Any(t => t.id == id))
            {
                model.eventData = thql.CandidateEvent.Single(t => t.id == id);
            }
            else
            {
                model.eventData = new CandidateEvent();
            }
            model.candidateList = thql.Candidate.ToList();
            model.eventTypeList = thql.CandidateEventType.ToList();
            return JsonConvert.SerializeObject(model);
        }

        public class editCandidateEventInputData
        {
            public CandidateEvent event_data { get; set; }
        }

        [HttpPost]
        public string editCandidateEvent([FromBody] editCandidateEventInputData input)
        {
            if (input.event_data.ZoomLink == null || (!input.event_data.ZoomLink.Contains("zoom.us") && input.event_data.ZoomLink != "NA"))
            {
                var model = new { result = false, error = "Нужно заполнить ссылку на Zoom или поставить NA" };
                return JsonConvert.SerializeObject(model);
            }
            var activeAdmin = thql.Admin.FirstOrDefault(t => t.Email == _httpService.LogonUserEmail);

            if (thql.CandidateEvent.Any(t => t.id == input.event_data.id))
            {
                CandidateEvent edit_event = thql.CandidateEvent.Single(t => t.id == input.event_data.id);
                edit_event.CreatedBy = activeAdmin.id;
                edit_event.HashCode = Guid.NewGuid().ToString();
                edit_event.editEvent(input.event_data);
                thql.SaveChanges();
                return BoolToJsonResponce(true);
            }
            else if (input.event_data.id == 0)
            {
                input.event_data.CreatedBy = activeAdmin.id;
                input.event_data.Completed = false;
                input.event_data.Date = input.event_data.Date.ToUniversalTime();
                input.event_data.HashCode = Guid.NewGuid().ToString();
                thql.CandidateEvent.Add(input.event_data);
                thql.SaveChanges();
                var model = new { result = "true", id = input.event_data.id };
                return JsonConvert.SerializeObject(model);
            }

            return BoolToJsonResponce(false);
        }


        public class getCalendarDataOnputData
        {
            public DateTime? startTime { get; set; }
            public DateTime? endTime { get; set; }
        }

        [HttpPost]
        public string getCalendarData([FromBody] getCalendarDataOnputData input)
        {
            CalendarViewModel model = new CalendarViewModel();
            model.eventList = thql.AllCandidateEventViewModel/*.Where(t=>t.Date>=startTime && t.Date<=endTime)*/.ToList();
            model.eventTypeList = thql.CandidateEventType.ToList();
            foreach (AllCandidateEventViewModel row in model.eventList)
            {
                if (row.EventType != "rrm")
                {

                    row.Date = TimeZoneInfo.ConvertTimeFromUtc(row.Date.Value, TZConvert.GetTimeZoneInfo(TZConvert.IanaToWindows("Europe/Moscow")));
                }
            }
            return JsonConvert.SerializeObject(model);
        }

        [HttpPost]
        //Delete ClientContact
        public string DeleteClientContact(int id)
        {
            if (thql.ClientContact.Any(t => t.id == id))
            {
                thql.ClientContact.Remove(thql.ClientContact.Single(t => t.id == id));
                thql.SaveChanges();
                return BoolToJsonResponce(true);
            }
            return BoolToJsonResponce(false);
        }


        [HttpPost]
        //Delete VacancyCandidate Connection by id
        public string DeleteVacancyCandidateConnection(int id)
        {
            if (thql.CandidatesVacancyStatus.Any(t => t.id == id))
            {
                thql.CandidatesVacancyStatus.Remove(thql.CandidatesVacancyStatus.Single(t => t.id == id));
                thql.SaveChanges();
                return BoolToJsonResponce(true);
            }
            return BoolToJsonResponce(false);
        }


        [HttpPost]
        public string DeleteCandidateRelocationCity(int id)
        {
            if (thql.CandidateRelocationCity.Any(t => t.id == id))
            {
                thql.CandidateRelocationCity.Remove(thql.CandidateRelocationCity.Single(t => t.id == id));
                thql.SaveChanges();
                return BoolToJsonResponce(true);
            }
            return BoolToJsonResponce(false);
        }

        [HttpPost]
        public string DeleteResumeExperience(int id)
        {
            if (thql.ResumeExperience.Any(t => t.id == id))
            {
                thql.ResumeExperience.Remove(thql.ResumeExperience.Single(t => t.id == id));
                thql.SaveChanges();
                return BoolToJsonResponce(true);
            }
            return BoolToJsonResponce(false);
        }

        [HttpPost]
        public string DeleteCandidateEvent(int id)
        {
            if (thql.CandidateEvent.Any(t => t.id == id))
            {
                thql.CandidateEvent.Remove(thql.CandidateEvent.Single(t => t.id == id));
                thql.SaveChanges();
                return BoolToJsonResponce(true);
            }
            return BoolToJsonResponce(false);
        }

        #region HeadHunter Parsing


        //Function for parsing headhunter user profile
        [HttpPost]
        public string ParseHeadHunterProfile(int candidateId, string resumeLink)
        {

            string pattern = @"\p{IsCyrillic}";
            var activeAdmin = thql.Admin.FirstOrDefault(t => t.Email == _httpService.LogonUserEmail);

            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                try
                {

                    string htmlCode = client.DownloadString(resumeLink);
                    int startIndex = htmlCode.IndexOf("\"authUrl\":");
                    string result = htmlCode.Substring(startIndex);
                    result = "{" + result.Substring(0, result.IndexOf("</div>"));
                    if (result.Contains("</template>"))
                    {
                        int templateIndex = result.IndexOf("</template>");
                        result = result.Substring(0, templateIndex);
                    }
                    //String[] parsedHtml = htmlCode.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    //string result = parsedHtml.Single(t => t.Contains("ReviewArray"));

                    var resultJson = JsonConvert.DeserializeObject<HeadHunterProfile>(result);
                    //TODO Json-> HeadhunterUserInfo
                    HeadHunterData userInfo = new HeadHunterData()
                    {

                    };
                    userInfo.Position = ParseHelper.ValueLenghtParse(HttpUtility.UrlDecode((resultJson.resume["title"].HasValues) ? (resultJson.resume["title"]["value"].ToString()) : "No data"), 100);
                    userInfo.Location = HttpUtility.UrlDecode((resultJson.resume["area"].HasValues) ? (resultJson.resume["area"]["value"]["title"].ToString()) : "No data");
                    userInfo.Location = ParseHelper.ValueLenghtParse(userInfo.Location + "," + HttpUtility.UrlDecode((resultJson.resume["citizenship"].HasValues) ? (resultJson.resume["citizenship"]["value"][0]["title"].ToString()) : "No data"), 250);

                    userInfo.About = ParseHelper.ValueLenghtParse(HttpUtility.UrlDecode((resultJson.resume["skills"].HasValues) ? (resultJson.resume["skills"]["value"].ToString()) : "No data"), 500);
                    userInfo.Employment = ParseHelper.ValueLenghtParse(HttpUtility.UrlDecode((resultJson.resume["employment"].HasValues) ? (resultJson.resume["employment"]["value"][0]["string"].ToString()) : "No data"), 25);

                    userInfo.Relocation = ParseHelper.ValueLenghtParse(HttpUtility.UrlDecode((resultJson.resume["relocation"].HasValues) ? (resultJson.resume["relocation"]["value"].ToString()) : "No data"), 25);
                    //userInfo.ResumeData = HttpUtility.UrlDecode((resultJson.resume["experience"].HasValues) ? (resultJson.resume["experience"]["value"].ToString()) : "No data");
                    userInfo.totalExperience = ParseHelper.ValueLenghtParse(HttpUtility.UrlDecode((resultJson.resume["totalExperience"].HasValues) ? (resultJson.resume["totalExperience"]["years"].ToString() + " years " + resultJson.resume["totalExperience"]["months"].ToString() + " month") : "No experience"), 100);
                    List<String> userSkills = new List<string>();
                    foreach (JToken token in resultJson.resume["keySkills"]["value"])
                    {
                        userSkills.Add(token["string"].ToString());
                    }
                    userInfo.CreatedBy = activeAdmin.id;
                    userInfo.CandidateId = candidateId;
                    userInfo.ProfileUrl = ParseHelper.ValueLenghtParse(resumeLink, 250);
                    userInfo.Skills = ParseHelper.ValueLenghtParse(String.Join(", ", userSkills.Where(t => Regex.Matches(t, pattern).Count == 0)), 250);

                    if (thql.HeadHunterData.Any(t => t.CandidateId == candidateId))
                    {
                        HeadHunterData currentData = thql.HeadHunterData.Single(t => t.CandidateId == candidateId);
                        currentData.edit(userInfo);
                        userInfo.id = currentData.id;
                    }
                    else
                    {
                        thql.HeadHunterData.Add(userInfo);
                    }
                    thql.SaveChanges();

                    List<HeadHunterJobExperienceData> userResume = new List<HeadHunterJobExperienceData>();
                    foreach (JToken token in resultJson.resume["experience"]["value"])
                    {
                        HeadHunterJobExperienceData jobExperienceData = JsonConvert.DeserializeObject<HeadHunterJobExperienceData>(token.ToString());
                        jobExperienceData.HhId = userInfo.id;
                        jobExperienceData.parseAllFields();
                        userResume.Add(jobExperienceData);
                    }
                    if (thql.HeadHunterJobExperienceData.Any(t => t.HhId == userInfo.id))
                    {
                        thql.HeadHunterJobExperienceData.RemoveRange(thql.HeadHunterJobExperienceData.Where(t => t.HhId == userInfo.id));
                    }
                    thql.HeadHunterJobExperienceData.AddRange(userResume);
                    thql.SaveChanges();
                    return BoolToJsonResponce(true);
                }
                catch (Exception exception)
                {
                    //thql.HeadhunterUsers.Single(t => t.id == _user.id).invalid = true;
                    //thql.SaveChanges();
                    return BoolToJsonResponce(false);
                }
                return BoolToJsonResponce(false);
            }


        }


        public class ParseHeadHunterProfilePluginInput
        {
            public string? data { get; set; }
            public int candidateId { get; set; }
            public string? resumeLink { get; set; }

        }

        [HttpPost]
        public string ParseHeadHunterProfilePlugin([FromBody] ParseHeadHunterProfilePluginInput model)
        {
            var activeAdmin = thql.Admin.FirstOrDefault(t => t.Email == _httpService.LogonUserEmail);

            string pattern = @"\p{IsCyrillic}";



            int startIndex = model.data.IndexOf("\"authUrl\":");
            string result = model.data.Substring(startIndex);
            result = "{" + result;

            var resultJson = JsonConvert.DeserializeObject<HeadHunterProfile>(result);
            //TODO Json-> HeadhunterUserInfo
            HeadHunterData userInfo = new HeadHunterData()
            {

            };
            userInfo.Position = ParseHelper.ValueLenghtParse(HttpUtility.UrlDecode((resultJson.resume["title"].HasValues) ? (resultJson.resume["title"]["value"].ToString()) : "No data"), 100);
            userInfo.Location = HttpUtility.UrlDecode((resultJson.resume["area"].HasValues) ? (resultJson.resume["area"]["value"]["title"].ToString()) : "No data");
            userInfo.Location = ParseHelper.ValueLenghtParse(userInfo.Location + "," + HttpUtility.UrlDecode((resultJson.resume["citizenship"].HasValues) ? (resultJson.resume["citizenship"]["value"][0]["title"].ToString()) : "No data"), 250);

            userInfo.About = ParseHelper.ValueLenghtParse(HttpUtility.UrlDecode((resultJson.resume["skills"].HasValues) ? (resultJson.resume["skills"]["value"].ToString()) : "No data"), 500);
            userInfo.Employment = ParseHelper.ValueLenghtParse(HttpUtility.UrlDecode((resultJson.resume["employment"].HasValues) ? (resultJson.resume["employment"]["value"][0]["string"].ToString()) : "No data"), 25);

            userInfo.Relocation = ParseHelper.ValueLenghtParse(HttpUtility.UrlDecode((resultJson.resume["relocation"].HasValues) ? (resultJson.resume["relocation"]["value"].ToString()) : "No data"), 25);
            //userInfo.ResumeData = HttpUtility.UrlDecode((resultJson.resume["experience"].HasValues) ? (resultJson.resume["experience"]["value"].ToString()) : "No data");
            userInfo.totalExperience = ParseHelper.ValueLenghtParse(HttpUtility.UrlDecode((resultJson.resume["totalExperience"].HasValues) ? (resultJson.resume["totalExperience"]["years"].ToString() + " years " + resultJson.resume["totalExperience"]["months"].ToString() + " month") : "No experience"), 100);
            List<String> userSkills = new List<string>();
            foreach (JToken token in resultJson.resume["keySkills"]["value"])
            {
                userSkills.Add(token["string"].ToString());
            }
            userInfo.CreatedBy = activeAdmin.id;
            userInfo.CandidateId = model.candidateId;
            userInfo.ProfileUrl = ParseHelper.ValueLenghtParse(model.resumeLink, 250);
            userInfo.Skills = ParseHelper.ValueLenghtParse(String.Join(", ", userSkills.Where(t => Regex.Matches(t, pattern).Count == 0)), 250);

            if (thql.HeadHunterData.Any(t => t.CandidateId == model.candidateId))
            {
                HeadHunterData currentData = thql.HeadHunterData.Single(t => t.CandidateId == model.candidateId);
                currentData.edit(userInfo);
                userInfo.id = currentData.id;
            }
            else
            {
                thql.HeadHunterData.Add(userInfo);
            }
            thql.SaveChanges();

            List<HeadHunterJobExperienceData> userResume = new List<HeadHunterJobExperienceData>();
            foreach (JToken token in resultJson.resume["experience"]["value"])
            {
                HeadHunterJobExperienceData jobExperienceData = JsonConvert.DeserializeObject<HeadHunterJobExperienceData>(token.ToString());
                jobExperienceData.HhId = userInfo.id;
                jobExperienceData.parseAllFields();
                jobExperienceData.id = 0;
                userResume.Add(jobExperienceData);
            }
            if (thql.HeadHunterJobExperienceData.Any(t => t.HhId == userInfo.id))
            {
                thql.HeadHunterJobExperienceData.RemoveRange(thql.HeadHunterJobExperienceData.Where(t => t.HhId == userInfo.id));
            }
            thql.HeadHunterJobExperienceData.AddRange(userResume);
            thql.SaveChanges();
            return BoolToJsonResponce(true);
        }


        [HttpPost]
        public string ClearHeadHunter(int id)
        {
            if (thql.HeadHunterData.Any(t => t.CandidateId == id))
            {
                HeadHunterData currentData = thql.HeadHunterData.Single(t => t.CandidateId == id);
                List<HeadHunterJobExperienceData> jobData = thql.HeadHunterJobExperienceData.Where(t => t.HhId == currentData.id).ToList();
                if (jobData.Count > 0)
                {
                    thql.HeadHunterJobExperienceData.RemoveRange(jobData);
                }
                //thql.HeadHunterData.Remove(currentData);
                thql.SaveChanges();
                thql.HeadHunterData.Remove(currentData);
                thql.SaveChanges();
            }
            return BoolToJsonResponce(true);
        }

        private void LogError(string error)
        {

            thql.Logs.Add(new Log()
            {
                Date = DateTime.UtcNow,
                Text = error
            });
            thql.SaveChanges();

        }

        //[DisableCors]
        [HttpPost]
        public void ParseLinkedin(string data, int candidateId, string url)
        {
            try
            {
                JToken parsedData = JsonConvert.DeserializeObject<JObject>(data);
                //LogError("TestPlugin json content included serilaized," + JsonConvert.SerializeObject(parsedData["included"][0]));
                ParsedLinkedinCandidateData profileData = new ParsedLinkedinCandidateData();
                profileData.CandidateId = candidateId;
                profileData.DateParsed = DateTime.UtcNow.Date;
                profileData.Active = true;
                profileData.Profile_url = url;
                List<ParsedLinkedinCandidateResume> resumeData = new List<ParsedLinkedinCandidateResume>();
                List<ParsedLinkedinCandidateEducation> educationData = new List<ParsedLinkedinCandidateEducation>();
                LogError("TestPlugin json  data," + parsedData["included"].ToString());
                for (int i = 0; i < (parsedData["included"]).Count(); i++)
                {
                    if (parsedData["included"][i]["$type"] != null)
                    {
                        switch (parsedData["included"][i]["$type"].ToString())
                        {
                            case "com.linkedin.voyager.dash.identity.profile.Position":
                                resumeData.Add(new ParsedLinkedinCandidateResume(parsedData["included"][i]));
                                break;

                            case "com.linkedin.voyager.dash.identity.profile.Skill":
                                profileData.ParseSkillsData(parsedData["included"][i]);
                                break;
                            case "com.linkedin.voyager.dash.identity.profile.Profile":
                                LogError("TestPlugin json profile data," + parsedData["included"][i].ToString());
                                profileData.ParseProfileData(parsedData["included"][i]);
                                break;
                            case "com.linkedin.voyager.dash.identity.profile.Language":
                                profileData.ParseLanguageData(parsedData["included"][i]);
                                break;
                            case "com.linkedin.voyager.dash.identity.profile.Education":
                                educationData.Add(new ParsedLinkedinCandidateEducation(parsedData["included"][i]));
                                break;
                            case "com.linkedin.voyager.dash.common.Industry":
                                profileData.ParseIndustryData(parsedData["included"][i]);
                                break;
                        }
                    }
                }

                if (thql.ParsedLinkedinCandidateData.Any(t => t.CandidateId == profileData.CandidateId && t.Active == true))
                {
                    foreach (ParsedLinkedinCandidateData dataRow in thql.ParsedLinkedinCandidateData.Where(t => t.CandidateId == profileData.CandidateId && t.Active == true).ToList())
                    {
                        dataRow.Active = false;
                    }
                }
                thql.ParsedLinkedinCandidateData.Add(profileData);
                thql.SaveChanges();
                foreach (ParsedLinkedinCandidateResume resume in resumeData)
                {
                    resume.ParsedId = profileData.id;
                }
                foreach (ParsedLinkedinCandidateEducation educ in educationData)
                {
                    educ.ParsedId = profileData.id;
                }
                thql.ParsedLinkedinCandidateEducation.AddRange(educationData);
                thql.ParsedLinkedinCandidateResume.AddRange(resumeData);
                thql.SaveChanges();

                //foreach (JToken member in (JArray)parsedData["included"])
                //{
                //    LogError("TestPlugin json content," + member["type"].ToString());
                //}

            }
            catch (Exception ex)
            {
                LogError("TestPlugin error," + Convert.ToString(ex));
                return;
            }
        }


        [HttpPost]
        public string ClearLinkedin(int id)
        {
            if (thql.ParsedLinkedinCandidateData.Any(t => t.CandidateId == id))
            {
                List<ParsedLinkedinCandidateData> currentDataList = thql.ParsedLinkedinCandidateData.Where(t => t.CandidateId == id).ToList();
                foreach (ParsedLinkedinCandidateData currentData in currentDataList)
                {
                    List<ParsedLinkedinCandidateResume> resume = thql.ParsedLinkedinCandidateResume.Where(t => t.ParsedId == currentData.id).ToList();
                    if (resume.Count > 0)
                    {
                        thql.ParsedLinkedinCandidateResume.RemoveRange(resume);
                    }
                    List<ParsedLinkedinCandidateEducation> edu = thql.ParsedLinkedinCandidateEducation.Where(t => t.ParsedId == currentData.id).ToList();
                    if (edu.Count > 0)
                    {
                        thql.ParsedLinkedinCandidateEducation.RemoveRange(edu);
                    }

                }
                thql.SaveChanges();
                foreach (ParsedLinkedinCandidateData currentData in currentDataList)
                {
                    thql.ParsedLinkedinCandidateData.Remove(currentData);
                }
                thql.SaveChanges();
            }
            return BoolToJsonResponce(true);
        }
        #endregion
    }

}