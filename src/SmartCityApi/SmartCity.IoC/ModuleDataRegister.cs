using Autofac;
using SmartCity.Data.Infrastructure;
using SmartCity.Data.Repositories;
using SmartCity.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.IoC
{
    public class ModuleDataRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new DbFactory(c.Resolve<AppConfig>().SmartCityConnString)).As<IDbFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<OcorrenciaRepository>().As<IOcorrenciaRepository>().InstancePerLifetimeScope();
            builder.RegisterType<VotoOcorrenciaRepository>().As<IVotoOcorrenciaRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UsuarioRepository>().As<IUsuarioRepository>().InstancePerLifetimeScope();
            
        }
    }
}
