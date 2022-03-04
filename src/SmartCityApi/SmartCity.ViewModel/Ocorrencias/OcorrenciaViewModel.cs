using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.ViewModel.Ocorrencias
{
    public class OcorrenciaViewModel
    {
        public int OcorrenciaId { get; set; }
        public string Descricao { get; set; }
        public int UsuarioId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string EnderecoCompleto { get; set; }
        public int QtdeVotos { get; set; }
        public int QtdeDenuncias { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }

        public OcorrenciaImagemViewModel Imagem { get; set; }
        
    }
}
