using System.Collections.Generic;
using System.Runtime.InteropServices;
using Artemis.Core;
using GameFinder.RegistryUtils;
using GameFinder.StoreHandlers.Steam;

namespace Artemis.Plugins.Games.CSGO.Prerequisites;

public class CsgoInstalledPrerequisite : PluginPrerequisite
{
    private readonly SteamHandler _steamHandler;

    public CsgoInstalledPrerequisite()
    {
        _steamHandler = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? new SteamHandler(new WindowsRegistry())
            : new SteamHandler(registry: null);

        InstallActions = new()
        {
            new RunInlinePowerShellAction("Install CS:GO", @"start ""steam://run/730""")
        };
        
        UninstallActions = new();
    }
    
    public override bool IsMet()
    {
        var game = _steamHandler.FindOneGameById(730, out var errors);
        return game != null;
    }

    public override string Name  => "CS:GO Installed";
    
    public override string Description => "CS:GO must be installed to use this plugin";
    
    public override List<PluginPrerequisiteAction> InstallActions { get; }
    
    public override List<PluginPrerequisiteAction> UninstallActions { get; }
}