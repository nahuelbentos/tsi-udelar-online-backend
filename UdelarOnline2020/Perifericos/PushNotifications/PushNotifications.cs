using System.Collections.Generic;
using Business.Interfaces;

namespace Perifericos.PushNotifications
{
    public class PushNotifications : IPushGenerator
    {
        public string SendPushPrueba(){
            string title = "Notificacion de PRUEBA";
            string body = "PRUEBA";
            List<string> tokens = new List<string>();
            tokens.Add("dwlY7g9sSNqGj1-cEcryDz:APA91bECAvkpIG_ZEK7qJ-7aZklxFD0HjoKVcqIg2TW0crcJvaq2hJdrPAdRcQyEnV7c4wZyJxgoAq9Y_mpUIsnjfe8UCT1V4xw5m-dAMgOOqw4ZRhCG-U3f4TktXNAuAl8jR0k96tII");
            return PushNotificationsService.SendPushNotifications(title,body,tokens);
        }

        public string SendPushNotifications(string title, string body, List<string> tokens){
            return PushNotificationsService.SendPushNotifications(title,body,tokens);
        }
    }
}