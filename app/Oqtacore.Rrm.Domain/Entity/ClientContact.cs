
namespace Oqtacore.Rrm.Domain.Entity
{
    
    public class ClientContact
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ContactDataId { get; set; }
        public int? CreatedBy { get; set; }
    }
}