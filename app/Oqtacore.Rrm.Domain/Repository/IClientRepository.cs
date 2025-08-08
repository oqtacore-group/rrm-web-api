using Oqtacore.Rrm.Domain.Models;

namespace Oqtacore.Rrm.Domain.Repository
{
    public interface IClientRepository
    {
        Task<Client> Get(int id);
        Task<ClientContact> GetContact(int id);
        Task<List<ClientContact>> GetContacts(int clientId);
        Task DeleteContacts(int clientId);

        Task Delete(Client client);
        Task AddClientArchive(ClientArchive clientArchive);
    }
}