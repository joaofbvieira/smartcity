using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.ViewModel
{
    public class ResultBase
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public Object Dados { get; set; }
        public Exception Excecao { get; set; }
        
    }
}
