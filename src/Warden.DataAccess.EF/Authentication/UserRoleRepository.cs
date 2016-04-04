using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.Core.Domain.Authentication;
using Warden.DataModel.Entities;

namespace Warden.DataAccess.EF.Authentication
{
    public class UserRoleRepository : EntityBaseRepository<UserRoleEntity>, IUserRoleRepository
    {
        public UserRoleRepository(WardenContext context)
            : base(context)
        { }
    }
}
