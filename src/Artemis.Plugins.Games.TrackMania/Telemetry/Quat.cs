using System.Runtime.InteropServices;

namespace Artemis.Plugins.Games.TrackMania.Telemetry;

#pragma warning disable 169
[StructLayout(LayoutKind.Sequential)]
public readonly struct Quat
{
    public readonly float w;
    public readonly float x;
    public readonly float y;
    public readonly float z;
};