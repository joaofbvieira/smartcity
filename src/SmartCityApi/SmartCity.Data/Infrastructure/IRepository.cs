using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SmartCity.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Inserir(T entity);
        void Alterar(T entity);
        void Deletar(T entity);
        void Deletar(Expression<Func<T, bool>> where);
        T BuscarPorId(int id);
        IEnumerable<T> BuscarTodos();
        IEnumerable<T> BuscarVariosComFiltro(Expression<Func<T, bool>> where);
        T BuscarUmComFiltro(Expression<Func<T, bool>> where);
    }
}
