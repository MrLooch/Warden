using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.Server.Services.Command;
using Warden.DataModel.Authentication;
using Warden.DataModel.Entities;

namespace Warden.Server.Services.CommandHandler
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand,string>
    {
        private IAccountService accountService;
        
        public CreateUserCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private async Task<string> Validate(CreateUserCommand command)
        {
            string errorMessage = "";
            IEnumerable<UserEntity> users = await this.accountService.FindByUserName(command.UserDetails.UserName);
            if (users.Count() > 0)
            {
                errorMessage = "User name " + command.UserDetails.UserName + " already exists.";
            }

            return errorMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        public async Task<string> Handle(CreateUserCommand command)
        {
            string errorMessage = await Validate(command);

            if (String.IsNullOrEmpty(errorMessage))
            {
                accountService.registereUser(command.UserDetails);
            }
            return errorMessage;
        }
    }
}
