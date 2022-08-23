using Artemis.Core;
using Artemis.Core.Services;
using Artemis.UI.Shared;
using Artemis.UI.Shared.Services;
using Microsoft.Win32;
using ReactiveUI;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;

namespace Artemis.Plugins.Modules.TruckSimulator.ViewModels
{
    public class TruckSimulatorConfigurationViewModel : PluginConfigurationViewModel
    {
        private readonly IWindowService _windowService;
        private const string x64PluginPath = @"win_x64\plugins\scs-telemetry.dll";
        private const string x86PluginPath = @"win_x86\plugins\scs-telemetry.dll";

        public ReactiveCommand<Unit, Unit> InstallPlugins { get; }
        public ReactiveCommand<Unit, Unit> UninstallPlugins { get; }

        public TruckSimulatorConfigurationViewModel(Plugin plugin, IWindowService windowService) : base(plugin)
        {
            _windowService = windowService;

            InstallPlugins = ReactiveCommand.CreateFromTask(ExecuteInstallPlugins);
            UninstallPlugins = ReactiveCommand.CreateFromTask(ExecuteUninstallPlugins);
        }

        public async Task ExecuteInstallPlugins()
        {
            var path = await GetGameBinPath();
            if (path != null)
            {
                CopyDLL(Path.Join(path, x86PluginPath), "scs-telemetry-x86.dll");
                CopyDLL(Path.Join(path, x64PluginPath), "scs-telemetry-x64.dll");

                // TODO: Add feedback to user that it's been installed
            }
        }

        public async Task ExecuteUninstallPlugins()
        {
            var path = await GetGameBinPath();
            if (path != null)
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
        private static void CopyDLL(string path, string resourceName)
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
        private static void DeleteDLL(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        /// <summary>
        /// Gets the game 'bin' path by asking the user to select one of the game exes.
        /// </summary>
        /// <returns>The bin path of the selected game, or null if the user cancelled.</returns>
        private async Task<string> GetGameBinPath()
        {
            var dlg = _windowService.CreateOpenFileDialog()
                .WithTitle("Select Euro Truck Simulator 2 or American Truck Simulator game executable")
                .HavingFilter(f => f.WithExtension("exe").WithName("Euro Truck Simulator 2 or American Truck Simulator game executable"));

            var files = await dlg.ShowAsync();
            if (files?.Length == 0)
                return null;

            // Game exe is in /bin/win_x64 or /bin/win_x86, so to get bin need to GetDirectoryName twice.
            return Path.GetDirectoryName(Path.GetDirectoryName(files[0]));
        }
    }
}
