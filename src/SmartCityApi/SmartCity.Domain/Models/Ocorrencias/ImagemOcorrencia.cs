using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.Domain.Models.Ocorrencias
{
    public class OcorrenciaImagem
    {
        public int OcorrenciaImagemId { get; set; }
        public byte[] Conteudo { get; set; }
        public string Extensao { get; set; }
        public DateTime DataHoraInclusao { get; set; }

        public int OcorrenciaId { get; set; }

    }
}
