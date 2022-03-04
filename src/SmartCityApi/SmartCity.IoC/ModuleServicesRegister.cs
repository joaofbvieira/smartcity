using Autofac;
using SmartCity.Application.Email;
using SmartCity.Application.Ocorrencias;
using SmartCity.Application.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.IoC
{
    public class ModuleServicesRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OcorrenciaService>().As<IOcorrenciaService>().InstancePerLifetimeScope();
            builder.RegisterType<UsuarioService>().As<IUsuarioService>().InstancePerLifetimeScope();
            builder.RegisterType<EnvioEmailService>().As<IEnvioEmailService>().InstancePerLifetimeScope();
        }
    }
}
