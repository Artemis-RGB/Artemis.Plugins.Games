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

    public void Apply(in STelemetry telemetry)
    {
        State = telemetry.Race.State;
        Time = TimeSpan.FromMilliseconds(Math.Max(0, telemetry.Race.Time));
        Respawns = (int) telemetry.Race.NbRespawns;
        Checkpoints = (int) telemetry.Race.NbCheckpoints;
        ReadOnlySpan<uint> checkpointTimes = telemetry.Race.CheckpointTimes;
        for (var i = 0; i < checkpointTimes.Length; i++)
            CheckpointTimes[i] = TimeSpan.FromMilliseconds(Math.Max(0, checkpointTimes[i]));
        CheckpointsPerLap = (int) telemetry.Race.NbCheckpointsPerLap;
        Laps = (int) telemetry.Race.NbLapsPerRace;
    }
}