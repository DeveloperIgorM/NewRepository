using MailKit.Security;

namespace NewRepository.Models
{
    public class EmailSettings
    {
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public SecureSocketOptions UseSSL { get; internal set; }
    }

}
