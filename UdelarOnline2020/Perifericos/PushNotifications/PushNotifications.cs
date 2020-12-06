using System.Collections.Generic;
using Business.Interfaces;

namespace Perifericos.PushNotifications
{
    public class PushNotifications : IPushGenerator
    {
        public bool SendPushPrueba(){
            string title = "Notificacion de PRUEBA";
            string body = "PRUEBA";
            List<string> tokens = new List<string>();
            tokens.Add("");
            PushNotificationsService.SendPushNotifications(title,body,tokens);
            return true;
        }

        public bool SendPushNotifications(string title, string body, List<string> tokens){
            PushNotificationsService.SendPushNotifications(title,body,tokens);
            return true;
        }
    }
}