using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.Core.Domain;
using Warden.DataModel;
using Warden.DataService.Core.Connection;
using Warden.DataService.Core.Repository;
using Warden.Services;

namespace Warden
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ConnectionConfig connectionConfig = new ConnectionConfig();
            IRepository<Site> siteRepo = new RepositoryMongoDB<Site>(connectionConfig, "Sites");
            builder
                .Register(c => siteRepo)
                .As<IRepository<Site>>()
                .InstancePerLifetimeScope();

            //builder.RegisterType<SiteService>()
            //    .WithParameter(
            //        new ResolvedParameter(
            //            (pi, ctx) => pi.ParameterType == typeof(IRepository<Site>),
            //            (pi, ctx) => ctx.Resolve<RepositoryMongoDB<Site>>()));
            //builder.Build();
            builder
                .Register(c => new SiteService(siteRepo))
                .As<ISiteService>()
                .InstancePerLifetimeScope();

        }
    }
}
