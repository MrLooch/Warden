using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Warden.Core.Domain.Authentication;
using Warden.DataModel.Entities;
using Warden.Infrastructure.Core;

namespace Warden.Server.Services.Authentication
{
    public class MembershipService : IMembershipService
    {
        #region Variables
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IEncryptionService _encryptionService;
        #endregion
        public MembershipService(IUserRepository userRepository, IRoleRepository roleRepository,
        IUserRoleRepository userRoleRepository, IEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _encryptionService = encryptionService;
        }

        #region IMembershipService Implementation

        public MembershipContext ValidateUser(string email,
                                              string password)
        {
            var membershipCtx = new MembershipContext();

            var user = _userRepository.GetSingleByEmail(email);
            if (user != null && isUserValid(user, password))
            {
                var userRoles = GetUserRoles( email);
                membershipCtx.User = user;

                var identity = new GenericIdentity(user.Username);
                membershipCtx.Principal = new GenericPrincipal(
                    identity,
                    userRoles.Select(x => x.Name).ToArray());
            }

            return membershipCtx;
        }
        public UserEntity CreateUser(string username, string email, string password, Guid[] roles)
        {
            var existingUser = _userRepository.GetSingleByEmail(email);

            if (existingUser != null)
            {
                throw new Exception("Username is already in use");
            }

            var passwordSalt = _encryptionService.CreateSalt();

            var user = new UserEntity()
            {
                Username = username,
                Salt = passwordSalt,
                Email = email,
                IsLocked = false,
                HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt),
                DateCreated = DateTime.Now
            };

            _userRepository.Add(user);

            _userRepository.Commit();

            if (roles != null || roles.Length > 0)
            {
                foreach (var role in roles)
                {
                    addUserToRole(user, role);
                }
            }

            _userRepository.Commit();

            return user;
        }

        public UserEntity GetUser(Guid userId)
        {
            return _userRepository.GetSingle(userId);
        }

        public List<RoleEntity> GetUserRoles(string email)
        {
            List<RoleEntity> _result = new List<RoleEntity>();

            var existingUser = _userRepository.GetSingleByEmail(email);

            if (existingUser != null)
            {
                foreach (var userRole in existingUser.UserRoles)
                {
                    _result.Add(userRole.Role);
                }
            }

            return _result.Distinct().ToList();
        }
        #endregion

        #region Helper methods
        private void addUserToRole(UserEntity user, Guid roleId)
        {
            var role = _roleRepository.GetSingle(roleId);
            if (role == null)
                throw new Exception("Role doesn't exist.");

            var userRole = new UserRoleEntity()
            {
                RoleId = role.Id,
                UserId = user.Id
            };
            _userRoleRepository.Add(userRole);

            _userRepository.Commit();
        }

        private bool isPasswordValid(UserEntity user, string password)
        {
            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }

        private bool isUserValid(UserEntity user, string password)
        {
            if (isPasswordValid(user, password))
            {
                return !user.IsLocked;
            }

            return false;
        }
        #endregion
    }
}
