using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.ViewModel.Ocorrencias
{
    public class VotoOcorrenciaViewModel
    {
        public bool Positivo { get; set; }
        public string Comentario { get; set; }

        public int OcorrenciaId { get; set; }
        public int UsuarioId { get; set; }
        public int MotivoDenunciaId { get; set; }

    }
}
