
namespace Oqtacore.Rrm.Application.Commands.Users
{
    public class LoginUserCommand : ICommand<LoginUserResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class LoginUserResult
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}