using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.EliteDangerous.DataModels;
using Artemis.Plugins.Games.EliteDangerous.Journal;
using Artemis.Plugins.Games.EliteDangerous.Status;
using System;
using System.Collections.Generic;
using System.IO;

namespace Artemis.Plugins.Games.EliteDangerous
{
    [PluginFeature(Name = "Elite: Dangerous", Icon = "Elite-Dangerous.svg", AlwaysEnabled = true)]
    public class EliteDangerousModule : Module<EliteDangerousDataModel>
    {
        private static readonly string EliteDataDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            @"Saved Games\Frontier Developments\Elite Dangerous"
        );

        private JournalParser journalParser;
        private StatusParser statusParser;

        public override List<IModuleActivationRequirement> ActivationRequirements { get; } = new()
        {
            new ProcessActivationRequirement("EliteDangerous64")
        };

        public override void Enable()
        {
            journalParser = new JournalParser(EliteDataDirectory);
            statusParser = new StatusParser(EliteDataDirectory);
        }

        public override void Disable()
        {
            journalParser.Dispose();
            statusParser.Dispose();
        }

        public override void Update(double deltaTime)
        {
            journalParser.PerformUpdate(DataModel);
            statusParser.PerformUpdate(DataModel);
        }

        public override void ModuleActivated(bool isOverride)
        {
            journalParser.Activate();
            statusParser.Activate();
        }

        public override void ModuleDeactivated(bool isOverride)
        {
            journalParser.Deactivate();
            statusParser.Deactivate();
        }
    }
}
