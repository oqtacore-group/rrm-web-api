using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oqtacore.Rrm.Domain.Models
{

    [Table("CandidateEventType")]
    [MetadataType(typeof(CandidateEventTypeMetadata))]
    public partial class CandidateEventType
    {
        internal sealed class CandidateEventTypeMetadata
        {
            [JsonIgnore]
            public ICollection<CandidateEvent> CandidateEvents { get; set; }

        }
    }

    [Table("CandidateEvent")]
    [MetadataType(typeof(CandidateEventMetadata))]
    public partial class CandidateEvent
    {
        public void editEvent(CandidateEvent new_event)
        {
            this.CandidateId = new_event.CandidateId;
            this.Caption = new_event.Caption;
            this.Date = new_event.Date.ToUniversalTime();
            this.TypeId = new_event.TypeId;
            this.ZoomLink = new_event.ZoomLink;
        }


        internal sealed class CandidateEventMetadata
        {
            [JsonIgnore]

            public Candidate Candidate { get; set; }
            [JsonIgnore]
            public CandidateEventType CandidateEventType { get; set; }
            [JsonIgnore]
            public Admin Admin { get; set; }

        }
    }



    [Table("ParsedLinkedinCandidateEducation")]
    [MetadataType(typeof(ParsedLinkedinCandidateEducationMetadata))]
    public partial class ParsedLinkedinCandidateEducation
    {

        internal sealed class ParsedLinkedinCandidateEducationMetadata
        {
            [JsonIgnore]
            public ParsedLinkedinCandidateData ParsedLinkedinCandidateData { get; set; }

        }
        public ParsedLinkedinCandidateEducation()
        {

        }
        public ParsedLinkedinCandidateEducation(JToken data)
        {

            if (data["degreeName"] != null)
            {
                this.degreeName = data["degreeName"].ToString();
            }
            if (data["fieldOfStudy"] != null)
            {
                this.fieldOfStudy = data["fieldOfStudy"].ToString();
            }
            if (data["grade"] != null)
            {
                this.grade = data["grade"].ToString();
            }
            if (data["schoolName"] != null)
            {
                this.schoolName = data["schoolName"].ToString();
            }


            if (data["dateRange"] != null)
            {
                if (data["dateRange"]["start"] != null && data["dateRange"]["start"]["year"] != null)
                {
                    this.YearStart = Convert.ToInt32(data["dateRange"]["start"]["year"].ToString());
                }
                if (data["dateRange"]["end"] != null && data["dateRange"]["end"]["year"] != null)
                {
                    this.YearEnd = Convert.ToInt32(data["dateRange"]["end"]["year"].ToString());
                }
            }


        }
    }
    [Table("ParsedLinkedinCandidateResume")]
    [MetadataType(typeof(ParsedLinkedinCandidateResumeMetadata))]
    public partial class ParsedLinkedinCandidateResume
    {

        internal sealed class ParsedLinkedinCandidateResumeMetadata
        {
            [JsonIgnore]
            public ParsedLinkedinCandidateData ParsedLinkedinCandidateData { get; set; }

        }
        public ParsedLinkedinCandidateResume()
        {

        }
        public ParsedLinkedinCandidateResume(JToken data)
        {
            if (data["locationName"] != null)
            {
                this.LocationName = data["locationName"].ToString();
            }
            if (data["title"] != null)
            {
                this.Title = data["title"].ToString();
            }
            if (data["companyName"] != null)
            {
                this.CompanyName = data["companyName"].ToString();
            }
            if (data["dateRange"] != null)
            {
                if (data["dateRange"]["start"] != null && data["dateRange"]["start"]["year"] != null && data["dateRange"]["start"]["month"] != null)
                {
                    this.StartDate = new DateTime(Convert.ToInt32(data["dateRange"]["start"]["year"].ToString()), Convert.ToInt32(data["dateRange"]["start"]["month"].ToString()), 1);
                }
                if (data["dateRange"]["end"] != null && data["dateRange"]["end"]["year"] != null && data["dateRange"]["end"]["month"] != null)
                {
                    this.EndDate = new DateTime(Convert.ToInt32(data["dateRange"]["end"]["year"].ToString()), Convert.ToInt32(data["dateRange"]["end"]["month"].ToString()), 1);
                }
            }


        }
    }
    [Table("ParsedLinkedinCandidateData")]
    [MetadataType(typeof(ParsedLinkedinCandidateDataMetadata))]
    public partial class ParsedLinkedinCandidateData
    {

        internal sealed class ParsedLinkedinCandidateDataMetadata
        {
            [JsonIgnore]
            public Candidate Candidate { get; set; }
            [JsonIgnore]
            public ICollection<ParsedLinkedinCandidateEducation> ParsedLinkedinCandidateEducations { get; set; }
            [JsonIgnore]
            public ICollection<ParsedLinkedinCandidateResume> ParsedLinkedinCandidateResumes { get; set; }

        }
        public void ParseLanguageData(JToken data)
        {
            if (data["name"] != null && data["proficiency"] != null)
            {
                if (this.LanguageSummary == null || this.LanguageSummary == "")
                {
                    this.LanguageSummary = data["name"].ToString() + "/" + data["proficiency"].ToString();
                }
                else
                {
                    this.LanguageSummary += ", " + data["name"].ToString() + "/" + data["proficiency"].ToString();
                }
            }
        }
        public void ParseSkillsData(JToken data)
        {
            if (data["name"] != null)
            {
                if (this.SkillSummary == null || this.SkillSummary == "")
                {
                    this.SkillSummary = data["name"].ToString();
                }
                else
                {
                    this.SkillSummary += ", " + data["name"].ToString();
                }
            }
        }
        public void ParseProfileData(JToken data)
        {
            this.DateParsed = DateTime.UtcNow.Date;
            try
            {
                if (data["summary"] != null)
                {
                    this.About = data["summary"].ToString();
                }
                if (data["birthDateOn"] != null && data["birthDateOn"].HasValues)
                {
                    this.DateBirth = data["birthDateOn"]["day"].ToString() + "/" + data["birthDateOn"]["month"].ToString();
                }
                if (data["firstName"] != null)
                {
                    this.FirstName = data["firstName"].ToString();
                }
                if (data["lastName"] != null)
                {
                    this.LastName = data["lastName"].ToString();
                }
                if (data["locationName"] != null)
                {
                    this.LocationName = data["locationName"].ToString();
                }
                if (data["headline"] != null)
                {
                    this.Position = data["headline"].ToString();
                }
            }
            catch (Exception ex)
            {
                this.Position = Convert.ToString(ex);
            }
        }
        public void ParseIndustryData(JToken data)
        {
            if (data["name"] != null)
            {
                if (this.IndustrySummary == null || this.IndustrySummary == "")
                {
                    this.IndustrySummary = data["name"].ToString();
                }
                else
                {
                    this.IndustrySummary += ", " + data["name"].ToString();
                }
            }
        }
    }


    [Table("Admin")]
    [MetadataType(typeof(AdminTypeMetadata))]
    public partial class Admin


    {
        internal sealed class AdminTypeMetadata
        {
            [JsonIgnore]

            public ICollection<Candidate> Candidates { get; set; }
            [JsonIgnore]
            public ICollection<CandidateArchive> CandidateArchives { get; set; }
            [JsonIgnore]
            public ICollection<CandidateArchive> CandidateArchives1 { get; set; }
            [JsonIgnore]
            public ICollection<CandidatesVacancyStatu> CandidatesVacancyStatus { get; set; }
            [JsonIgnore]
            public ICollection<Client> Clients { get; set; }
            [JsonIgnore]
            public ICollection<ClientArchive> ClientArchives { get; set; }
            [JsonIgnore]
            public ICollection<ClientArchive> ClientArchives1 { get; set; }
            [JsonIgnore]
            public ICollection<ClientContact> ClientContacts { get; set; }
            [JsonIgnore]
            public ICollection<ContactData> ContactDatas { get; set; }
            [JsonIgnore]
            public ICollection<Vacancy> Vacancies { get; set; }
            [JsonIgnore]
            public ICollection<VacancyArchive> VacancyArchives { get; set; }
            [JsonIgnore]
            public ICollection<VacancyArchive> VacancyArchives1 { get; set; }

        }

    }
    [Table("VacancyStatusType")]
    [MetadataType(typeof(VacancyStatusTypeMetadata))]
    public partial class VacancyStatusType
    {
        internal sealed class VacancyStatusTypeMetadata
        {
            [JsonIgnore]
            public ICollection<CandidatesVacancyStatu> CandidatesVacancyStatus { get; set; }

        }

    }

    [Table("HeadHunterData")]
    [MetadataType(typeof(HeadHunterDataMeta))]
    public partial class HeadHunterData
    {
        public void edit(HeadHunterData _copy)
        {
            this.About = _copy.About;
            this.CandidateId = _copy.CandidateId;
            this.CreatedBy = _copy.CreatedBy;
            this.Employment = _copy.Employment;
            this.Location = _copy.Employment;
            this.Position = _copy.Position;
            this.ProfileUrl = _copy.ProfileUrl;
            this.Relocation = _copy.Relocation;
            this.Skills = _copy.Skills;
            this.totalExperience = _copy.totalExperience;
        }
        internal sealed class HeadHunterDataMeta
        {
            [JsonIgnore]
            public ICollection<HeadHunterJobExperienceData> HeadHunterJobExperienceDatas { get; set; }
            [JsonIgnore]
            public Candidate Candidate { get; set; }
            [JsonIgnore]
            public Admin Admin { get; set; }


        }
    }
    public partial class HeadHunterJobExperienceData
    {
        //Fix all lenght problems if there are any
        public void parseAllFields()
        {
            this.companyAreaTitle = this.companyAreaTitle != null ? ParseHelper.ValueLenghtParse(this.companyAreaTitle, 100) : null;
            this.companyName = this.companyName != null ? ParseHelper.ValueLenghtParse(this.companyName, 100) : null;
            this.description = this.description != null ? ParseHelper.ValueLenghtParse(this.description, 300) : null;
            this.endData = this.endData != null ? ParseHelper.ValueLenghtParse(this.endData, 100) : null;
            this.endDate = this.endDate != null ? ParseHelper.ValueLenghtParse(this.endDate, 100) : "current time";
            this.position = this.position != null ? ParseHelper.ValueLenghtParse(this.position, 100) : null;
            this.startData = this.startData != null ? ParseHelper.ValueLenghtParse(this.startData, 100) : null;
            this.startDate = this.startDate != null ? ParseHelper.ValueLenghtParse(this.startDate, 100) : null;

        }

    }


    [Table("CandidateFile")]
    [MetadataType(typeof(CandidateFileMetadata))]
    public partial class CandidateFile
    {
        internal sealed class CandidateFileMetadata
        {

            [JsonIgnore]
            public Candidate Candidate { get; set; }

        }
    }

    [Table("ClientContact")]
    [MetadataType(typeof(ClientContactMetadata))]
    public partial class ClientContact
    {
        public void editContactData(ClientContact edited)
        {
            this.Email = edited.Email;
            this.Name = edited.Name;
            this.PhoneNumber = edited.PhoneNumber;
            this.ClientId = edited.ClientId;
            this.ContactDataId = edited.ContactDataId;


        }
        internal sealed class ClientContactMetadata
        {

        }

    }
    [Table("ContactData")]
    [MetadataType(typeof(ContactDataMetadata))]
    public partial class ContactData
    {
        public void editContactData(ContactData edited)
        {
            this.Email = edited.Email;
            this.PhoneNumber = edited.PhoneNumber;
            this.Linkedin = edited.Linkedin;
            this.Location = edited.Location;
            this.PhoneNumber = edited.PhoneNumber;
            this.SecondPhoneNumber = edited.SecondPhoneNumber;
            this.Skype = edited.Skype;
            this.Telegram = edited.Telegram;
            this.Vkontakte = edited.Vkontakte;


        }
        internal sealed class ContactDataMetadata
        {
            [JsonIgnore]
            public ICollection<ClientContact> ClientContacts { get; set; }
        }

    }
}
