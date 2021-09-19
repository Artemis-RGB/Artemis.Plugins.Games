using Artemis.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;

namespace Artemis.Plugins.Games.RocketLeague.Module.Prerequisites
{
    public class BakkesModPluginPrerequisite : PluginPrerequisite
    {
        public override string Name => "Artemis GSI BakkesMod Plugin";

        public override string Description => "Plugin for BakkesMod responsible for sending data to Artemis";

        public override List<PluginPrerequisiteAction> InstallActions { get; }

        public override List<PluginPrerequisiteAction> UninstallActions { get; }

        public override bool IsMet()
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\BakkesMod\AppPath");
            if (key == null)
            {
                return false;
            }

            string path = key?.GetValue("BakkesModPath")?.ToString();
            if (path == null)
            {
                return false;
            }

            string pluginsPath = Path.Combine(path, "plugins");
            if (!Directory.Exists(pluginsPath))
            {
                return false;
            }

            string pluginFilename = Path.Combine(pluginsPath, "ArtemisGSI.dll");

            return File.Exists(pluginFilename);
        }

        public BakkesModPluginPrerequisite()
        {
            //hack: how do i get info that only exists after the first pre requisite is met?
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path = Path.Combine(appData, "bakkesmod", "bakkesmod");
            string pluginPath = Path.Combine(path, "plugins", "ArtemisGSI.dll");

            InstallActions = new List<PluginPrerequisiteAction>
            {
                new ExecuteFileAction("Install BakkesMod Plugin", "bakkesmod://install/230")
            };

            UninstallActions = new List<PluginPrerequisiteAction>
            {
                new DeleteFileAction("Delete ArtemisGSI plugin", pluginPath),
            };
        }
    }
}
