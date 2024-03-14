using System.Runtime.InteropServices;

namespace Artemis.Plugins.Games.TrackMania.Telemetry;

#pragma warning disable 169
[StructLayout(LayoutKind.Sequential)]
public readonly struct Vec3
{
    public readonly float x;
    public readonly float y;
    public readonly float z;
};