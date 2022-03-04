using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.IoC
{
    public class ModuleCommonRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppConfig>().As<AppConfig>().SingleInstance();

        }
    }
}
