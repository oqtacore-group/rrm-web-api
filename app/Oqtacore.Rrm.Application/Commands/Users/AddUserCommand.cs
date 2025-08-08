using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oqtacore.Rrm.Application.Commands.Users
{
    public class AddUserCommand : ICommand<AddUserResult>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class AddUserResult : Result
    {

    }
}
