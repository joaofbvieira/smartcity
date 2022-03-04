using Microsoft.EntityFrameworkCore;
using SmartCity.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SmartCity.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        #region Properties
        private SmartCityContext dataContext;
        private readonly DbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected SmartCityContext DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Inicializar()); }
        }
        #endregion

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        #region Implementation
        public virtual void Inserir(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Alterar(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Deletar(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void Deletar(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public virtual T BuscarPorId(int id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> BuscarTodos()
        {
            return dbSet;
        }

        public virtual IEnumerable<T> BuscarVariosComFiltro(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where);
        }

        public T BuscarUmComFiltro(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }

        #endregion

    }
}
