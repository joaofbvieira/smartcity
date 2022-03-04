using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartCity.Domain.Models.Cidades
{
    public class Cidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CidadeId { get; set; }

        public string Nome { get; set; }
        public string IBGE { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }
    }
}
