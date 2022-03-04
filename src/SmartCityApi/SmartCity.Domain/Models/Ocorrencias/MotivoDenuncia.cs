using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartCity.Domain.Models.Ocorrencias
{
    public class MotivoDenuncia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MotivoDenunciaId { get; set; }

        public string Descricao { get; set; }

    }
}
