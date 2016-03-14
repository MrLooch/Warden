using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Warden.Core.Domain.Authentication;
using Warden.DataModel.Authentication;
using Warden.DataModel.Entities;
using Warden.Infrastructure.Core;
using Warden.Server.Services.Authentication;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Http.Authentication;

namespace Warden.Server.Services
{
    public class AccountService : IAccountService
    {        
        private readonly IMembershipService membershipService;
        private readonly IUserRepository userRepository;

        public AccountService(IMembershipService membershipService,
                              IUserRepository userRepository)
        {
            this.membershipService = membershipService;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserEntity registereUser(UserRegistrationDTO registration)
        {
            return this.membershipService.CreateUser(registration.UserName, registration.Email, registration.Password, new Guid[] { Guid.NewGuid() });            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<UserEntity> login(UserLoginDTO user,
                                            AuthenticationManager authenticationManager)
        {

            MembershipContext userContext = this.membershipService.ValidateUser(user.Email, user.Password);

            if (userContext.User != null)
            {
                IEnumerable<RoleEntity> roles = this.userRepository.GetUserRoles(user.Email);
                List<Claim> claims = new List<Claim>();
                foreach (RoleEntity role in roles)
                {
                    Claim claim = new Claim(ClaimTypes.Role, "Admin", ClaimValueTypes.String, user.Email);
                    claims.Add(claim);
                }
                
                await authenticationManager.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)),
                    new Microsoft.AspNet.Http.Authentication.AuthenticationProperties { IsPersistent = user.RememberMe });
                
            }

            return userContext.User;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserEntity>> FindByUserName(string name)
        {
            return await userRepository.FindByAsync(s => s.Username == name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserEntity>> FindByEmail(string email)
        {
            return await this.userRepository.FindByAsync(s => s.Email == email);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void Add(UserEntity user)
        {
            this.userRepository.Add(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(UserEntity user)
        {
            this.userRepository.Delete(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserEntity>> Get()
        {
            return await this.userRepository.GetAllAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserEntity>> GetById(Guid id)
        {
            return await this.userRepository.FindByAsync(s=>s.Id == id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        public void Update(UserEntity user)
        {
            this.userRepository.Edit(user);
        }       
    }
}
