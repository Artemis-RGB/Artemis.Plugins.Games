using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels
{
    /// <summary>
    /// Base class for a child DataModel class that stores a reference to the root TruckSimulatorDataModel
    /// where it can access the latest telemetry data.
    /// </summary>
    public abstract class ChildDataModel
    {
        protected ChildDataModel(TruckSimulatorDataModel root) => DataModelRoot = root;

        protected TruckSimulatorDataModel DataModelRoot { get; }
        private protected TruckSimulatorMemoryStruct Telemetry => DataModelRoot.Telemetry;
    }
}
