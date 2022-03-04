using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.ViewModel.Ocorrencias
{
    public class ConsultaOcorrenciasUsuarioViewModel
    {
        public List<OcorrenciaViewModel> Pendentes { get; set; }
        public List<OcorrenciaViewModel> Atendidas { get; set; }
        public List<OcorrenciaViewModel> Denunciadas { get; set; }

        public ConsultaOcorrenciasUsuarioViewModel()
        {
            Pendentes = new List<OcorrenciaViewModel>();
            Atendidas = new List<OcorrenciaViewModel>();
            Denunciadas = new List<OcorrenciaViewModel>();
        }
    }
}
