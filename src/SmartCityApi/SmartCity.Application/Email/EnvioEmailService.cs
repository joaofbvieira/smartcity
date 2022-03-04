using SmartCity.Domain;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Application.Email
{
    public class EnvioEmailService : IEnvioEmailService
    {
        private string PrimaryDomain = "smtp.gmail.com";
        private int PrimaryPort = 587;
        private string UsernameEmail = "smartcity.anhanguera@gmail.com";
        private string UsernamePassword = "smart1234";
        private string FromEmail = "smartcity.anhanguera@gmail.com";
        
        public Task EnviarEmailAsync(string emailDestino, string assunto, string mensagem)
        {
            Execute(emailDestino, assunto, mensagem).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(string email, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(UsernameEmail, "SmartCity")
                };
                mail.To.Add(new MailAddress(email));

                mail.Subject = "SmartCity - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(PrimaryDomain, PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(UsernameEmail, UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        
    }
}
