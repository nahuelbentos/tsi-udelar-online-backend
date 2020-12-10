using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Perifericos.PushNotifications {
    public class PushNotificationsService {
        public static string SendPushNotifications (string title, string body, List<string> tokens) {
            try {

                string applicationID = "AAAA9bhpV2s:APA91bEk0NLDdzvAYbcC-dyVqo4RN0QLY4UI0qLIB83kuOuvbD_MtK7tqIBhfQ5CZyFx4ZqhmANUJvowwjkBegoeZ-6lscjyn-3tsqa0MC_6CNGjQq4dqTTDq-Mc-FnvNT0UE5HufiM4";
                // string senderId = "1055360898923";
                WebRequest tRequest = WebRequest.Create ("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new {
                    registration_ids = tokens,
                    notification = new {
                    body = body,
                    title = title,
                    }
                };
                string json = JsonSerializer.Serialize (data);
                Byte[] byteArray = Encoding.UTF8.GetBytes (json);
                tRequest.Headers.Add (string.Format ("Authorization: Bearer {0}", applicationID));
                // tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream ()) {
                    dataStream.Write (byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse ()) {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream ()) {
                            using (StreamReader tReader = new StreamReader (dataStreamResponse)) {
                                String sResponseFromServer = tReader.ReadToEnd ();
                                string str = sResponseFromServer;
                                return str;
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                return ex.Message;
            }
        }
    }
}