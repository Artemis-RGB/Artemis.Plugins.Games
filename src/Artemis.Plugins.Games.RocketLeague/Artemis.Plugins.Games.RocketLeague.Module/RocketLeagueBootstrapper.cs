using Artemis.Core;
using Artemis.Plugins.Games.RocketLeague.Module.Prerequisites;

namespace Artemis.Plugins.Games.RocketLeague.Module
{
    public class RocketLeagueBootstrapper : PluginBootstrapper
    {
        public override void OnPluginLoaded(Plugin plugin)
        {
            AddPluginPrerequisite(new BakkesModPrerequisite(plugin));
            AddPluginPrerequisite(new BakkesModPluginPrerequisite());
        }
    }
}
