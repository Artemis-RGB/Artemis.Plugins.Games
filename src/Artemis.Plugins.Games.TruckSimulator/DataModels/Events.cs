using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

public class Events
{
    private byte _lastFined;
    private byte _lastTollGate;
    private byte _lastFerry;
    private byte _lastTrain;
    // The events from the SDK use a value change to indicate an event has fired, therefore these events here
    // are basically just budget alternatives for Artemis's datamodel changed events; but doing it like this
    // is easier for the user than having to tell them to setup a change event on an integer.

    [DataModelProperty(Description = "When the player is fined for an offence, such as running a red light, crashing, etc.")]
    public DataModelEvent<FinedDataModelEventArgs> Fined { get; } = new();

    [DataModelProperty(Description = "When the player passes through a toll gate")]
    public DataModelEvent<TollGateDataModelEventArgs> TollGate { get; } = new();

    [DataModelProperty(Description = "When the player travels on a ferry")]
    public DataModelEvent<TransportDataModelEventArgs> FerryTravel { get; } = new();

    [DataModelProperty(Description = "When the player travels on a train")]
    public DataModelEvent<TransportDataModelEventArgs> TrainTravel { get; } = new();
    
    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        // If the telemetry is no longer connected (i.e. the game has been closed), then reset the event toggle
        // states to 0. If this is not done, then when the game starts up again, the events from the telemetry
        // SDK will be 0, and it will falsely trigger some events.
        if (data.sdkActive == 0)
        {
            _lastFined = _lastTollGate = _lastFerry = _lastTrain = 0;
            return;
        }

        if (data.fined != _lastFined)
        {
            Fined.Trigger(new FinedDataModelEventArgs
            {
                Amount = data.finedAmount,
                Offence = data.fineOffence
            });
            _lastFined = data.fined;
        }

        if (data.tollgate != _lastTollGate)
        {
            TollGate.Trigger(new TollGateDataModelEventArgs
            {
                PayAmount = data.tollgatePayAmount
            });
            _lastTollGate = data.tollgate;
        }

        if (data.ferry != _lastFerry)
        {
            FerryTravel.Trigger(new TransportDataModelEventArgs
            {
                PayAmount = data.ferryPayAmount,
                SourceId = data.ferrySourceId,
                SourceName = data.ferrySourceName,
                DestinationId = data.ferryDestinationId,
                DestinationName = data.ferryDestinationName
            });
            _lastFerry = data.ferry;
        }

        if (data.train != _lastTrain)
        {
            TrainTravel.Trigger(new TransportDataModelEventArgs
            {
                PayAmount = data.trainPayAmount,
                SourceId = data.trainSourceId,
                SourceName = data.trainSourceName,
                DestinationId = data.trainDestinationId,
                DestinationName = data.trainDestinationName
            });
            _lastTrain = data.train;
        }
    }
}

public class FinedDataModelEventArgs : DataModelEventArgs
{
    [DataModelProperty(Prefix = "€")]
    public long Amount { get; init; }
    public string Offence { get; init; }
}

public class TollGateDataModelEventArgs : DataModelEventArgs
{
    [DataModelProperty(Prefix = "€")]
    public long PayAmount { get; init; }
}

public class TransportDataModelEventArgs : DataModelEventArgs
{
    [DataModelProperty(Prefix = "€")]
    public long PayAmount { get; init; }
    public string SourceId { get; init; }
    public string SourceName { get; init; }
    public string DestinationId { get; init; }
    public string DestinationName { get; init; }
}