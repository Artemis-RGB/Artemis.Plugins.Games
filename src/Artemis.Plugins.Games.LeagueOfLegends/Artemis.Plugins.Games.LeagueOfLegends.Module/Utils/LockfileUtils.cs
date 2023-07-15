using System.Diagnostics;
using System.IO;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;

public record LockfileData(string ProcessName, int ProcessId, int Port, string Password, string Protocol);

internal static class LockfileUtils
{
    public static bool TryFind(string processName, out LockfileData data)
    {
        var processes = Process.GetProcessesByName(processName);
        if (processes.Length == 0)
        {
            data = default;
            return false;
        }

        var path = Path.Combine(
            Path.GetDirectoryName(processes[0].MainModule.FileName),
            "lockfile");

        if (!File.Exists(path))
        {
            data = default;
            return false;
        }

        using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var textReader = new StreamReader(fileStream);
        var lockFile = textReader.ReadToEnd();

        var parts = lockFile.Split(':');
        if (parts.Length != 5)
        {
            data = default;
            return false;
        }

        data = new LockfileData(parts[0], int.Parse(parts[1]), int.Parse(parts[2]), parts[3], parts[4]);
        return true;
    }
}