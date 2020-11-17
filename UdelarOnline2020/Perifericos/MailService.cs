using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Perifericos
{
    public static class MailService
    {
          public static void SendMail(string to, string cc,string subject, string body, string from)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("noreplyudelaronline@gmail.com", "UdelarOnline2020.");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;  

            MailMessage m = new MailMessage();

            m.From = new MailAddress("noreplyudelaronline@gmail.com", from);
            m.BodyEncoding = Encoding.UTF8;
            m.Subject = subject;
            m.Body = body;
            m.IsBodyHtml = true;
            m.To.Add(to);

            if(cc != "")
            m.CC.Add(cc);         

            client.Send(m);
        }

    }
}
