using System.Collections.Generic;

namespace Business.Interfaces
{
    public interface IPushGenerator
    {
        bool SendPushPrueba();
        bool SendPushNotifications(string title, string body, List<string> tokens);
    }
}