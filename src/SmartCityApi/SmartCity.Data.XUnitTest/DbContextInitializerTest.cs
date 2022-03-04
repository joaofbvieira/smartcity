using Autofac;
using SmartCity.Data.Context;
using SmartCity.Data.Infrastructure;
using SmartCity.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SmartCity.Data.XUnitTest
{
    public class DbContextInitializerTest
    {
        [Fact]
        public void InicializarDb_Test()
        {
            var di = new AutofacContainerGenerator();
            di.CarregarDependenciasTestesUnitarios();

            using (var dbFactory = di.Container.Resolve<IDbFactory>())
            {
                var dbContext = dbFactory.Inicializar();
                DbContextInitializer.InicializarDb(dbContext);
            }

            Assert.True(true);
        }

    }
}
