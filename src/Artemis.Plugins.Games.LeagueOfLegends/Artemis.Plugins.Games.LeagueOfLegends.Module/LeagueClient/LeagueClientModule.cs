using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient.LcuEvents.EventData;
using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;
using Newtonsoft.Json;
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
                if (TrySetupLcuClient().Result)
                    return;

                tries++;
                Thread.Sleep(1000);
            }

            throw new ArtemisPluginException(Plugin, "Couldn't setup LCU client.");
        }

        private async Task<bool> TrySetupLcuClient()
        {
            try
            {
                if (!LockfileUtils.TryFind(PROCESS_NAME, out var lockFile))
                    return false;

                lcuClient = new(lockFile);
                lcuClient.EventReceived += LcuClientOnEventReceived;
                await lcuClient.Connect();
                //list of events to subscribe to:
                //https://www.mingweisamuel.com/lcu-schema/lcu/help.json
                await lcuClient.Subscribe("OnJsonApiEvent_lol-gameflow_v1_session");
                await lcuClient.Subscribe("OnJsonApiEvent_lol-champ-select_v1_session");
                //await lcuClient.Subscribe("OnJsonApiEvent_lol-lobby_v2_lobby");
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void LcuClientOnEventReceived(object sender, ILcuEvent e)
        {
            switch (e)
            {
                case LcuEvent<GameFlowData> gameFlow:
                    _logger.Information("GameFlow event: {uri} | {eventType} | {data}", gameFlow.Uri, gameFlow.EventType, JsonConvert.SerializeObject(gameFlow.Data));
                    break;
                case LcuEvent<ChampSelectData> champSelect:
                    _logger.Information("ChampSelect event: {uri} | {eventType} | {data}", champSelect.Uri, champSelect.EventType,JsonConvert.SerializeObject(champSelect.Data));
                    break;
                default:
                    _logger.Information("Unknown event: {uri} | {eventType}", e.Uri, e.EventType);
                    break;
            }
        }

        public override void ModuleDeactivated(bool isOverride)
        {
            lcuClient.EventReceived -= LcuClientOnEventReceived;
            lcuClient.Dispose();
        }
    }
}
