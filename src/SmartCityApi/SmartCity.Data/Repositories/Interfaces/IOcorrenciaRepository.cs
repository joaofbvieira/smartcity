using SmartCity.Data.Infrastructure;
using SmartCity.Domain.Models.Ocorrencias;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.Data.Repositories.Interfaces
{
    public interface IOcorrenciaRepository : IRepository<Ocorrencia>
    {
        IEnumerable<OcorrenciaImagem> BuscarImagensPorIdOcorrencia(int id);

        IEnumerable<Ocorrencia> BuscarOcorrenciasPorUsuario(int idUsuario, DateTime dataInicial);

        IEnumerable<Ocorrencia> BuscarOcorrenciasNoRaioDeKm(double raioKm, double latitude, double longitude);
    }
}
