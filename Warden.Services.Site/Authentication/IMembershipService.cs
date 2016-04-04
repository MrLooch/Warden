using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.DataModel.Entities;
using Warden.Infrastructure.Core;

namespace Warden.Server.Services.Authentication
{
    public interface IMembershipService
    {
        MembershipContext ValidateUser(string email,string password);
        UserEntity CreateUser(string username, string email, string password, Guid[] roles);
        UserEntity GetUser(Guid userId);
        List<RoleEntity> GetUserRoles(string email);
    }
}
