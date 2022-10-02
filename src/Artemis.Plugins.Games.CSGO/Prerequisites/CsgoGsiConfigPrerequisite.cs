using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Artemis.Core;
using GameFinder.RegistryUtils;
using GameFinder.StoreHandlers.Steam;

namespace Artemis.Plugins.Games.CSGO.Prerequisites;

public class CsgoGsiConfigPrerequisite : PluginPrerequisite
{
    private const int CsgoSteamId = 730;
    private const string CsgoGsiConfigFilename = "gamestate_integration_artemis.cfg";
    private readonly string _configDestinationPathRelative;
    private readonly string _configSourcePath;
    private readonly SteamHandler _steamHandler;

    public CsgoGsiConfigPrerequisite(Plugin plugin)
    {
        _configDestinationPathRelative = Path.Combine("csgo", "cfg", CsgoGsiConfigFilename);
        _configSourcePath = plugin.ResolveRelativePath(Path.Combine("Resources", CsgoGsiConfigFilename));
        _steamHandler = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? new SteamHandler(new WindowsRegistry())
            : new SteamHandler(registry: null);

        InstallActions = new()
        {
            new CopyFileToSteamGameFolderAction(
                "Copy CS:GO GSI Config",
                CsgoSteamId, 
                _configSourcePath,
                _configDestinationPathRelative)
        };

        UninstallActions = new()
        {
            new DeleteFileFromSteamGameFolder(
                "Delete CS:GO GSI Config",
                CsgoSteamId,
                _configDestinationPathRelative)
        };
    }
    public override string Name  => "CS:GO GSI Config";
    public override string Description => "This will install the required GSI config for CS:GO";
    public override List<PluginPrerequisiteAction> InstallActions { get; }
    public override List<PluginPrerequisiteAction> UninstallActions { get; }
    
    public override bool IsMet()
    {
        if (!TryGetGamePath(CsgoSteamId, out var path))
            return false;
        
        return File.Exists(Path.Combine(path, _configDestinationPathRelative));
    }
    
    private bool TryGetGamePath(int id, [NotNullWhen(true)] out string? path)
    {
        var game = _steamHandler.FindOneGameById(id, out var errors);
        if (game == null)
        {
            path = null;
            return false;
        }
        
        path = game.Path;
        return true;
    }
}

public class CopyFileToSteamGameFolderAction : PluginPrerequisiteAction
{
    /// <summary>
    /// Copies a file to the specified steam game folder
    /// </summary>
    /// <param name="name">The name of the action</param>
    /// <param name="steamGameId">The steam game id</param>
    /// <param name="source">The absolute path to the file to copy</param>
    /// <param name="destination">The destination path of the file, relative to the game root.</param>
    public CopyFileToSteamGameFolderAction(string name, int steamGameId, string source, string destination) 
        : base(name)
    {
        SteamGameId = steamGameId;
        Source = source;
        Destination = destination;
        ProgressIndeterminate = true;
    }
    
    public int SteamGameId { get; }
    public string Source { get; }
    public string Destination { get; }
    
    public override async Task Execute(CancellationToken cancellationToken)
    {
        var steamHandler = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? new SteamHandler(new WindowsRegistry())
            : new SteamHandler(registry: null);
        
        var game = steamHandler.FindOneGameById(SteamGameId, out var errors);
        if (game == null)
            throw new ArtemisPluginException("Could not find game with id " + SteamGameId);
        
        var destinationPath = Path.Combine(game.Path, Destination);

        using var source = File.Open(Source, FileMode.Open);
        using var destination = File.Create(destinationPath);
        
        await source.CopyToAsync(destination);
    }
}

public class DeleteFileFromSteamGameFolder : PluginPrerequisiteAction
{
    /// <summary>
    ///   Deletes a file from the specified steam game folder
    /// </summary>
    /// <param name="name">The name of the action</param>
    /// <param name="steamId">The id of the steam game</param>
    /// <param name="filePath">The path to the file to delete, relative to the game root.</param>
    public DeleteFileFromSteamGameFolder(string name, int steamId, string filePath)
        : base(name)
    {
        SteamId = steamId;
        FilePath = filePath;
        ProgressIndeterminate = true;
    }
    
    public int SteamId { get; }
    public string FilePath { get; }
    
    public override Task Execute(CancellationToken cancellationToken)
    {
        var steamHandler = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? new SteamHandler(new WindowsRegistry())
            : new SteamHandler(registry: null);

        var game = steamHandler.FindOneGameById(SteamId, out var errors);
        if (game == null)
            throw new ArtemisPluginException("Could not find game with id " + SteamId);

        var filePath = Path.Combine(game.Path, FilePath);
        if (File.Exists(filePath))
            File.Delete(filePath);

        return Task.CompletedTask;
    }
}