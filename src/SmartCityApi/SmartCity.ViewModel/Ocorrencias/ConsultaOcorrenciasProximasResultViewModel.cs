using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.ViewModel.Ocorrencias
{
    public class ConsultaOcorrenciasProximasResultViewModel
    {
        public List<OcorrenciaViewModel> Ocorrencias { get; set; }

        public ConsultaOcorrenciasProximasResultViewModel()
        {
            Ocorrencias = new List<OcorrenciaViewModel>();
        }
    }
}
