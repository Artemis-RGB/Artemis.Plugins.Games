using System;
using System.IO.MemoryMappedFiles;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace Artemis.Plugins.Games.TrackMania.Telemetry;

internal sealed class MemoryMappedStructReader<T> : IDisposable where T : unmanaged
{
    private readonly MemoryMappedFile _file;
    private readonly MemoryMappedViewAccessor _view;

    public MemoryMappedStructReader(string name)
    {
        Name = name;
        Size = Unsafe.SizeOf<T>();

        _file = MemoryMappedFile.OpenExisting(Name, MemoryMappedFileRights.Read);
        _view = _file.CreateViewAccessor(0, Size, MemoryMappedFileAccess.Read);
    }

    public string Name { get; }
    public int Size { get; }

    public void Dispose()
    {
        _file.Dispose();
    }

    public unsafe T Read()
    {
        ObjectDisposedException.ThrowIf(_file.SafeMemoryMappedFileHandle.IsClosed, _file.SafeMemoryMappedFileHandle);

        return Unsafe.AsRef<T>(_view.SafeMemoryMappedViewHandle.DangerousGetHandle().ToPointer());
    }

    public override string ToString()
    {
        return $"[{Name}, {Size}]";
    }
}