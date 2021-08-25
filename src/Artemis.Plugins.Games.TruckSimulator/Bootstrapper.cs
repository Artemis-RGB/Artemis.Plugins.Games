using Artemis.Core;
using Artemis.Plugins.Modules.TruckSimulator.ViewModels;
using Artemis.UI.Shared;

namespace Artemis.Plugins.Modules.TruckSimulator
{
    public class Bootstrapper : PluginBootstrapper
    {
        public override void OnPluginLoaded(Plugin plugin)
        {
            plugin.ConfigurationDialog = new PluginConfigurationDialog<TruckSimulatorConfigurationViewModel>();
        }
    }
}
