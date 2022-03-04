using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.ViewModel.Usuarios
{
    public class CadastroUsuarioViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
        public int CidadeId { get; set; }

    }
}
