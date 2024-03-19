using System;
using Artemis.Plugins.Games.TrackMania.Telemetry;

namespace Artemis.Plugins.Games.TrackMania.DataModels;

public class RaceDataModel
{
    public ERaceState State { get; set; }
    public TimeSpan Time { get; set; }
    public int Respawns { get; set; }
    public int Checkpoints { get; set; }

    public TimeSpan[] CheckpointTimes { get; set; } = new TimeSpan[125];
    public int CheckpointsPerLap { get; set; }
    public int Laps { get; set; }

    public void Apply(in SRaceState race)
    {
        State = race.State;
        Time = TimeSpan.FromMilliseconds(Math.Max(0, race.Time));
        Respawns = (int) race.NbRespawns;
        Checkpoints = (int) race.NbCheckpoints;
        ReadOnlySpan<uint> checkpointTimes = race.CheckpointTimes;
        for (var i = 0; i < checkpointTimes.Length; i++)
            CheckpointTimes[i] = TimeSpan.FromMilliseconds(Math.Max(0, checkpointTimes[i]));
        CheckpointsPerLap = (int) race.NbCheckpointsPerLap;
        Laps = (int) race.NbLapsPerRace;
    }
    
    public void Apply(in SRaceStateV2 race)
    {
        State = race.State;
        Time = TimeSpan.FromMilliseconds(Math.Max(0, race.Time));
        Respawns = (int) race.NbRespawns;
        Checkpoints = (int) race.NbCheckpoints;
        ReadOnlySpan<uint> checkpointTimes = race.CheckpointTimes;
        for (var i = 0; i < checkpointTimes.Length; i++)
            CheckpointTimes[i] = TimeSpan.FromMilliseconds(Math.Max(0, checkpointTimes[i]));
        CheckpointsPerLap = -1;
        Laps = -1;
    }
}