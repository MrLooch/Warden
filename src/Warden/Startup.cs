using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Warden.Services;
using Warden.Core.Domain;
using Warden.DataModel;
using Warden.DataService.Core.Repository;
using Autofac;
using Warden.DataService.Core.Connection;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Authentication.Cookies;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Builder;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Builder.Extensions;


namespace Warden
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {            
            services.AddMvc();
            //services.AddAuthentication()
            // Create the Autofac container builder.
            var builder = new ContainerBuilder();
            
            // Add any Autofac modules or registrations.
            builder.RegisterModule(new AutofacModule());
            
            // Populate the services.            
            builder.Populate(services);
            
            // Build the container.
            var container = builder.Build();
            
            // Resolve and return the service provider.            
            return container.ResolveOptional<IServiceProvider>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOwin(IApplicationBuilder app)
        {
            
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    LoginPath = new PathString("Account/LogOn")
            //});
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            ConfigureOwin(app);

            // Add cookie-based authentication to the request pipeline.
            //app.UseIdentity();

            app.UseIISPlatformHandler();
            //Most websites will need static files, but default documents and
            // directory browsing are typically not used.
            // Default files
            app.UseDefaultFiles();
            
            // Add static files to the request pipeline
            app.UseStaticFiles();

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
