using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.Core.Domain;
using Warden.DataModel.Authentication;


namespace Warden.Server.Services
{
    public class AccountService : IAccountService
    {
        private IRepository<UserRegistration> userRepostiry;

        public AccountService(IRepository<UserRegistration> userRepostiry)
        {
            this.userRepostiry = userRepostiry;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserRegistration>> FindByUserName(string name)
        {
            return await this.userRepostiry.FindAsync(s=>s.UserName == name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserRegistration>> FindByEmail(string email)
        {
            return await this.userRepostiry.FindAsync(s => s.Email == email);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public async Task Add(UserRegistration user)
        {
            await this.userRepostiry.AddAsync(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public async Task Delete(Guid id)
        {
            await this.userRepostiry.RemoveAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserRegistration>> Get()
        {
            return await this.userRepostiry.GetAllAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<UserRegistration> GetById(Guid id)
        {
            return await this.userRepostiry.FindByIdAsync(id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        public async Task Update(UserRegistration user)
        {
            await Task.FromResult(0);
            //await this.siteRepostiry.UpdateOneAsync()
        }
    }
}
