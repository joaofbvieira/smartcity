using SmartCity.Data.Infrastructure;
using SmartCity.Data.Repositories.Interfaces;
using SmartCity.Domain.Models.Ocorrencias;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartCity.Data.Repositories
{
    public class VotoOcorrenciaRepository : RepositoryBase<VotoOcorrencia>, IVotoOcorrenciaRepository
    {
        public VotoOcorrenciaRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public int ObterQtdeDenunciasDaOcorrencia(int idOcorrencia)
        {
            return DbContext.OcorrenciaVotos.Count(v => v.OcorrenciaId == idOcorrencia && !v.Positivo);
        }

        public int ObterQtdeVotosDaOcorrencia(int idOcorrencia)
        {
            return DbContext.OcorrenciaVotos.Count(v => v.OcorrenciaId == idOcorrencia && v.Positivo);
        }

        public int ObterQtdeVotosPorUsuarioEOcorrencia(int idUsuario, int idOcorrencia)
        {
            return DbContext.OcorrenciaVotos.Count(v => v.OcorrenciaId == idOcorrencia && v.UsuarioId == idUsuario);
        }

    }
}
