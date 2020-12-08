using Business.Interfaces;
using Models;

namespace Perifericos.Mail
{
  public class MailGenerator : IMailGenerator
  {
    public bool mailCerrarActa(string to, string subject, Curso curso)
    {


      string mail = string.Empty;
      mail = $"<html>" +
                  "<head> " +
                  "<link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css' integrity='sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T' crossorigin='anonymous'>" +
                  "</head>" +
                  "<body>";
      mail += "<h4>Estimado alumno:</h4> <br>";
      mail += $"<h4>Le informamos que ya se encuentran cerradas las actas para el curso {curso.Nombre} {curso.Descripcion}.</h4><br>";
      mail += $"<h4>Cualquier duda que tenga, no dude en consultar con sus docentes.</h4>";
      mail += "<br><br><br><br><br>";
      mail += "<h4>Saluda atte.: Equipo de UdelarOnline</h4>";
      mail += "</body>" +
                  "</html>";

      MailService.SendMail(to, "", subject, mail, "UdelarOnline - Notificaciones");
      return true;
    }

    public bool mailInscripcionCurso(string to, string subject, Curso curso)
    {


      string mail = string.Empty;

      mail = $"<html>" +
                  "<head> " +
                  "<link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css' integrity='sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T' crossorigin='anonymous'>" +
                  "</head>" +
                  "<body>";
      mail += "<h4>Estimado alumno:</h4> <br>";
      mail += $"<h4>Se ha registrado la inscripción al curso {curso.Nombre} {curso.Descripcion}.</h4><br>";
      mail += $"<h4>Esperamos que los contenidos del curso, sean de su agrado.</h4>";
      mail += "<br><br><br><br><br>";
      mail += "<h4>Saluda atte.: Equipo de UdelarOnline</h4>";
      mail += "</body>" +
                  "</html>";

      MailService.SendMail(to, "", subject, mail, "UdelarOnline - Notificaciones");
      return true;
    }

    public bool mailInscripcionEvaluacion(string to, string subject, PruebaOnline evaluacion)
    {
      
          string mail = string.Empty;

          mail = $"<html>" +
                      "<head> " +
                      "<link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css' integrity='sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T' crossorigin='anonymous'>" +
                      "</head>" +
                      "<body>";
          mail += "<h4>Estimado alumno:</h4> <br>";
          mail += $"<h4>Se ha registrado la inscripción a la evaluación {evaluacion.Nombre} {evaluacion.Descripcion}.</h4><br>";
          mail += $"<h4>La misma tiene fecha de realización: {evaluacion.FechaFinalizada}.</h4>";
          mail += "<br><br><br><br><br>";
          mail += "<h4>Le deseamos, buena suerte en la misma.</h4>";
          mail += "<br><br><br>";
          mail += "<h4>Saluda atte.: Equipo de UdelarOnline</h4>";
          mail += "</body>" +
                      "</html>";

          MailService.SendMail(to, "", subject, mail, "UdelarOnline - Notificaciones");
          return true;
    }

    public bool ResetPassword(string to, string subject,  string token, Usuario usuario)
    {

          string mail = string.Empty;

          mail = $"<html>" +
                      "<head> " +
                      "<link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css' integrity='sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T' crossorigin='anonymous'>" +
                      "</head>" +
                      "<body>";
          mail += "<h4>Estimado alumno:</h4> <br>";
          mail += "<h4>En este mail encontrará un link para poder cambiar su contraseña. En caso de no haber solicitado dicha acción, ignore el mensaje.</h4><br>";
          mail += $"<h4>Link:  <a href='http://udelaronline.web.elasticloud.uy/home/forgot-password?fromEmail=true&token={token}' target='_blank' rel='noopener noreferrer'>ir a UdelarOnline</a>.</h4>";
          mail += "<br><br><br><br><br>";
          mail += "<h4>Saluda atte.: Equipo de UdelarOnline</h4>";
          mail += "</body>" +
                      "</html>";

          MailService.SendMail(usuario.EmailPersonal, "", subject, mail, "UdelarOnline - Notificaciones");
          return true;
    }

    public bool SendMail(string to, string subject, string body)
    {

      string mail = string.Empty;

      mail = $"<html>" +
                  "<head> " +
                  "<link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css' integrity='sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T' crossorigin='anonymous'>" +
                  "</head>" +
                  "<body>";
      mail += "<h4>Estimado alumno:</h4> <br>";
      mail += "<h4>En este mail encontrará un link para poder cambiar su contraseña. En caso de no haber solicitado dicha acción, ignore el mensaje.</h4><br>";
      mail += $"<h4>Link:  <a href='http://localhost:4200/home/forgot-password?fromEmail=true&email={to}' target='_blank' rel='noopener noreferrer'>ir a UdelarOnline</a>.</h4>";
      mail += "<br><br><br><br><br>";
      mail += "<h4>Saluda atte.: Equipo de UdelarOnline</h4>";
      mail += "</body>" +
                  "</html>";

      MailService.SendMail(to, "", subject, mail, "UdelarOnline - Notificaciones");
      return true;
    }

    public bool SendMailPrueba()
    {

      string mail = string.Empty;

      mail = $"<html>" +
                  "<head> " +
                  "<link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css' integrity='sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T' crossorigin='anonymous'>" +
                  "</head>" +
                  "<body>";
      mail += "<h4>Esto es un mail de prueba TSI.NET</h4>";
      mail += "</body>" +
                  "</html>";
      string to = "pascaltomas27@gmail.com, nahuelbentosgnocchi@gmail.com";
      string cc = "";
      string subject = $"Mail de notificaciones PRUEBA.";
      string from = "PRUEBA NOTIFICACION ";
      MailService.SendMail(to, cc, subject, mail, from);
      return true;
    }
  }
}