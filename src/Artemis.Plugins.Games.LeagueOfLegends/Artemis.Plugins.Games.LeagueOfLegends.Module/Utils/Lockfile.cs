using System.Diagnostics;
using System.IO;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;

/// <summary>
///     Represents the lockfile data of a League of Legends client
/// </summary>
public struct Lockfile
{
    public string ProcessName { get; }
    public int ProcessId { get; }
    public int Port { get; }
    public string Password { get; }
    public string Protocol { get; }
    
    private Lockfile(string processName, int processId, int port, string password, string protocol)
    {
        ProcessName = processName;
        ProcessId = processId;
        Port = port;
        Password = password;
        Protocol = protocol;
    }
    
    public static bool TryFind(string processName, out Lockfile data)
    {
        var processes = Process.GetProcessesByName(processName);
        if (processes.Length == 0)
        {
            data = default;
            return false;
        }

        var path = Path.Combine(
            Path.GetDirectoryName(processes[0].MainModule!.FileName)!,
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

        data = new Lockfile(parts[0], int.Parse(parts[1]), int.Parse(parts[2]), parts[3], parts[4]);
        return true;
    }
}