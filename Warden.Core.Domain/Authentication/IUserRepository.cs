using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.Core.Domain.Repository;
using Warden.DataModel.Entities;

namespace Warden.Core.Domain.Authentication
{
    public interface IUserRepository : IEntityBaseRepository<UserEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        UserEntity GetSingleByUsername(string username);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        UserEntity GetSingleByEmail(string email);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        IEnumerable<RoleEntity> GetUserRoles(string emaiul);
    }
}
