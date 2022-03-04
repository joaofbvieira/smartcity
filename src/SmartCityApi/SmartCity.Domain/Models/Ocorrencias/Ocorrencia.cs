using SmartCity.Domain.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.Domain.Models.Ocorrencias
{
    public class Ocorrencia
    {
        public int OcorrenciaId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHoraInclusao { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string EnderecoCompleto { get; set; }
        public bool Atendida { get; set; }

        public ICollection<OcorrenciaImagem> Imagens { get; set; }
        public ICollection<VotoOcorrencia> Votos { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public bool PermitirGravacao()
        {
            return (Imagens.Count > 0);
        }
    }
}
