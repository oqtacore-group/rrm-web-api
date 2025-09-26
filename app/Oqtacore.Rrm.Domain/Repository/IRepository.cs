
namespace Oqtacore.Rrm.Domain.Repository
{
    public interface IRepository
    {
        public Task Add<T>(T entity);
        public Task Update<T>(T entity);
        public Task Delete<T>(T entity);
    }
}