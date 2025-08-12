using Oqtacore.Rrm.Domain.Models;

namespace Oqtacore.Rrm.Domain.Repository
{
    public interface ICandidateRepository
    {
        Task<Candidate> Get(int id);
        Task<CandidateEvent> GetEvent(int eventId);
        Task ChangeVacancyStatus(CandidatesVacancyStatu candidatesVacancyStatu);
        Task Delete(Candidate candidate);
    }
}