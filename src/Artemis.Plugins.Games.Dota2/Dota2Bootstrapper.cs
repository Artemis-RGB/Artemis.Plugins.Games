using System.IO;
using Artemis.Core;
using Artemis.GameFinder.Prerequisites;

namespace Artemis.Plugins.Games.Dota2;

public class Dota2Bootstrapper : PluginBootstrapper
{
    private  const int Dota2SteamId = 570;
    private const string CsgoGsiConfigFilename = "gamestate_integration_artemis.cfg";
    
    public override void OnPluginLoaded(Plugin plugin)
    {
        //TODO: verify this
        AddPluginPrerequisite(new IsSteamInstalledPrerequisite(plugin));
        AddPluginPrerequisite(new IsSteamGameInstalledPrerequisite(Dota2SteamId, "Dota 2"));
        AddPluginPrerequisite(new IsFilePresentInSteamGameFolderPrerequisite(
            Dota2SteamId,
            plugin.ResolveRelativePath(Path.Combine("Resources", CsgoGsiConfigFilename)),
            Path.Combine("game", "dota", "cfg", "gamestate_integration",CsgoGsiConfigFilename)));
    }
}