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
        "OnJsonApiEvent_lol-champ-select_v1_session",
        "OnJsonApiEvent_lol-lobby_v2_lobby",
    };

    public override List<IModuleActivationRequirement> ActivationRequirements { get; }
        = new() { new ProcessActivationRequirement(ProcessName) };

    private readonly ILogger _logger;
    private LcuWsClient? _lcuClient;
    private LcuHttpClient? _lcuHttpClient;

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
        if (isOverride)
            return;   
        
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
            if (!Lockfile.TryFind(ProcessName, out var lockFile))
                return false;

            _lcuHttpClient = new LcuHttpClient(lockFile.Port, lockFile.Password);
            _lcuClient = new LcuWsClient(lockFile);
            _lcuClient.EventReceived += LcuClientOnEventReceived;
            _lcuClient.MessageReceived += LcuClientOnMessageReceived;
            _lcuClient.Error += LcuClientOnError;
            await _lcuClient.Connect();
            foreach (var lcuEvent in LcuEvents) await _lcuClient.Subscribe(lcuEvent);
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
                _logger.Verbose("GameFlow event: {Uri} | {EventType} | {Data}", gameFlow.Uri, gameFlow.EventType,
                    JsonConvert.SerializeObject(gameFlow.Data));
                if (gameFlow.EventType == "Update")
                    UpdateGameFlow(gameFlow.Data);
                break;
            case LcuEvent<ChampSelectData> champSelect:
                _logger.Verbose("ChampSelect event: {Uri} | {EventType} | {Data}", champSelect.Uri, champSelect.EventType,
                    JsonConvert.SerializeObject(champSelect.Data));
                if (champSelect.EventType == "Update")
                    UpdateChampSelect(champSelect.Data);
                break;
            case LcuEvent<LobbyData> lobby:
                _logger.Verbose("Lobby event: {Uri} | {EventType} | {Data}", lobby.Uri, lobby.EventType,
                    JsonConvert.SerializeObject(lobby.Data));
                break;
            case LcuEvent<LobbyMember[]> lobbyMembers:
                _logger.Verbose("Lobby members event: {Uri} | {EventType} | {Data}", lobbyMembers.Uri, lobbyMembers.EventType,
                    JsonConvert.SerializeObject(lobbyMembers.Data));
                break;
            case LcuEvent<LobbySearchState> lobbySearchState:
                _logger.Verbose("Lobby search state event: {Uri} | {EventType} | {Data}", lobbySearchState.Uri, lobbySearchState.EventType,
                    JsonConvert.SerializeObject(lobbySearchState.Data));
                break;
            case LcuEvent<object> @event:
                _logger.Verbose("Event: {Uri} | {EventType} | {Data}", @event.Uri, @event.EventType,
                    JsonConvert.SerializeObject(@event.Data));
                break;
            default:
                _logger.Information("Unknown event: {Uri} | {EventType}", e.Uri, e.EventType);
                break;
        }
    }

    private void UpdateChampSelect(ChampSelectData champSelectData)
    {
        //TODO
    }

    private void UpdateGameFlow(GameFlowData gameFlowData)
    {
        if (DataModel.GameFlow.Phase == gameFlowData.Phase)
            return;
        
        _logger.Information("Gameflow state changed: {PreviousState} -> {NewState}", DataModel.GameFlow.Phase, gameFlowData.Phase);

        DataModel.GameFlow.Phase = gameFlowData.Phase;

        switch (DataModel.GameFlow.Phase)
        {
            case GameFlowPhase.Lobby:
                DataModel.GameFlow.Lobby.Trigger();
                break;
            case GameFlowPhase.Matchmaking:
                DataModel.GameFlow.Matchmaking.Trigger();
                break;
            case GameFlowPhase.ReadyCheck:
                DataModel.GameFlow.QueuePop.Trigger();
                break;
            case GameFlowPhase.ChampSelect:
                DataModel.GameFlow.ChampSelect.Trigger();
                break;

        }
    }

    public override void ModuleDeactivated(bool isOverride)
    {
        if (_lcuClient == null)
            return;
        
        try
        {
            _lcuHttpClient?.Dispose();
            _lcuClient.EventReceived -= LcuClientOnEventReceived;
            _lcuClient.MessageReceived -= LcuClientOnMessageReceived;
            _lcuClient.Error -= LcuClientOnError;
            _lcuClient.Dispose();
        }
        catch
        {
            // ignored
        }
    }
}