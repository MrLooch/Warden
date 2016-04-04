using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.Core.Domain.Authentication;
using Warden.DataModel.Entities;

namespace Warden.DataAccess.EF.Authentication
{
    public class RoleRepository : EntityBaseRepository<RoleEntity>, IRoleRepository
    {
        public RoleRepository(WardenContext context)
            : base(context)
        { }
    }
}
