using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Warden.Models;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Warden.API
{
    [Route("api/[controller]")]
    public class SitesController : Controller
    {
        private static List<Site> sites = null; 
        public SitesController()
        {
            if (SitesController.sites == null)
            {
                SitesController.sites = new List<Site>()
                {
                     new Site() { Address = "615 Dandenon Road Armadale 3143 VIC", Name = "Jeppesen" },
                        new Site() { Address = "101 Collin Street Melbourne 3000 VIC", Name = "Telstra" },
                        new Site() { Address = "Hawkers Fishermens Bend 3001 VIC", Name = "Boeing" }
                };
            }
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<Site> Get()
        {
            return sites;
        }
        [HttpPost]
        public IActionResult Post([FromBody]Site site)
        {
            sites.Add(site);
            return new ObjectResult(site);
        }

        [HttpPost]
        [Route("api/sites/add/{id}")]
        public IActionResult Post(int id, [FromBody]Site site)
        {
            sites.Add(site);
            return new ObjectResult(site);
        }

        [HttpPost]
        [Route("addSite")]
        public IActionResult AddSite([FromBody]Site site)
        {
            sites.Add(site);
            return Ok(site);
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
