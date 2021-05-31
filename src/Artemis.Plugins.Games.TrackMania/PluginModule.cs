using Artemis.Core.Modules;
using Artemis.Plugins.Games.TrackMania.DataModels;
using Artemis.Plugins.Games.TrackMania.Telemetry;

namespace Artemis.Plugins.Games.TrackMania
{
    // The core of your module. Hover over the method names to see a description.
    public class PluginModule : Module<PluginDataModel>
    {
        private SharedProcessMemory<STelemetry> _sharedProcessMemory;

        public PluginModule()
        {
            DisplayName = "TrackMania";
            DisplayIcon = "CarSports";

            ActivationRequirements.Add(new ProcessActivationRequirement("ManiaPlanet"));
            ActivationRequirements.Add(new ProcessActivationRequirement("ManiaPlanet32"));
            ActivationRequirementMode = ActivationRequirementType.Any;
        }
        
        public override void Enable()
        {
        }

        public override void Disable()
        {
        }

        public override void Update(double deltaTime)
        {
            _sharedProcessMemory.Read();
            DataModel.Apply(_sharedProcessMemory.Data);
        }

        public override void ModuleActivated(bool isOverride)
        {
            _sharedProcessMemory = new SharedProcessMemory<STelemetry>(@"Local\ManiaPlanet_Telemetry");
        }

        public override void ModuleDeactivated(bool isOverride)
        {
        }
    }
}