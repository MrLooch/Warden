using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Warden.DataAccess.EF;
using System.Security.Claims;

namespace Warden
{
    public partial class Startup
    {
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="services"></param>
        //public void authenticationConfigureServices(IServiceCollection services)
        //{
        //    services.AddEntityFramework().AddDbContext<WardenContext>();

        //    //services.AddIdentity<ApplicationUser, IdentityRole>()
        //    //.AddEntityFrameworkStores<WardenContext>()
        //    //.AddDefaultTokenProviders();

        //    services.AddAuthentication();
        //    services.AddCaching();

        //    services.AddAuthorization
        //   (
        //       options =>
        //       {
        //           options.AddPolicy
        //           (
        //               JwtBearerDefaults.AuthenticationScheme,
        //               builder =>
        //               {
        //                   builder.
        //                   AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).
        //                   RequireAuthenticatedUser().
        //                   Build();
        //               }
        //           );
        //       }
        //   );
        //}

        //public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        //public static string PublicClientId { get; private set; }

        //public void ConfigureStoreAuthentication(IApplicationBuilder app)
        //{
        //    app.UseJwtBearerAuthentication(options =>
        //    {
        //        options.AutomaticAuthenticate = true;
        //        options.Audience = "http://localhost:25803";
        //        options.Authority = "http://localhost:25803";
        //        options.RequireHttpsMetadata = false;
        //       //options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>
        //       //(
        //       //    metadataAddress: options.Authority + ".well-known/openid-configuration",
        //       //    configRetriever: new OpenIdConnectConfigurationRetriever(),
        //       //    docRetriever: new HttpDocumentRetriever { RequireHttps = false }
        //       //);
        //    });

        //    app.UseOpenIdConnectServer(configuration =>
            
        //    {
        //        configuration.TokenEndpointPath = "/token";
        //        configuration.AllowInsecureHttp = true;
        //        configuration.Provider = new OpenIdConnectServerProvider
        //        {
        //            OnValidateClientAuthentication = context =>
        //            {
        //                context.Skipped();
        //                return Task.FromResult<object>(null);
        //            },

        //            OnGrantResourceOwnerCredentials = context =>
        //            {
        //                var identity = new ClaimsIdentity(OpenIdConnectDefaults.AuthenticationScheme);
        //                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "todo"));
        //                identity.AddClaim(new Claim("urn:customclaim", "value", "token id_token"));
        //                context.Validated(new ClaimsPrincipal(identity));
        //                return Task.FromResult<object>(null);
        //            }
        //        };                
        //    });         
        //}
    }
}
