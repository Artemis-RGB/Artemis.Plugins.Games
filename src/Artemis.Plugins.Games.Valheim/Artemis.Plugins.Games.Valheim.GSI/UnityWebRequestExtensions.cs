using System;
using UnityEngine.Networking;

namespace Artemis.Plugins.Games.Valheim.GSI
{
    public static class UnityWebRequestExtensions
    {
        public static void SendWithTimeout(this UnityWebRequest request, TimeSpan timeout)
        {
            var startTime = DateTime.UtcNow;
            request.SendWebRequest();
            while (!request.isDone)
            {
                if (DateTime.UtcNow > startTime + timeout)
                {
                    throw new TimeoutException();
                }
            }
        }
    }
}
