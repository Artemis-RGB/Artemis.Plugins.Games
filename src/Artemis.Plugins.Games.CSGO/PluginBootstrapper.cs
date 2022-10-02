using Artemis.Core;
using Artemis.Plugins.Games.CSGO.Prerequisites;

namespace Artemis.Plugins.Games.CSGO;

public class CsgoBootstrapper : PluginBootstrapper
{
    public override void OnPluginLoaded(Plugin plugin)
    {
        AddPluginPrerequisite(new CsgoInstalledPrerequisite());
        AddPluginPrerequisite(new CsgoGsiConfigPrerequisite(plugin));
    }
}