using Microsoft.EntityFrameworkCore;
using SmartCity.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        SmartCityContext dbContext;
        string _connectionString;

        public DbFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SmartCityContext Inicializar()
        {
            return dbContext ?? (dbContext = new SmartCityContext(
                new DbContextOptionsBuilder<SmartCityContext>()
                    .UseSqlServer(_connectionString)
                    .Options
                ));
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
