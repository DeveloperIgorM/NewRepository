namespace NewRepository.Services.EmailService
{
    public interface IEmailService
    {
        Task EnviarEmailAsync(string para, string assunto, string mensagem);
    }
}
