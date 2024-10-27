using MimeKit;
using MailKit.Net.Smtp;
using NewRepository.Models;

namespace NewRepository.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarEmailAsync(string para, string assunto, string mensagem)
        {
            var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailSettings.SenderName, emailSettings.SenderEmail));
            message.To.Add(new MailboxAddress("", para)); // Nome vazio, apenas para garantir o formato correto
            message.Subject = assunto;
            message.Body = new TextPart("plain") { Text = mensagem };


            using var client = new SmtpClient();
            await client.ConnectAsync(emailSettings.SmtpServer, emailSettings.Port, emailSettings.UseSSL);
            await client.AuthenticateAsync(emailSettings.Username, emailSettings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

}
