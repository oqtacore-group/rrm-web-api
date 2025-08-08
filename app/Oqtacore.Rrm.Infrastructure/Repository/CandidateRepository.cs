using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Domain.Repository;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Infrastructure.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ApplicationContext _dataContext;
        public CandidateRepository(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Candidate?> Get(int id)
        {
            var candidate = await _dataContext.Candidate.FirstOrDefaultAsync(candidate => candidate.id == id);

            return candidate;
        }
        public async Task<CandidateEvent?> GetEvent(int eventId)
        {
            var candidateEvent = await _dataContext.CandidateEvent.FirstOrDefaultAsync(candidateEvent => candidateEvent.id == eventId);

            return candidateEvent;
        }
        public async Task ChangeVacancyStatus(CandidatesVacancyStatu candidatesVacancyStatu)
        {
            _dataContext.Add(candidatesVacancyStatu);

            await _dataContext.SaveChangesAsync();
        }
        public async Task Delete(Candidate candidate)
        {
            candidate.IsDeleted = true;

            await _dataContext.SaveChangesAsync();
        }
    }
}