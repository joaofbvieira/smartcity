using SmartCity.ViewModel.Cidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.ViewModel.Usuarios
{
    public class UsuarioViewModel
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public CidadeViewModel Cidade { get; set; }
        public DateTime DataHoraCadastro { get; set; }

    }
}
