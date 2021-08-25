using Artemis.Plugins.Modules.EliteDangerous.DataModels;
using System;
using System.IO;

namespace Artemis.Plugins.Modules.EliteDangerous.Utils
{
    /// <summary>
    /// Base class that provides access to a file and will read from it each update.
    /// </summary>
    internal abstract class FileReaderBase : IDisposable
    {
        // Lock and flag prevents accidently reading from a closed stream
        private readonly object streamLock = new();
        private FileStream fileStream;
        private StreamReader streamReader;
        private readonly bool tailMode;

        /// <param name="tailMode">
        /// <list type="bullet">
        ///     <item>If <c>false</c>, will read entire file on update.
        ///     <see cref="OnContentRead"/> will be called once per update and be passed the full contents of the file.</item>
        ///     <item>If <c>true</c>, will only read new lines.
        ///     <see cref="OnContentRead"/> may be called multiple times per update, each time with a line from the file.</item>
        ///     </list>
        /// </param>
        internal FileReaderBase(bool tailMode)
        {
            this.tailMode = tailMode;
        }

        /// <summary>
        /// Whether or not the reader currently has a file open.
        /// </summary>
        public bool IsOpen => fileStream != null;

        /// <summary>
        /// Closes current file (if one is open) and opens a new one.
        /// </summary>
        /// <param name="path">The full path of the file to open.</param>
        protected void OpenFile(string path)
        {
            CloseFile();
            lock (streamLock)
            {
                fileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                streamReader = new StreamReader(fileStream);
            }
        }

        /// <summary>
        /// Closes the streams accessing the current file.
        /// </summary>
        protected void CloseFile()
        {
            lock (streamLock)
            {
                streamReader?.Close();
                fileStream?.Close();
                streamReader = null;
                fileStream = null;
            }
        }

        /// <summary>
        /// Called whenever the Elite module is activated.
        /// </summary>
        public abstract void Activate();

        /// <summary>
        /// Called whenever the Elite module is deactivated.
        /// </summary>
        public virtual void Deactivate() => CloseFile();

        /// <summary>
        /// Reads from the file and updates the data model with new values.
        /// </summary>
        /// <param name="dataModel">Data model to update.</param>
        public void PerformUpdate(EliteDangerousDataModel dataModel)
        {
            lock (streamLock)
            {
                if (!IsOpen) return;
                if (tailMode)
                {
                    // In tail mode, read each new line one-by-one
                    while (!streamReader.EndOfStream)
                        OnContentRead(dataModel, streamReader.ReadLine());
                }
                else
                {
                    // In non-tail mode, seek to start of file and read entire thing
                    fileStream.Seek(0, SeekOrigin.Begin);
                    OnContentRead(dataModel, streamReader.ReadToEnd());
                }
            }
        }

        /// <summary>
        /// Occurs when content is read from the current file during an update.<para/>
        /// Depending on whether the reader is in tail mode, this may either be called multiple
        /// times per update with each line or once per update with entire file contents.
        /// </summary>
        /// <param name="dataModel">Data model to update with parsed data.</param>
        /// <param name="content">The content of the file/current line.</param>
        protected abstract void OnContentRead(EliteDangerousDataModel dataModel, string content);

        public virtual void Dispose() => CloseFile();
    }
}
