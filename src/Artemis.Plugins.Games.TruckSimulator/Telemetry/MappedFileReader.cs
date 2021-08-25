using System;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace Artemis.Plugins.Modules.TruckSimulator.Telemetry
{

    public class MappedFileReader<TStruct> : IDisposable where TStruct : struct
    {

        private readonly string filename;
        private MemoryMappedFile memoryMappedFile;
        private MemoryMappedViewAccessor memoryMappedViewAccessor;

        public MappedFileReader(string filename)
        {
            this.filename = filename;
        }

        public TStruct? Read()
        {
            // If the mapped file isn't open, and it fails to open, return nothing
            if (memoryMappedFile == null && !TryOpenMap())
                return null;

            // -- Below code adapted from the ETS2 Telemetry Server by Funbit (https://github.com/Funbit/ets2-telemetry-server) --
            var memPointer = IntPtr.Zero;
            try
            {
                var raw = new byte[Marshal.SizeOf<TStruct>()];
                memoryMappedViewAccessor.ReadArray(0, raw, 0, raw.Length);

                memPointer = Marshal.AllocHGlobal(raw.Length);
                Marshal.Copy(raw, 0, memPointer, raw.Length);
                return Marshal.PtrToStructure<TStruct>(memPointer);
            }
            catch
            {
                return null;
            }
            finally
            {
                if (memPointer != IntPtr.Zero)
                    Marshal.FreeHGlobal(memPointer);
            }
            // -- End ETS2 Telemetry Server code --
        }

        /// <summary>
        /// Attempts to open the MemoryMappedFile at the given filename.
        /// </summary>
        /// <returns>Whether the operation was successful (<c>true</c>) or not (<c>false</c>).</returns>
        public bool TryOpenMap()
        {
            if (memoryMappedFile != null) return true;
            try
            {
                memoryMappedFile = MemoryMappedFile.OpenExisting(filename, MemoryMappedFileRights.ReadWrite);
                memoryMappedViewAccessor = memoryMappedFile.CreateViewAccessor(0, Marshal.SizeOf<TStruct>(), MemoryMappedFileAccess.Read);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            memoryMappedFile?.Dispose();
            memoryMappedViewAccessor?.Dispose();
        }
    }
}
