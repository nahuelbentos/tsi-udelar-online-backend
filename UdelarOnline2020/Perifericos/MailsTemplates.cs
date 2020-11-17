namespace Perifericos
{
    public static class MailsTemplates
    {
        public static bool MailPrueba(){

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