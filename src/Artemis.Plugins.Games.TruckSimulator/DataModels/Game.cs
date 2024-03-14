using System;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.TruckSimulator.Conversions;
using Artemis.Plugins.Games.TruckSimulator.Telemetry;

namespace Artemis.Plugins.Games.TruckSimulator.DataModels;

public class Game
{
    [DataModelProperty(Description = "Returns the game that the telemetry data is currently being retrieved from.")]
    public SCSGame CurrentGame { get; set; }
    public bool Paused { get; set; }

    [DataModelProperty(Description = "Current date and time of the game world. Note that the year starts at 1.")]
    public DateTime GameTime { get; set; }

    [DataModelProperty(Description = "Determines the scale between real time and in-game time. Typical values are 3 (in cities), 15 (in UK) and 19 (everywhere else). For example, at a scale of 19, one real-life minute would be 19 minutes in game.")]
    public float Scale { get; set; }
    
    internal void Update(in TruckSimulatorMemoryStruct data)
    {
        CurrentGame = (SCSGame)data.currentGame;
        Paused = data.paused != 0;
        GameTime = data.gameTime.ToGameDateTime();
        Scale = data.scale;
    }
}