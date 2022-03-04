using SmartCity.Domain.Models.Cidades;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SmartCity.Domain.Models.Usuarios
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataHoraCadastro { get; set; }

        public int CidadeId { get; set; }
        public Cidade Cidade { get; set; }


        public string GerarSenhaAleatoria()
        {
            var tamanhoSenhaAleatoria = 8;

            string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            char[] chars = new char[tamanhoSenhaAleatoria];
            Random rd = new Random();

            for (int i = 0; i < tamanhoSenhaAleatoria; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        public string ObterSenhaCriptografada(string senha)
        {
            senha = senha.ToUpperInvariant();

            using (var algorithm = SHA512.Create())
            {
                var hashedBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(senha));
                senha = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return senha;
            }
        }

        public bool ValidarSenhaParaAutenticacao(string senhaAuth)
        {
            senhaAuth = senhaAuth.ToUpperInvariant();
            string senhaCriptografada = this.ObterSenhaCriptografada(senhaAuth);

            return this.Senha.Equals(senhaCriptografada, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
