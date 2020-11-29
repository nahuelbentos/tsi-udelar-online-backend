namespace Business.Interfaces
{
    public interface IMailGenerator
    {
         bool SendMailPrueba();
         bool SendMail(string to, string subject, string body);
    }
}