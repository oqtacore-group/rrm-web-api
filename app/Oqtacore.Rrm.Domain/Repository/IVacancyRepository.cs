using Oqtacore.Rrm.Domain.Models;

namespace Oqtacore.Rrm.Domain.Repository
{
    public interface IVacancyRepository
    {
        Task<Vacancy> Get(int id);
        Task<List<Vacancy>> GetByClientId(int clientId);
        Task AddVacancyArchive(VacancyArchive vacancyArchive);
        Task Delete(Vacancy vacancy);
        Task DeleteRange(List<Vacancy> vacancies);
    }
}