using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Domain.Repository;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Infrastructure.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationContext _dataContext;
        public ClientRepository(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Client?> Get(int id)
        {
            var client = await _dataContext.Client.FirstOrDefaultAsync(client => client.id == id);

            return client;
        }
        public async Task<ClientContact?> GetContact(int id)
        {
            var clientContact = await _dataContext.ClientContact.FirstOrDefaultAsync(x => x.id == id);

            return clientContact;
        }
        public async Task<List<ClientContact>> GetContacts(int clientId)
        {
            var clientContacts = await _dataContext.ClientContact
                .Where(x => x.ClientId == clientId)
                .ToListAsync();

            return clientContacts;
        }
        public async Task AddClientArchive(ClientArchive clientArchive)
        {
            _dataContext.ClientArchive.Add(clientArchive);

            await _dataContext.SaveChangesAsync();
        }

        public async Task Delete(Client client)
        {
            client.IsDeleted = true;

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteContacts(int clientId)
        {
            var clientContacts = await GetContacts(clientId);
            _dataContext.ClientContact.RemoveRange(clientContacts);
         
            await _dataContext.SaveChangesAsync();
        }
    }
}