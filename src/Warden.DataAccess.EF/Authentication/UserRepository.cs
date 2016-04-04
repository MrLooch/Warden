using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.Core.Domain.Authentication;
using Warden.DataModel.Entities;

namespace Warden.DataAccess.EF.Authentication
{
    public class UserRepository : EntityBaseRepository<UserEntity>, IUserRepository
    {
        IRoleRepository _roleReposistory;
        public UserRepository(WardenContext context, IRoleRepository roleReposistory)
            : base(context)
        {
            _roleReposistory = roleReposistory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserEntity GetSingleByEmail(string email)
        {
            return this.GetSingle(x => x.Email == email);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserEntity GetSingleByUsername(string username)
        {
            return this.GetSingle(x => x.Username == username);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetUserRoles(string email)
        {
            List<RoleEntity> _roles = null;

            UserEntity _user = this.GetSingle(u => u.Email == email, u => u.UserRoles);
            if (_user != null)
            {
                _roles = new List<RoleEntity>();
                foreach (var _userRole in _user.UserRoles)
                    _roles.Add(_roleReposistory.GetSingle(_userRole.RoleId));
            }

            return _roles;
        }
    }
}
