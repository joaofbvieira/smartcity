using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;

namespace SmartCity.IoC
{
    public class ModuleUnitTestRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Add the configuration to the ConfigurationBuilder.
            var config = new ConfigurationBuilder();
            config.AddJsonFile("appsettings.json");

            var cfg = config.Build();
            
            // Register the ConfigurationModule with Autofac.
            var module = new ConfigurationModule(cfg);
            builder.RegisterModule(module);

            builder.RegisterInstance<IConfiguration>(cfg).SingleInstance();
        }
    }
}
