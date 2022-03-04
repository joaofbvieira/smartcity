using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.ViewModel.Ocorrencias
{
    public class OcorrenciaImagemViewModel
    {
        public string Conteudo { get; set; }
        public string Extensao { get; set; }

        public byte[] Base64ToByteArray(string base64)
        {
            return Convert.FromBase64String(base64);
        }

        public string ByteArrayToBase64(byte[] img)
        {
            return Convert.ToBase64String(img);
        }
    }
}
