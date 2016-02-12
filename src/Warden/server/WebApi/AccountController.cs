using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Microsoft.AspNet.Mvc.ModelBinding;
using Warden.DataModel.Authentication;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Routing;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Warden.server.WebApi
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
      

        // POST api/values
        [HttpPost]
        [Route("Register")]
        public IActionResult Post([FromBody]UserRegistration registration)
        {
            if (!ModelState.IsValid)
            {
                //return HttpBadRequest.HttpUnauthorized;
                return HttpBadRequest();
            }

            // Validation
            // Check user name has not been taken

            // Bad request with list of errors??

            // TODO: Set new id
            //registration.Id = 
            return new HttpOkResult();
        }       
    }
}
