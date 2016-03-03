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
using Warden.Server.Services;
using Warden.Server.Services.CommandHandler;
using Warden.Server.Services.Command;
using Microsoft.AspNet.Authorization;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Warden.server.WebApi
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private IAccountService accountService;
        private readonly ILogger logger;

        public AccountController(IAccountService accountService,
                                 ILogger<AccountController> logger)
        {
            this.accountService = accountService;
            this.logger = logger;
        }

        // POST api/values
        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Post([FromBody]UserRegistration registration)
        {
            this.logger.LogVerbose("Registeration for {0}", registration.Email);

            if (!ModelState.IsValid)
            {
                //return HttpBadRequest.HttpUnauthorized;
                return HttpBadRequest();
            }

            try
            {
                // Create user and do validation
                // Check user name has not been taken
                CreateUserCommandHandler cmdHandler = new CreateUserCommandHandler(this.accountService);
                string errorMessage = await cmdHandler.Handle(
                    new CreateUserCommand() { UserDetails = registration });
                if (!String.IsNullOrEmpty(errorMessage))
                {
                    ModelState.AddModelError("UserName", errorMessage);
                    return new BadRequestObjectResult(ModelState);
                }
            }
            catch (Exception)
            {

            }
            // TODO: Set new id            
            return new HttpOkResult();
        }

        // POST api/values
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Post([FromBody]UserLogin loginDetails)
        {
            if (!ModelState.IsValid)
            {
                //return HttpBadRequest.HttpUnauthorized;
                return HttpBadRequest();
            }

            try
            {
                var userRegistration = await accountService.FindByEmail(loginDetails.Email);

                if (userRegistration.Count() != 1)
                {
                    ModelState.AddModelError("Email", "Duplicate email accounts");
                    return new BadRequestObjectResult(ModelState);
                }
                else
                {
                    var user = userRegistration.First();
                    if (user.Password != loginDetails.Password)
                    {
                        ModelState.AddModelError("Password", "Incorrect passowrd");
                        return new BadRequestObjectResult(ModelState);
                    }
                }
            }
            catch (Exception)
            {

            }
            // TODO: Set new id            
            return new HttpOkResult();
        }
    }
}
