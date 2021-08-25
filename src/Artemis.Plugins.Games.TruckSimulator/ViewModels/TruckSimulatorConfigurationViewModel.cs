using Artemis.Core;
using Artemis.UI.Shared;
using Microsoft.Win32;
using System.IO;
using System.Windows.Navigation;

namespace Artemis.Plugins.Modules.TruckSimulator.ViewModels
{
    public class TruckSimulatorConfigurationViewModel : PluginConfigurationViewModel
    {

        private const string x64PluginPath = @"win_x64\plugins\scs-telemetry.dll";
        private const string x86PluginPath = @"win_x86\plugins\scs-telemetry.dll";

        public TruckSimulatorConfigurationViewModel(Plugin plugin) : base(plugin) { }

        public void InstallPlugins()
        {
            if (GetGameBinPath() is string path)
            {
                CopyDLL(Path.Join(path, x86PluginPath), "scs-telemetry-x86.dll");
                CopyDLL(Path.Join(path, x64PluginPath), "scs-telemetry-x64.dll");

                // TODO: Add feedback to user that it's been installed
            }
        }

        public void UninstallPlugins()
        {
            if (GetGameBinPath() is string path)
            {
                DeleteDLL(Path.Join(path, x86PluginPath));
                DeleteDLL(Path.Join(path, x64PluginPath));

                // TODO: Add feedback to user that it's been installed
            }
        }

        /// <summary>
        /// Copies the contents of the specified telemetry dll to the given path.
        /// </summary>
        /// <param name="path">The destination path (directory and filename) of the destination file to write.</param>
        /// <param name="resourceName">The name of the resource inside the /Telemetry/Plugins folder of the dll to write.</param>
        private void CopyDLL(string path, string resourceName)
        {
            if (File.Exists(path))
                return;

            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            var resourcePath = $"Artemis.Plugins.Modules.TruckSimulator.Telemetry.Plugins.{resourceName}";
            using var fileStream = File.Create(path);
            typeof(TruckSimulatorConfigurationViewModel).Assembly.GetManifestResourceStream(resourcePath).CopyTo(fileStream);
        }

        /// <summary>
        /// If the given file exists, deletes it.
        /// </summary>
        public void DeleteDLL(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        /// <summary>
        /// Gets the game 'bin' path by asking the user to select one of the game exes.
        /// </summary>
        /// <returns>The bin path of the selected game, or null if the user cancelled.</returns>
        private string GetGameBinPath()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select Euro Truck Simulator 2 or American Truck Simulator game executable",
                Filter = "Game Executable (eurotrucks2.exe; amtrucks.exe)|eurotrucks2.exe;amtrucks.exe"
            };

            // Game exe is in /bin/win_x64 or /bin/win_x86, so to get bin need to GetDirectoryName twice.
            return dialog.ShowDialog() == true ? Path.GetDirectoryName(Path.GetDirectoryName(dialog.FileName)) : null;
        }

        public void OpenHyperlink(object sender, RequestNavigateEventArgs e)
        {
            Utilities.OpenUrl(e.Uri.AbsoluteUri);
        }
    }
}
