using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warden.DataModel.Entities
{
    public class UserRoleEntity : EntityBase
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public virtual RoleEntity Role { get; set; }
    }
}
