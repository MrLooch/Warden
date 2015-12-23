using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Warden.DataModel;
using Warden.Services;
using Warden.Core.Domain;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Warden.API
{
    [Route("api/[controller]")]
    public class SitesController : Controller
    {
        private static List<Site> sites = null;

        private ISiteService siteService = null;

        public SitesController(ISiteService siteService)
        {
            this.siteService = siteService;           
        }


        /// <summary>
        /// GET: api/sites
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Site>> Get()
        {
            List<Site> sites = new List<Site>();

            try
            {
                sites = await this.siteService.Get();
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

            return sites;
        }

        /// <summary>
        /// Get based by site entity id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            Site site = null;
            try
            {
                site = await this.siteService.GetById(id);
            }
            catch (ArgumentNullException)
            {
                return new HttpNotFoundObjectResult("Not found site");
            }
            return new ObjectResult(site);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Site site)
        {            
            // Check to see if the id is unique?
            await this.siteService.Add(site);
            return new ObjectResult(site);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody]Site site)
        {
            // Check id is unique
            await this.siteService.Update(site);
            return new ObjectResult(site);
        }

        [HttpPut]
        [Route("update/{Guid}")]
        public IActionResult Update(Guid? id, [FromBody]Site site)
        {
            // Check id is unique
            sites.Add(site);
            return new ObjectResult(site);
        }
       
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Check id is unique
            await this.siteService.Delete(id);
            // Return true or false????
            return new ObjectResult(id);
        }
    }
}
