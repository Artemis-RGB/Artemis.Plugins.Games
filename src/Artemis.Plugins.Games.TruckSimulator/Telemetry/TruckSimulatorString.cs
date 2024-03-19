using System.Text;

namespace Artemis.Plugins.Games.TruckSimulator.Telemetry;

public struct TruckSimString64
{
    private Array64<byte> _data;
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (ref readonly var c in _data)
        {
            if (c == 0)
                break;
            sb.Append((char)c);
        }
        return sb.ToString();
    }
    
    public static implicit operator string(TruckSimString64 str) => str.ToString();
}

public struct TruckSimString32
{
    private Array32<byte> _data;
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (ref readonly var c in _data)
        {
            if (c == 0)
                break;
            sb.Append((char)c);
        }
        return sb.ToString();
    }
    
    public static implicit operator string(TruckSimString32 str) => str.ToString();
}

public struct TruckSimString16
{
    private Array16<byte> _data;
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (ref readonly var c in _data)
        {
            if (c == 0)
                break;
            sb.Append((char)c);
        }
        return sb.ToString();
    }
    
    public static implicit operator string(TruckSimString16 str) => str.ToString();
}