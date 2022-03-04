using SmartCity.Domain.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.Domain.Models.Ocorrencias
{
    public class VotoOcorrencia
    {
        public int VotoOcorrenciaId { get; set; }

        public bool Positivo { get; set; }
        public DateTime DataHoraInclusao { get; set; }
        public string Comentario { get; set; }

        public int OcorrenciaId { get; set; }
        public int UsuarioId { get; set; }
        public int MotivoDenunciaId { get; set; }
    }
}
