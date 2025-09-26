using System;

namespace Oqtacore.Rrm.Application.Commands.Candidates
{
    public class UpdateCandidateCommand : ICommand<UpdateCandidateResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? profileUrl { get; set; }
        public int ContactDataId { get; set; }
        public string? Note { get; set; }
        public string? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? RelocationReady { get; set; }
        public bool? RemoteWorkplaceReady { get; set; }
        public int? SalaryWish { get; set; }
        public string? SalaryCurrency { get; set; }
        public string? PhotoUrl { get; set; }
        public string? MainSkill { get; set; }
        public bool? Leader { get; set; }
        public string? HowKnowAboutVacancy { get; set; }
        public int? CreatedBy { get; set; }
        public string? HHurl { get; set; }
        public string? lastjob_position { get; set; }
        public string? lastjob_company { get; set; }
        public string? salary { get; set; }
        public string? resume_text { get; set; }
        public bool? favorite { get; set; }
    }
    public class UpdateCandidateResult : Result
    {
    }
}
