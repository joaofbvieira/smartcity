using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Application.Email
{
    public interface IEnvioEmailService
    {
        Task EnviarEmailAsync(string emailDestino, string assunto, string mensagem);
    }
}
