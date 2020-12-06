using Models;

namespace Business.Interfaces
{
    public interface IMailGenerator
    {
         bool SendMailPrueba();
         bool SendMail(string to, string subject, string body);
         bool ResetPassword(string to, string subject, string token, Usuario usuario);
    }
}