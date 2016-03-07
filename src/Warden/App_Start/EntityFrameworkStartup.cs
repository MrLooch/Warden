using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.Data.Entity;
using Warden.DataAccess.EF;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNet.Identity;
using Warden.DataModel.Authentication;
using Microsoft.AspNet.Identity.EntityFramework;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using AspNet.Security.OpenIdConnect.Server;
using System.Security.Claims;
using Microsoft.AspNet.Authentication.OpenIdConnect;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Authentication;

namespace Warden
{
    public partial class Startup
    {
        public void authenticationConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework().AddDbContext<WardenContext>();

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //.AddEntityFrameworkStores<WardenContext>()
            //.AddDefaultTokenProviders();

            services.AddAuthentication();
            services.AddCaching();

            services.AddAuthorization
           (
               options =>
               {
                   options.AddPolicy
                   (
                       JwtBearerDefaults.AuthenticationScheme,
                       builder =>
                       {
                           builder.
                           AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).
                           RequireAuthenticatedUser().
                           Build();
                       }
                   );
               }
           );
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public void ConfigureStoreAuthentication(IApplicationBuilder app)
        {
            app.UseJwtBearerAuthentication(options =>
            {
                options.AutomaticAuthenticate = true;
                options.Audience = "http://localhost:25803";
                options.Authority = "http://localhost:25803";
                options.RequireHttpsMetadata = false;
               //options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>
               //(
               //    metadataAddress: options.Authority + ".well-known/openid-configuration",
               //    configRetriever: new OpenIdConnectConfigurationRetriever(),
               //    docRetriever: new HttpDocumentRetriever { RequireHttps = false }
               //);
            });

            app.UseOpenIdConnectServer(configuration =>
            
            {
                configuration.TokenEndpointPath = "/token";
                configuration.AllowInsecureHttp = true;
                configuration.Provider = new OpenIdConnectServerProvider
                {
                    OnValidateClientAuthentication = context =>
                    {
                        context.Skipped();
                        return Task.FromResult<object>(null);
                    },

                    OnGrantResourceOwnerCredentials = context =>
                    {
                        var identity = new ClaimsIdentity(OpenIdConnectDefaults.AuthenticationScheme);
                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "todo"));
                        identity.AddClaim(new Claim("urn:customclaim", "value", "token id_token"));
                        context.Validated(new ClaimsPrincipal(identity));
                        return Task.FromResult<object>(null);
                    }
                };                
            });
            //UserManager<ApplicationUser> userManager = new Microsoft.AspNet.Identity.UserManager<ApplicationUser>();
            //// User a single instance of StoreContext and AppStoreUserManager per request
            //app.(WardenContext.Create);
            //app.CreatePerOwinContext<AppStoreUserManager>(userManager);

            //// Configure the application for OAuth based flow
            //PublicClientId = "self";
            //OAuthOptions = new OAuthAuthorizationServerOptions
            //{
            //    TokenEndpointPath = new PathString("/Token"),
            //    Provider = new ApplicationOAuthProvider(PublicClientId),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(10),
            //    AllowInsecureHttp = true
            //};

            //// Enable the application to use bearer tokens to authenticate users
            //app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}
