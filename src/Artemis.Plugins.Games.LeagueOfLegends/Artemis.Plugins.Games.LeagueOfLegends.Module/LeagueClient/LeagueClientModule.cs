using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient
{
    public class LeagueClientModule : Module<LeagueClientDataModel>
    {
        private const string PROCESS_NAME = "LeagueClientUx";
        public override List<IModuleActivationRequirement> ActivationRequirements { get; }
            = new() { new ProcessActivationRequirement(PROCESS_NAME) };

        private readonly ILogger _logger;
        private LcuClient lcuClient;

        public LeagueClientModule(ILogger logger)
        {
            _logger = logger;
        }

        public override void Enable()
        {
        }

        public override void Disable()
        {
        }

        public override void Update(double deltaTime)
        {
        }

        public override void ModuleActivated(bool isOverride)
        {
            const int MAX_TRIES = 3;
            int tries = 0;
            while (tries < MAX_TRIES)
            {
                if (TrySetupLcuClient())
                    return;

                tries++;
                Thread.Sleep(1000);
            }

            throw new ArtemisPluginException(Plugin, "Couldn't setup LCU client.");
        }

        private bool TrySetupLcuClient()
        {
            try
            {
                if (!LockfileUtils.TryFind(PROCESS_NAME, out var lockFile))
                    return false;

                lcuClient = new(lockFile);
                lcuClient.EventReceived += LcuClientOnEventReceived;
                lcuClient.Connect().Wait();
                //list of events to subscribe to:
                //https://www.mingweisamuel.com/lcu-schema/lcu/help.json
                lcuClient.Subscribe("OnJsonApiEvent_lol-champ-select_v1_session").Wait();
                lcuClient.Subscribe("OnJsonApiEvent_lol-lobby_v2_lobby").Wait();
                lcuClient.Subscribe("OnJsonApiEvent_lol-gameflow_v1_session").Wait();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void LcuClientOnEventReceived(object sender, LcuEvent e)
        {
            _logger.Information("{uri} | {data}", e.Uri, e.Data.ToString());
        }

        public override void ModuleDeactivated(bool isOverride)
        {
            lcuClient.EventReceived -= LcuClientOnEventReceived;
            lcuClient.Dispose();
        }
    }
}
