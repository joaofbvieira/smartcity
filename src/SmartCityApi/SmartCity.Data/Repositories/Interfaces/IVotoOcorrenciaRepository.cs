using SmartCity.Data.Infrastructure;
using SmartCity.Domain.Models.Ocorrencias;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.Data.Repositories.Interfaces
{
    public interface IVotoOcorrenciaRepository : IRepository<VotoOcorrencia>
    {
        int ObterQtdeVotosDaOcorrencia(int idOcorrencia);
        int ObterQtdeDenunciasDaOcorrencia(int idOcorrencia);
        int ObterQtdeVotosPorUsuarioEOcorrencia(int idUsuario, int idOcorrencia);
    }
}
