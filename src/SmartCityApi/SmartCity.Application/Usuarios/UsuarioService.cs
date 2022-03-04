using System;
using System.Collections.Generic;
using System.Text;
using SmartCity.ViewModel;
using SmartCity.ViewModel.Usuarios;
using SmartCity.Data.Repositories.Interfaces;
using SmartCity.Domain.Models.Usuarios;
using SmartCity.Data.Infrastructure;
using SmartCity.Application.Email;

namespace SmartCity.Application.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        IUsuarioRepository _usuarioRepo;
        IUnitOfWork _unitOfWork;
        IEnvioEmailService _envioEmailService;

        public UsuarioService(IUsuarioRepository usuarioRepo, IUnitOfWork unitOfWork, IEnvioEmailService envioEmailService)
        {
            _usuarioRepo = usuarioRepo;
            _unitOfWork = unitOfWork;
            _envioEmailService = envioEmailService;
        }

        public ResultBase AutenticarUsuario(AutenticacaoUsuarioViewModel autenticacao)
        {
            var result = new ResultBase();

            try
            {
                Usuario usuario = _usuarioRepo.ObterUsuarioPorEmail(autenticacao.Email);

                if (usuario == null)
                {
                    result.Sucesso = false;
                    result.Mensagem = "Não existe um usuário com o e-mail informado.";
                    return result;
                }

                result.Sucesso = usuario.ValidarSenhaParaAutenticacao(autenticacao.Senha);

                if (!result.Sucesso)
                    result.Mensagem = "A senha não confere.";
                else
                    result.Dados = ConverterUsuarioEmViewModel(usuario);

            }
            catch (Exception ex)
            {
                result.Sucesso = false;
                result.Mensagem = "Não foi possível autenticar o usuário.";
                result.Excecao = ex;
            }

            return result;
        }

        public ResultBase RecuperarSenha(RecuperarSenhaUsuarioViewModel dados)
        {
            var result = new ResultBase();
            
            try
            {
                Usuario usuario = _usuarioRepo.ObterUsuarioPorEmail(dados.Email);

                if (usuario == null)
                {
                    result.Sucesso = false;
                    result.Mensagem = "Não existe um usuário com o e-mail informado.";
                    return result;
                }

                string senhaGerada = usuario.GerarSenhaAleatoria();
                usuario.Senha = usuario.ObterSenhaCriptografada(senhaGerada);
                
                _usuarioRepo.Alterar(usuario);
                _unitOfWork.Commit();

                EnviarEmailRecuperacao(usuario, senhaGerada);

                result.Sucesso = true;
            }
            catch (Exception ex)
            {
                result.Sucesso = false;
                result.Mensagem = "Não foi possível recuperar a senha.";
                result.Excecao = ex;
            }

            return result;
        }

        public ResultBase RegistrarUsuario(CadastroUsuarioViewModel dadosUsuario)
        {
            var result = new ResultBase();

            try
            {
                Usuario usuarioExistente = _usuarioRepo.ObterUsuarioPorEmail(dadosUsuario.Email);

                if (usuarioExistente != null)
                {
                    result.Sucesso = false;
                    result.Mensagem = "Já existe um usuário com o e-mail informado.";
                    return result;
                }

                if (!dadosUsuario.Senha.Equals(dadosUsuario.ConfirmacaoSenha, StringComparison.InvariantCultureIgnoreCase))
                {
                    result.Sucesso = false;
                    result.Mensagem = "As senhas informadas não conferem.";
                    return result;
                }

                var usuario = new Usuario();
                usuario.Email = dadosUsuario.Email.ToLowerInvariant();
                usuario.NomeCompleto = dadosUsuario.Nome;
                usuario.CidadeId = dadosUsuario.CidadeId;
                usuario.DataHoraCadastro = DateTime.Now;
                usuario.Senha = usuario.ObterSenhaCriptografada(dadosUsuario.Senha);
                
                _usuarioRepo.Inserir(usuario);
                _unitOfWork.Commit();

                var usuarioInserido = _usuarioRepo.ObterUsuarioPorEmail(usuario.Email);
                result.Dados = ConverterUsuarioEmViewModel(usuarioInserido);
                result.Sucesso = true;

                EnviarEmailBoasVindas(usuarioInserido, dadosUsuario.Senha);
            }
            catch (Exception ex)
            {
                result.Sucesso = false;
                result.Mensagem = "Não foi possível registrar o usuário.";
                result.Excecao = ex;
            }

            return result;
        }
        
        private UsuarioViewModel ConverterUsuarioEmViewModel(Usuario usuario)
        {
            var usuarioVM = new UsuarioViewModel();
            usuarioVM.UsuarioId = usuario.UsuarioId;
            usuarioVM.Nome = usuario.NomeCompleto;
            usuarioVM.Email = usuario.Email;
            usuarioVM.DataHoraCadastro = usuario.DataHoraCadastro;

            usuarioVM.Cidade = new ViewModel.Cidades.CidadeViewModel();
            usuarioVM.Cidade.CidadeId = usuario.CidadeId;
            usuarioVM.Cidade.Nome = usuario.Cidade.Nome;
            usuarioVM.Cidade.IBGE = usuario.Cidade.IBGE;
            usuarioVM.Cidade.CEP = usuario.Cidade.CEP;
            usuarioVM.Cidade.UF = usuario.Cidade.UF;

            return usuarioVM;
        }

        private async void EnviarEmailRecuperacao(Usuario usuario, string novaSenha)
        {
            string corpoEmail = string.Format(@"
Olá {0}! <br />
Geramos uma nova senha para o seu login. <br />
Segue abaixo a senha gerada: <br />
<b> {1} </b> <br />

 <br />
Atenciosamente, <br />
Equipe SmartCity", usuario.NomeCompleto, novaSenha);
            
            await _envioEmailService.EnviarEmailAsync(usuario.Email, "Recuperação de Senha", corpoEmail);
        }

        private async void EnviarEmailBoasVindas(Usuario usuario, string senha)
        {
            string corpoEmail = string.Format(@"
Olá {0}! <br /><br />

Seja bem-vindo ao SmartCity em {1}-{2}. <br /><br />

Segue abaixo seus dados de login: <br />
E-mail: <b> {3} </b> <br />
Senha: <b> {4} </b> <br />
<br /><br />

Muito obrigado por utilizar o SmartCity!
<br /><br />
Atenciosamente, <br />
Equipe SmartCity", usuario.NomeCompleto, usuario.Cidade.Nome, usuario.Cidade.UF, usuario.Email, senha);

            await _envioEmailService.EnviarEmailAsync(usuario.Email, "Bem-vindo ao SmartCity", corpoEmail);
        }
    }
}
