using System.ComponentModel.DataAnnotations;

namespace Oqtacore.Rrm.Domain.Models
{
    //Model for Linkedin statistic page
    public class LinkedinStatisticViewModel
    {

    }


    public class EventProfileViewModel
    {
        public CandidateEvent eventData { get; set; }
        public List<Candidate> candidateList { get; set; }
        public List<CandidateEventType> eventTypeList { get; set; }
    }

    //Model For Calendar page
    public class CalendarViewModel
    {
        public List<AllCandidateEventViewModel> eventList { get; set; }
        public List<CandidateEventType> eventTypeList { get; set; }
    }




    //Model For Statistic Page
    public class RecruiterStatisticModel
    {
        public List<RecruiterStatisticViewModel> list { get; set; }
        public List<VacancyStatusType> statusList { get; set; }
        public RecruiterTotalDistinctStat distinctStat { get; set; }
        public RecruiterStatisticModel()
        {
            list = new List<RecruiterStatisticViewModel>();
        }
    }
    public class RecruiterTotalDistinctStat
    {
        public int totalCount { get; set; }
        public int successCount { get; set; }
        public int failCount { get; set; }
    }



    public class LinkedinData
    {
        public List<LinkedinDataMember> included { get; set; }
    }
    public class LinkedinDataMember
    {
        public string type { get; set; }
    }

    public class AddVacancyDataModel
    {
        public int vacancyId { get; set; }
        public string note { get; set; }
    }


    public class ClientStateModel
    {
        public string name { get; set; }
        public int clientCount { get; set; }
    }

    public class ClientStateListModel
    {
        public List<Client> fullList { get; set; }
        public List<Client> listWithoutVacancy { get; set; }
        public List<Client> listWithActiveVacancy { get; set; }
        public List<Client> listWithoutActiveVacancy { get; set; }

    }



    public class Recruiter : Admin
    {
        public bool Current { get; set; }
    }

    public class Client_listModel
    {
        public List<Client> list { get; set; }
    }
    public class Vacancy_listModel
    {
        public List<Vacancy> list { get; set; }
        public List<Client> client_list { get; set; }
    }

    public class Candidate_listModel
    {
        public List<Candidate> list { get; set; }
    }
    public class Candidate_DraglistModel
    {
        public List<CurrentVacancyStatusListViewModel> list { get; set; }
        public Dictionary<int, List<CurrentVacancyStatusListViewModel>> filtered_list { get; set; }

        public Candidate_DraglistModel()
        {
            list = new List<CurrentVacancyStatusListViewModel>();
            filtered_list = new Dictionary<int, List<CurrentVacancyStatusListViewModel>>();
        }
    }

    public class VacancyStatus_listModel
    {
        public List<VacancyStatusListingViewModel> list { get; set; }
    }
    public class AllVacancyStatus_listModel
    {
        public List<AllVacancyStatusListingViewModel> list { get; set; }
    }

    public class ClientContact_listModel
    {
        public List<ClientContact> list { get; set; }
    }


    public class ClientProfileModel
    {
        public Client data { get; set; }
        public List<Vacancy> vacancy_list { get; set; }
    }
    public class CandidateProfileModel
    {
        public Candidate data { get; set; }
        public ContactData contactData { get; set; }
        public List<String> currencyList { get; set; }
        public List<VacancyStatusListViewModel> vacancy_status_state_list { get; set; }
        public List<Vacancy> vacancy_list { get; set; }
        public List<AllCandidateEventViewModel> event_list { get; set; }
        public List<VacancyStatusType> status_list { get; set; }
        public HeadHunterData headHunterData { get; set; }
        public List<HeadHunterJobExperienceData> headHunterJobExperienceDatas { get; set; }
        public ParsedLinkedinCandidateData linkedin_data { get; set; }
        public List<ParsedLinkedinCandidateEducation> linkedin_edu_list { get; set; }
        public List<ParsedLinkedinCandidateResume> linkedin_resume_list { get; set; }
        public List<CandidateFileDataModel> file_list { get; set; }
        public CandidateProfileModel()
        {
            this.data = new Candidate();
            this.contactData = new ContactData();
            this.vacancy_list = null;
            this.vacancy_status_state_list = null;
        }
    }



    public class SearchResult
    {
        public int id { get; set; }
        public string Name { get; set; }
    }




    //Model for diplaying results of search in DB
    public class SearchResultViewModel
    {
        public List<SearchResult> candidateOutput { get; set; }
        public List<SearchResult> clientOutput { get; set; }
        public List<SearchResult> vacancyOutput { get; set; }
        public SearchResultViewModel()
        {
            candidateOutput = new List<SearchResult>();
            clientOutput = new List<SearchResult>();
            vacancyOutput = new List<SearchResult>();
        }
    }

    public class SearchAdvancedResultViewModel
    {
        public List<SearchResult> searchOutput { get; set; }
        public string type { get; set; }
        public SearchAdvancedResultViewModel()
        {
            searchOutput = new List<SearchResult>();

        }
    }
    public class VacancyProfileModel
    {
        public Vacancy data { get; set; }
        public List<String> currencyList { get; set; }
        public List<Client> clientList { get; set; }
    }
    public class ClientContactProfileModel
    {
        public ClientContact data { get; set; }
    }

    public class TestViewModel
    {
        public string ImageUrl { get; set; }
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public int endTime { get; set; }
        public int TotalQuestions { get; set; }
    }

    public class TestStartViewModel
    {
        public string Title { get; set; }
        public string UserId { get; set; }
        public int TimeToTest { get; set; }
    }


    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }



    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
