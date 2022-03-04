using SmartCity.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        SmartCityContext Inicializar();
    }
}
