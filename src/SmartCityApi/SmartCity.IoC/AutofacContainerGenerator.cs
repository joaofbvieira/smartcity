using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.IoC
{
    public class AutofacContainerGenerator
    {
        private ContainerBuilder ContainerBuilder;
        public IContainer Container { get; set; }

        public AutofacContainerGenerator()
        {
            // create a Autofac container builder
            ContainerBuilder = new ContainerBuilder();
        }
        
        public void CarregarDependenciasDeServicesCollection(IServiceCollection services)
        {
            // read service collection to Autofac
            ContainerBuilder.Populate(services);
        }

        public void CarregarDependencias()
        {
            ContainerBuilder.RegisterModule<ModuleCommonRegister>();
            ContainerBuilder.RegisterModule<ModuleDataRegister>();
            ContainerBuilder.RegisterModule<ModuleServicesRegister>();

            // build the Autofac container
            Container = ContainerBuilder.Build();
        }

        public void CarregarDependenciasTestesUnitarios()
        {
            ContainerBuilder.RegisterModule<ModuleUnitTestRegister>();
            ContainerBuilder.RegisterModule<ModuleCommonRegister>();
            ContainerBuilder.RegisterModule<ModuleDataRegister>();
            ContainerBuilder.RegisterModule<ModuleServicesRegister>();

            // build the Autofac container
            Container = ContainerBuilder.Build();
        }
    }
}
