using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.LolEventArgs;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.GameDataModels;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.GameDataModels.Events;
using Artemis.Plugins.Games.LeagueOfLegends.Module.Services;
using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi;

[PluginFeature(Name = "League Of Legends")]
public class LeagueOfLegendsModule : Module<LeagueOfLegendsDataModel>
{
    public override List<IModuleActivationRequirement> ActivationRequirements { get; }
        = new() { new ProcessActivationRequirement("League Of Legends") };

    private readonly ILogger _logger;
    private readonly ChampionColorService _championColorService;
    private LolInGameApiClient? _inGameApi;
    
    private RootGameData? _gameData;
    private float? _lastEventTime;
    private string? _lastChampionName;
    private int? _lastChampionSkin;

    public LeagueOfLegendsModule(ILogger logger, ChampionColorService championColorService)
    {
        _logger = logger;
        _championColorService = championColorService;

        UpdateDuringActivationOverride = false;
    }

    public override void Enable()
    {
        _inGameApi = new LolInGameApiClient();
        AddTimedUpdate(TimeSpan.FromMilliseconds(100), UpdateData);
    }

    public override void Disable()
    {
        _inGameApi?.Dispose();
    }

    public override void ModuleActivated(bool isOverride)
    {
        _gameData = null;
        _lastEventTime = null;
        _lastChampionName = null;
        _lastChampionSkin = null;
    }

    public override void ModuleDeactivated(bool isOverride)
    {
        _gameData = new RootGameData();
        UpdateDataModel();
        _lastEventTime = 0f;
    }

    public override void Update(double deltaTime)
    {
    }

    private async Task UpdateData(double deltaTime)
    {
        if (_inGameApi == null)
            return;
        
        try
        {
            _gameData = await _inGameApi.GetAllGameDataAsync();
        }
        catch (TaskCanceledException)
        {
            //ignore
            return;
        }
        catch (HttpRequestException)
        {
            //ignore
            return;
        }
        catch (JsonSerializationException jsonException) when (jsonException.Path == "activePlayer.error")
        {
            //happens on the first couple of ticks, ignore
            return;
        }
        catch (Exception e)
        {
            _logger.Error(e, "Error updating LoL game data");
            return;
        }

        UpdateDataModel();
    }

    /// <summary>
    /// Data that needs to be set every tick
    /// </summary>
    private void UpdateDataModel()
    {
        if (_gameData == null)
            return;
        
        if (DataModel.Player.ShortChampionName != _lastChampionName && DataModel.Player.SkinID != _lastChampionSkin)
        {
            Task.Run(async () =>
            {
                DataModel.Player.ChampionColors = await _championColorService.GetSwatch(DataModel.Player.ShortChampionName, DataModel.Player.SkinID);
                _lastChampionName = DataModel.Player.ShortChampionName;
                _lastChampionSkin = DataModel.Player.SkinID;
            });
        }

        DataModel.Update(_gameData);
        FireOffEvents();
    }

    private void FireOffEvents()
    {
        if (_gameData == null)
            return;
        
        // If we got here, we have a game data object, so we can safely assume we are in a game
        _lastEventTime ??= 0f;
        
        foreach (var e in _gameData.Events.Events.Where(ev => ev.EventTime > _lastEventTime))
        {
            _lastEventTime = e.EventTime;

            switch (e)
            {
                case AceEvent aceEvent:
                    DataModel.Match.Ace.Trigger(new AceEventArgs
                    {
                        Acer = aceEvent.Acer,
                        AcingTeam = ParseEnum<Team>.TryParseOr(aceEvent.AcingTeam, Team.Unknown)
                    });
                    break;
                case BaronKillEvent baronKillEvent:
                    DataModel.Match.BaronKill.Trigger(new EpicCreatureKillEventArgs
                    {
                        Assisters = baronKillEvent.Assisters,
                        KillerName = baronKillEvent.KillerName,
                        Stolen = baronKillEvent.Stolen
                    });
                    break;
                case ChampionKillEvent championKillEvent:
                    DataModel.Match.ChampionKill.Trigger(new ChampionKillEventArgs
                    {
                        KillerName = championKillEvent.KillerName,
                        Assisters = championKillEvent.Assisters,
                        VictimName = championKillEvent.VictimName
                    });
                    break;
                case DragonKillEvent dragonKillEvent:
                    DataModel.Match.DragonKill.Trigger(new DragonKillEventArgs
                    {
                        Assisters = dragonKillEvent.Assisters,
                        DragonType = ParseEnum<DragonType>.TryParseOr(dragonKillEvent.DragonType, DragonType.Unknown),
                        KillerName = dragonKillEvent.KillerName,
                        Stolen = dragonKillEvent.Stolen
                    });
                    break;
                case FirstBloodEvent firstBloodEvent:
                    DataModel.Match.FirstBlood.Trigger(new FirstBloodEventArgs
                    {
                        Recipient = firstBloodEvent.Recipient
                    });
                    break;
                case FirstBrickEvent firstBrickEvent:
                    DataModel.Match.FirstBrick.Trigger(new FirstBrickEventArgs
                    {
                        KillerName = firstBrickEvent.KillerName
                    });
                    break;
                case GameEndEvent gameEndEvent:
                    DataModel.Match.GameEnd.Trigger(new GameEndEventArgs
                    {
                        Win = gameEndEvent.Result == "Win"
                    });
                    break;
                case GameStartEvent gameStartEvent:
                    DataModel.Match.GameStart.Trigger();
                    break;
                case HeraldKillEvent heraldKillEvent:
                    DataModel.Match.HeraldKill.Trigger(new EpicCreatureKillEventArgs
                    {
                        Stolen = heraldKillEvent.Stolen,
                        KillerName = heraldKillEvent.KillerName,
                        Assisters = heraldKillEvent.Assisters
                    });
                    break;
                case InhibKillEvent inhibKillEvent:
                    DataModel.Match.InhibKill.Trigger(new InhibKillEventArgs
                    {
                        Assisters = inhibKillEvent.Assisters,
                        KillerName = inhibKillEvent.KillerName,
                        InhibKilled = ParseEnum<Inhibitor>.TryParseOr(inhibKillEvent.InhibKilled, Inhibitor.Unknown)
                    });
                    break;
                case InhibRespawnedEvent inhibRespawnedEvent:
                    DataModel.Match.InhibRespawned.Trigger(new InhibRespawnedEventArgs
                    {
                        InhibRespawned = ParseEnum<Inhibitor>.TryParseOr(inhibRespawnedEvent.InhibRespawned, Inhibitor.Unknown)
                    });
                    break;
                case InhibRespawningSoonEvent inhibRespawningSoonEvent:
                    DataModel.Match.InhibRespawningSoon.Trigger(new InhibRespawningSoonEventArgs
                    {
                        InhibRespawningSoon = ParseEnum<Inhibitor>.TryParseOr(inhibRespawningSoonEvent.InhibRespawningSoon, Inhibitor.Unknown)
                    });
                    break;
                case MinionsSpawningEvent minionsSpawningEvent:
                    DataModel.Match.MinionsSpawning.Trigger();
                    break;
                case MultikillEvent multikillEvent:
                    DataModel.Match.Multikill.Trigger(new MultikillEventArgs
                    {
                        KillerName = multikillEvent.KillerName,
                        KillStreak = multikillEvent.KillStreak
                    });
                    break;
                case TurretKillEvent turretKillEvent:
                    DataModel.Match.TurretKill.Trigger(new TurretKillEventArgs
                    {
                        KillerName = turretKillEvent.KillerName,
                        Assisters = turretKillEvent.Assisters,
                        TurretKilled = ParseEnum<Turret>.TryParseOr(turretKillEvent.TurretKilled, Turret.Unknown)
                    });
                    break;
            }
        }
    }
}