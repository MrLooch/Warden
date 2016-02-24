using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.DataModel.Authentication;

namespace Warden.Server.Services.Command
{
    public class CreateUserCommand : ICommand<string>
    {
        public UserRegistration UserDetails {get;set;}
    }
}
