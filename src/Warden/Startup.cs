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

namespace Warden
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
