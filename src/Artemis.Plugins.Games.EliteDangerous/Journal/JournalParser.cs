using Artemis.Plugins.Modules.EliteDangerous.DataModels;
using Artemis.Plugins.Modules.EliteDangerous.Utils;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Artemis.Plugins.Modules.EliteDangerous.Journal
{
    internal class JournalParser : FileReaderBase
    {
        private const string JournalFileFilter = "Journal*.*.log";

        private readonly string dataDirectory;
        private FileSystemWatcher journalFileWatcher;

        public JournalParser(string dataDirectory) : base(true)
        {
            this.dataDirectory = dataDirectory;

            // Create file system watcher to watch for when new journal files are created.
            journalFileWatcher = new FileSystemWatcher(dataDirectory, JournalFileFilter);
            journalFileWatcher.Created += JournalFileWatcher_Created;
        }

        /// <summary>
        /// Returns the filename of the newest journal log in the given journal directory.
        /// </summary>
        private string LatestJournal
        {
            get
            {
                var logFiles = Directory.GetFiles(dataDirectory, JournalFileFilter);
                Array.Sort(logFiles);
                return logFiles[^1];
            }
        }

        /// <summary>
        /// When the module is activated, begin reading the latest journal file.
        /// </summary>
        public override void Activate()
        {
            journalFileWatcher.EnableRaisingEvents = true;
            OpenFile(LatestJournal);
        }

        /// <inheritdoc />
        public override void Deactivate()
        {
            journalFileWatcher.EnableRaisingEvents = false;
            base.Deactivate();
        }

        /// <summary>
        /// Parses a single journal line and if it is a known event applies it to the datamodel.
        /// </summary>
        protected override void OnContentRead(EliteDangerousDataModel dataModel, string line)
        {
            var @event = JsonConvert.DeserializeObject<IJournalEvent>(line, JournalEvent.JournalEventSettings);
            @event?.ApplyUpdate(dataModel);
        }

        /// <summary>
        /// Handles when a new journal file is created by opening the newest file.
        /// </summary>
        private void JournalFileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            OpenFile(LatestJournal);
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            journalFileWatcher.Dispose();
            journalFileWatcher = null;
            base.Dispose();
        }
    }
}
