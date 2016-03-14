using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Warden.DataAccess.EF;
using Microsoft.Data.Entity;
using System.Data.Common;
using Warden.Core.Domain.Authentication;
using Warden.DataAccess.EF.Authentication;
using Warden.Server.Services.Authentication;
using Warden.Server.Services;

namespace Warden
{
    public partial class Startup
    {
        private Serilog.ILogger logger;

        private static string _applicationPath = string.Empty;
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            _applicationPath = appEnv.ApplicationBasePath;
            // Setup configuration sources.

            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add Entity Framework services to the services container.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<WardenContext>(options =>
                options.UseSqlServer(Configuration["Data:WardenConnection:ConnectionString"]));

            //authenticationConfigureServices(services);

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            // Services
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IAccountService, AccountService>();
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
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        private void configLogger(IHostingEnvironment env,
                                  ILoggerFactory loggerFactory)
        {
            var logConsole = new LoggerConfiguration()
              .WriteTo.Console()
              .CreateLogger();

            var appLoc = env.MapPath("APP_DATA").ToString();
            
            var configLogger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Logger(logConsole)
               .WriteTo.RollingFile(appLoc + "/Log-{Date}.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {SourceContext} [{Level}] {Message}{NewLine}{Exception}");

            this.logger = configLogger.CreateLogger();

            loggerFactory.AddSerilog(this.logger);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app,
                             IHostingEnvironment env,
                             ILoggerFactory loggerFactory)
        {

            configLogger(env, loggerFactory);

            app.UseIISPlatformHandler();

            //ConfigureStoreAuthentication(app);

            //Most websites will need static files, but default documents and
            // directory browsing are typically not used.
            app.UseDefaultFiles();
            
            // Add static files to the request pipeline
            app.UseStaticFiles();

            app.UseCookieAuthentication(options =>
            {
                options.AutomaticAuthenticate = true;
                options.AutomaticChallenge = false;
            });

            app.UseMvc(routes =>
            {
                // route1
                routes.MapRoute(
                    name: "Application",
                    template: "{*url}",
                    defaults: new { controller = "Home", action = "Index" }
                );              
            });

            // app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
