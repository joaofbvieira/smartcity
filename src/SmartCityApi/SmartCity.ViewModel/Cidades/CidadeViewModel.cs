using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.ViewModel.Cidades
{
    public class CidadeViewModel
    {
        public int CidadeId { get; set; }
        public string Nome { get; set; }
        public string IBGE { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }

    }
}
