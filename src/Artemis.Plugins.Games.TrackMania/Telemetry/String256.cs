using System;
using System.Text;

namespace Artemis.Plugins.Games.TrackMania.Telemetry;

#pragma warning disable 169
public struct String256
{
    private Array256<byte> _data;

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

    public static implicit operator string(String256 str) => str.ToString();
}

public struct String128
{
    private Array128<byte> _data;

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

    public static implicit operator string(String128 str) => str.ToString();
}

public struct String64
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

    public static implicit operator string(String64 str) => str.ToString();
}

public struct String32
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

    public static implicit operator string(String32 str) => str.ToString();
}

public struct String24
{
    private Array24<byte> _data;

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

    public static implicit operator string(String24 str) => str.ToString();
}

public struct String16
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

    public static implicit operator string(String16 str) => str.ToString();
}

public struct String20
{
    private Array20<byte> _data;

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

    public static implicit operator string(String20 str) => str.ToString();
}

public struct String28
{
    private Array28<byte> _data;

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

    public static implicit operator string(String28 str) => str.ToString();
}

public struct String4
{
    private Array4<byte> _data;

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

    public static implicit operator string(String4 str) => str.ToString();
}