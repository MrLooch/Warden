using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.Core.Domain;
using Warden.DataModel;

namespace Warden.Services
{
    public class SiteService : ISiteService
    {
        private IRepository<Site> siteRepostiry;

        public SiteService(IRepository<Site> siteRepostiry)
        {
            this.siteRepostiry = siteRepostiry;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        public async Task Add(Site site)
        {
            await this.siteRepostiry.AddAsync(site);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public async Task Delete(int id)
        {
            await this.siteRepostiry.RemoveAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Site>> Get()
        {
            return await this.siteRepostiry.GetAllAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        public async Task Update(Site site)
        {
            await Task.FromResult(0);
            //await this.siteRepostiry.UpdateOneAsync()
        }
    }
}
