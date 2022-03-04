using SmartCity.Data.Infrastructure;
using SmartCity.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using SmartCity.Domain.Models.Ocorrencias;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace SmartCity.Data.Repositories
{
    public class OcorrenciaRepository : RepositoryBase<Ocorrencia>, IOcorrenciaRepository
    {
        public OcorrenciaRepository(IDbFactory dbFactory)
            : base(dbFactory) { }


        public override Ocorrencia BuscarPorId(int id)
        {
            var ocorrencia = base.BuscarPorId(id);
            DbContext.Entry(ocorrencia).Collection(o => o.Imagens).Load();
            DbContext.Entry(ocorrencia).Reference(o => o.Usuario).Load();
            return ocorrencia;
        }

        public IEnumerable<OcorrenciaImagem> BuscarImagensPorIdOcorrencia(int id)
        {
            return DbContext.OcorrenciaImagens.Where(i => i.OcorrenciaId == id);
        }

        public IEnumerable<Ocorrencia> BuscarOcorrenciasPorUsuario(int idUsuario, DateTime dataInicial)
        {
            return DbContext.Ocorrencias
                .Where(o => o.UsuarioId == idUsuario 
                            && ((o.DataHoraInclusao >= dataInicial && o.Atendida) || !o.Atendida)
                );
        }
        
        public IEnumerable<Ocorrencia> BuscarOcorrenciasNoRaioDeKm(double raioKm, double latitude, double longitude)
        {
            var sql = $@"
SELECT d.OcorrenciaId, d.Atendida, d.DataHoraInclusao, d.Descricao, d.EnderecoCompleto, d.Latitude, d.Longitude, d.UsuarioId 
  FROM (
 SELECT 
	    z.OcorrenciaId, z.Atendida, z.DataHoraInclusao, z.Descricao, 
	    z.EnderecoCompleto, z.Latitude, z.Longitude, z.UsuarioId,
        p.radius,
        p.distance_unit
                 * DEGREES(ACOS(COS(RADIANS(p.latpoint))
                 * COS(RADIANS(CAST(z.latitude AS REAL)))
                 * COS(RADIANS(p.longpoint - CAST(z.longitude AS REAL)))
                 + SIN(RADIANS(p.latpoint))
                 * SIN(RADIANS(CAST(z.latitude AS REAL))))) AS distance
  FROM Ocorrencias AS z (NOLOCK)
  JOIN ( 
        SELECT  @latitude  AS latpoint,  @longitude AS longpoint,
                @raioKm AS radius,      111.045 AS distance_unit
    ) AS p ON 1=1
  WHERE CAST(z.latitude AS REAL)
     BETWEEN p.latpoint  - (p.radius / p.distance_unit)
         AND p.latpoint  + (p.radius / p.distance_unit)
    AND CAST(z.longitude AS REAL)
     BETWEEN p.longpoint - (p.radius / (p.distance_unit * COS(RADIANS(p.latpoint))))
         AND p.longpoint + (p.radius / (p.distance_unit * COS(RADIANS(p.latpoint))))
) AS d
WHERE distance <= radius
      AND ISNULL(Atendida,0) = 0
ORDER BY distance ASC";

            var paramLatitude = new SqlParameter("latitude", latitude);
            var paramlongitude = new SqlParameter("longitude", longitude);
            var paramRaioKm = new SqlParameter("raioKm", raioKm);

            return DbContext.Ocorrencias.FromSql(sql, paramLatitude, paramlongitude, paramRaioKm).ToList();
        }
    }
}
