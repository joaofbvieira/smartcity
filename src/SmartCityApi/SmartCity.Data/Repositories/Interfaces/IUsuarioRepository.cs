using SmartCity.Data.Infrastructure;
using SmartCity.Domain.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.Data.Repositories.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario ObterUsuarioPorEmail(string email);
        
    }
}
