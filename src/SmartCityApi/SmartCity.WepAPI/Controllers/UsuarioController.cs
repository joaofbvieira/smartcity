using Microsoft.AspNetCore.Mvc;
using SmartCity.Application.Usuarios;
using SmartCity.ViewModel;
using SmartCity.ViewModel.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity.WepAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("Autenticar")]
        public ResultBase Autenticar([FromBody] AutenticacaoUsuarioViewModel autenticacao)
        {
            return _usuarioService.AutenticarUsuario(autenticacao);
        }


        [HttpPost("Registrar")]
        public ResultBase Registrar([FromBody] CadastroUsuarioViewModel usuario)
        {
            return _usuarioService.RegistrarUsuario(usuario);
        }


        [HttpPost("RecuperarSenha")]
        public ResultBase RecuperarSenha([FromBody] RecuperarSenhaUsuarioViewModel dados)
        {
            return _usuarioService.RecuperarSenha(dados);
        }


    }
}
