using System;
using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient.DataModels;
using Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient.LcuEvents.EventData;
using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;
using Newtonsoft.Json;
using Serilog;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient.LcuEvents;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient;

[PluginFeature(Name = "League Client")]
public class LeagueClientModule : Module<LeagueClientDataModel>
{
    private const string ProcessName = "LeagueClientUx";

    private static readonly string[] LcuEvents =
    {
        // "OnJsonApiEvent",
        "OnJsonApiEvent_lol-gameflow_v1_session",
        "OnJsonApiEvent_lol-champ-select-legacy_v1_session",
        "OnJsonApiEvent_lol-champ-select_v1_session",
        "OnJsonApiEvent_lol-game-settings_v1_ready",
        "OnJsonApiEvent_lol-login_v1_session",
        "OnJsonApiEvent_lol-login_v1_summoner_session",
        "OnJsonApiEvent_lol-lobby_v2_lobby",
        "OnJsonApiEvent_lol-chat_v1_session",
    };

    public override List<IModuleActivationRequirement> ActivationRequirements { get; }
        = new() { new ProcessActivationRequirement(ProcessName) };

    private readonly ILogger _logger;
    private GameFlowData? _gameFlow;
    private ChampSelectData? _champSelect;
    private LcuClient? lcuClient;

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
        var tries = 0;
        while (tries < MAX_TRIES)
        {
            if (TrySetupLcuClient().Result)
                return;

            tries++;
            Thread.Sleep(1000);
        }

        throw new ArtemisPluginException("Couldn't setup LCU client.");
    }

    private async Task<bool> TrySetupLcuClient()
    {
        try
        {
            if (!LockfileUtils.TryFind(ProcessName, out var lockFile))
                return false;

            lcuClient = new LcuClient(lockFile);
            lcuClient.EventReceived += LcuClientOnEventReceived;
            lcuClient.MessageReceived += LcuClientOnMessageReceived;
            lcuClient.Error += LcuClientOnError;
            await lcuClient.Connect();
            foreach (var lcuEvent in LcuEvents) await lcuClient.Subscribe(lcuEvent);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private void LcuClientOnError(object? sender, Exception e)
    {
        _logger.Error(e, "LCU client error");
    }

    private void LcuClientOnMessageReceived(object? sender, LcuEvent e)
    {
        _logger.Information("Message: {Uri} | {EventType}", e.Uri, e.EventType);
    }

    private void LcuClientOnEventReceived(object? sender, LcuEvent e)
    {
        switch (e)
        {
            case LcuEvent<GameFlowData> gameFlow:
                //sometimes the game fires an event with the same data as the previous one, so we ignore it
                if (gameFlow.Data == _gameFlow)
                    return;
                
                _gameFlow = gameFlow.Data;
                _logger.Information("GameFlow event: {Uri} | {EventType} | {Data}", gameFlow.Uri, gameFlow.EventType,
                    JsonConvert.SerializeObject(gameFlow.Data));
                break;
            case LcuEvent<ChampSelectData> champSelect:
                if (champSelect.Data == _champSelect)
                    return;
                
                _champSelect = champSelect.Data;
                _logger.Information("ChampSelect event: {Uri} | {EventType} | {Data}", champSelect.Uri, champSelect.EventType,
                    JsonConvert.SerializeObject(champSelect.Data));
                break;
            case LcuEvent<object> @event:
                _logger.Information("Event: {Uri} | {EventType} | {Data}", @event.Uri, @event.EventType,
                    JsonConvert.SerializeObject(@event.Data));
                break;
            default:
                _logger.Information("Unknown event: {Uri} | {EventType}", e.Uri, e.EventType);
                break;
        }
    }

    public override void ModuleDeactivated(bool isOverride)
    {
        if (lcuClient == null)
            return;
        
        try
        {
            lcuClient.EventReceived -= LcuClientOnEventReceived;
            lcuClient.MessageReceived -= LcuClientOnMessageReceived;
            lcuClient.Error -= LcuClientOnError;
            lcuClient.Dispose();
        }
        catch
        {
            // ignored
        }
    }
}