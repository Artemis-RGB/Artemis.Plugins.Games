using System;

namespace Artemis.Plugins.Games.TrackMania.DataModels;

[Flags]
public enum CarHandicap
{
    None = 0,
    EngineForcedOff = 1,
    EngineForcedOn = 2,
    NoBrakes = 4,
    NoSteering = 8,
    NoGrip = 16
}