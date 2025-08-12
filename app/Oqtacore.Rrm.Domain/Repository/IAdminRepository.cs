using Oqtacore.Rrm.Domain.Models;

namespace Oqtacore.Rrm.Domain.Repository
{
    public interface IAdminRepository
    {
        Task<Admin> GetByEmail(string email);
    }
}
