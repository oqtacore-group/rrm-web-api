using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Domain.Repository;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Infrastructure.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationContext _dataContext;
        public AdminRepository(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Admin?> GetByEmail(string email)
        {
            var admin = await _dataContext.Admin
                .FirstOrDefaultAsync(user => user.Email == email);

            return admin;
        }
    }
}