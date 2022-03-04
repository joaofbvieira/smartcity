using SmartCity.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private SmartCityContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public SmartCityContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Inicializar()); }
        }

        public void Commit()
        {
            DbContext.Commit();
        }
    }
}
