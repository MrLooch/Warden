using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.Core.Domain;
using Warden.DataModel;
using Warden.DataModel.Authentication;
using Warden.DataService.Core.Connection;
using Warden.DataService.Core.Repository;
using Warden.Server.Services;

namespace Warden
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ConnectionConfig connectionConfig = new ConnectionConfig()
            {
                DatabaseName = "Warden"
            };
            ObjectConfiguration objectConfig = new ObjectConfiguration();
            objectConfig.mapObjects();

            connectionConfig.connect();
            IRepository<Site> siteRepo = new RepositoryMongoDB<Site>(connectionConfig, "Sites");
            builder
                .Register(c => siteRepo)
                .As<IRepository<Site>>()
                .InstancePerLifetimeScope();

            IRepository<UserRegistration> userRepo = new RepositoryMongoDB<UserRegistration>(connectionConfig, "Users");
            builder
                .Register(c => siteRepo)
                .As<IRepository<Site>>()
                .InstancePerLifetimeScope();

            builder
                .Register(c => new SiteService(siteRepo))
                .As<ISiteService>()
                .InstancePerLifetimeScope();

            builder
                .Register(c => new AccountService(userRepo))
                .As<IAccountService>()
                .InstancePerLifetimeScope();

            //builder.RegisterType<SiteService>()
            //    .WithParameter(
            //        new ResolvedParameter(
            //            (pi, ctx) => pi.ParameterType == typeof(IRepository<Site>),
            //            (pi, ctx) => ctx.Resolve<RepositoryMongoDB<Site>>()));

        }
    }
}
