using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Domain.Repository;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Infrastructure.Repository
{
    public class VacancyRepository : IVacancyRepository
    {
        private readonly ApplicationContext _dataContext;
        public VacancyRepository(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Vacancy?> Get(int id)
        {
            var vacancy = await _dataContext.Vacancy.FirstOrDefaultAsync(vacancy => vacancy.id == id);

            return vacancy;
        }
        public async Task<List<Vacancy>> GetByClientId(int clientId)
        {
            var vacancies = await _dataContext.Vacancy
                 .Where(v => v.ClientId == clientId)
                 .ToListAsync();
            
            return vacancies;
        }
        public async Task AddVacancyArchive(VacancyArchive vacancyArchive)
        {
            _dataContext.VacancyArchive.Add(vacancyArchive);

            await _dataContext.SaveChangesAsync();
        }
        public async Task Delete(Vacancy vacancy)
        {
            vacancy.IsDeleted = true;

            await _dataContext.SaveChangesAsync();
        }
        public async Task DeleteRange(List<Vacancy> vacancies)
        {
            vacancies.ForEach(vacancy => vacancy.IsDeleted = true);

            await _dataContext.SaveChangesAsync();
        }
    }
}