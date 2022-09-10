using Artemis.Core;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;

namespace Artemis.Plugins.Games.RocketLeague.Prerequisites
{
    public class BakkesModPrerequisite : PluginPrerequisite
    {
        public override string Name => "BakkesMod";

        public override string Description => "Mod for Rocket League required for game state integration.";

        public override List<PluginPrerequisiteAction> InstallActions { get; }

        public override List<PluginPrerequisiteAction> UninstallActions { get; }

        public override bool IsMet()
        {
            const string registryPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{BF029534-4334-4CFC-B771-50B7EE54346F}_is1";
            using RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath);

            return key != null;
        }

        public BakkesModPrerequisite(Plugin plugin)
        {
            string installerFilename = Path.Combine(plugin.Directory.FullName, "BakkesModSetup.exe");

            InstallActions = new List<PluginPrerequisiteAction>
            {
                new DownloadFileAction("Download BakkesMod setup", "https://github.com/bakkesmodorg/BakkesModInjectorCpp/releases/latest/download/BakkesModSetup.exe", installerFilename),
                new ExecuteFileAction("Install BakkesMod", installerFilename),
                new DeleteFileAction("Cleaning up...", installerFilename),
            };

            UninstallActions = new List<PluginPrerequisiteAction>
            {
                new ExecuteFileAction("Uninstall BakkesMod", @"C:\Program Files\BakkesMod\unins000.exe", "/SILENT"),
            };
        }
    }
}
