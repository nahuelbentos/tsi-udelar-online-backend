using System.Collections.Generic;

namespace Business.Interfaces
{
    public interface IPushGenerator
    {
        string SendPushPrueba();
        string SendPushNotifications(string title, string body, List<string> tokens);
    }
}