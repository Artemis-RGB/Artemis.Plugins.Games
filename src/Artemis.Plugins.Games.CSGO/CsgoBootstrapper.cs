using System.IO;
using Artemis.Core;
using Artemis.GameFinder.Prerequisites;

namespace Artemis.Plugins.Games.CSGO;

public class CsgoBootstrapper : PluginBootstrapper
{
    private  const int CsgoSteamId = 730;
    private const string CsgoGsiConfigFilename = "gamestate_integration_artemis.cfg";
    
    public override void OnPluginLoaded(Plugin plugin)
    {
        AddPluginPrerequisite(new IsSteamInstalledPrerequisite(plugin));
        AddPluginPrerequisite(new IsSteamGameInstalledPrerequisite(CsgoSteamId, "CS:GO"));
        AddPluginPrerequisite(new IsFilePresentInSteamGameFolderPrerequisite(
            CsgoSteamId,
            plugin.ResolveRelativePath(Path.Combine("Resources", CsgoGsiConfigFilename)),
            Path.Combine("game", "csgo", "cfg", CsgoGsiConfigFilename) ));
    }
}