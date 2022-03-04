using SmartCity.ViewModel;
using SmartCity.ViewModel.Ocorrencias;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.Application.Ocorrencias
{
    public interface IOcorrenciaService
    {
        ResultBase AdicionarOcorrencia(OcorrenciaViewModel ocorrenciaVm);

        ResultBase BuscarOcorrenciaPorId(int id);

        ResultBase BuscarOcorrenciaPorUsuario(int idUsuario);

        ResultBase BuscarImagensPorOcorrencia(int idOcorrencia);

        ResultBase VotarEmOcorrencia(VotoOcorrenciaViewModel votoVm);

        ResultBase BuscarOcorrenciasProximas(ConsultaOcorrenciasProximasViewModel coordenadas);
    }
}
