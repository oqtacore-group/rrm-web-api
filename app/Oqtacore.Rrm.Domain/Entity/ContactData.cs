
namespace Oqtacore.Rrm.Domain.Entity
{
    public class ContactData
    {
        public int Id { get; set; }
        public string? Linkedin { get; set; }
        public string? Vkontakte { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SecondPhoneNumber { get; set; }
        public string? Telegram { get; set; }
        public string? Skype { get; set; }
        public string? Email { get; set; }
        public string? Location { get; set; }
        public int? CreatedBy { get; set; }
    }
}
