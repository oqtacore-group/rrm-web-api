using Oqtacore.Rrm.Domain.Repository;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Infrastructure.Repository
{
    public class GenericRepository : IRepository
    {
        private readonly ApplicationContext _dataContext;
        public GenericRepository(ApplicationContext dataContext) => _dataContext = dataContext;
        public async Task Add<T>(T entity)
        {
            _dataContext.Add(entity);

            await _dataContext.SaveChangesAsync();
        }
        public async Task Update<T>(T entity)
        {
            _dataContext.Update(entity);

            await _dataContext.SaveChangesAsync();
        }
        public async Task Delete<T>(T entity)
        {
            _dataContext.Remove(entity);

            await _dataContext.SaveChangesAsync();
        }
    }
}