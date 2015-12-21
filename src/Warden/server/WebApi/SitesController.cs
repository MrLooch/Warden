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
        //[FromServices]
        private ISiteService siteService = null;

        //private ISiteService siteService = null;
        public SitesController(ISiteService siteService)
        {
            this.siteService = siteService;
            if (SitesController.sites == null)
            {
                SitesController.sites = new List<Site>()
                {
                     new Site() { Id = 1, Address = "615 Dandenon Road Armadale 3143 VIC", Name = "Jeppesen" },
                     new Site() { Id = 2, Address = "101 Collin Street Melbourne 3000 VIC", Name = "Telstra" },
                     new Site() { Id = 3, Address = "Hawkers Fishermens Bend 3001 VIC", Name = "Boeing" }
                };
            }
        }



        // GET: api/sites
        [HttpGet]
        public IEnumerable<Site> Get()
        {
            return sites;
        }
        // Get based by site entity id
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            Site site = null;
            try
            {
                site = SitesController.sites.SingleOrDefault(s => s.Id == id);
            }
            catch (ArgumentNullException)
            {
                return new HttpNotFoundObjectResult("Not found site");
            }
            return new ObjectResult(site);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Site site)
        {
            
            // Check to see if the id is unique?
            sites.Add(site);
            return new ObjectResult(site);
        }

        [HttpPost]
        [Route("add/{id}")]
        public IActionResult Post(int id, [FromBody]Site site)
        {
            // Check id is unique
            sites.Add(site);
            return new ObjectResult(site);
        }
       
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // Find site
        }
    }
}
