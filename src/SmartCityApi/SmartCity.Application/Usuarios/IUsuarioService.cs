using SmartCity.ViewModel;
using SmartCity.ViewModel.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.Application.Usuarios
{
    public interface IUsuarioService
    {
        ResultBase AutenticarUsuario(AutenticacaoUsuarioViewModel autenticacao);

        ResultBase RegistrarUsuario(CadastroUsuarioViewModel dadosUsuario);

        ResultBase RecuperarSenha(RecuperarSenhaUsuarioViewModel dados);

    }
}
