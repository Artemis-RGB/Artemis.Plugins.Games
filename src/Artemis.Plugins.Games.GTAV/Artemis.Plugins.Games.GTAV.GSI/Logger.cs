using System;
using System.IO;

namespace Artemis.Plugins.Games.GTAV.GSI
{
    /// <summary>
    /// Static logger class that allows direct logging of anything to a text file
    /// </summary>
    public static class Logger
    {
        public static void Log(object message)
        {
            File.AppendAllText("ArtemisGSI.log", DateTime.Now + " : " + message + Environment.NewLine);
        }
    }
}