using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.Core.Domain.Repository;
using Warden.DataModel.Entities;

namespace Warden.Core.Domain.Authentication
{
    public interface IUserRoleRepository : IEntityBaseRepository<UserRoleEntity> 
    {
    }
}
