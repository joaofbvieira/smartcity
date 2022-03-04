using SmartCity.Data.Infrastructure;
using SmartCity.Data.Repositories.Interfaces;
using SmartCity.Domain.Models.Usuarios;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.Data.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        
        public Usuario ObterUsuarioPorEmail(string email)
        {
            var usuario = DbContext.Usuarios
                .Where(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();

            if (usuario != null)
                DbContext.Entry(usuario).Reference(o => o.Cidade).Load();

            return usuario;
        }


    }
}
