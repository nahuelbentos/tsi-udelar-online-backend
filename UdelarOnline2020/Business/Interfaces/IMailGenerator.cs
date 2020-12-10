using Models;

namespace Business.Interfaces
{
    public interface IMailGenerator
    {
         bool SendMailPrueba();
         bool SendMail(string to, string subject, string body);
         bool ResetPassword(string to, string subject, string token, Usuario usuario);

         bool mailCerrarActa(string to, string subject, Curso curso);
         bool mailInscripcionEvaluacion(string to, string subject, PruebaOnline evaluacion);
         bool mailInscripcionCurso(string to, string subject, Curso curso);
    }
}