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

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi
{
    [PluginFeature(Name = "League Of Legends")]
    public class LeagueOfLegendsModule : Module<LeagueOfLegendsDataModel>
    {
        public override List<IModuleActivationRequirement> ActivationRequirements { get; }
            = new() { new ProcessActivationRequirement("League Of Legends") };

        private readonly ILogger _logger;
        private readonly ChampionColorService _championColorService;

        private LolInGameApiClient inGameApi;
        private RootGameData gameData;
        private float lastEventTime;
        private bool newMatch;

        public LeagueOfLegendsModule(ILogger logger, ChampionColorService championColorService)
        {
            _logger = logger;
            _championColorService = championColorService;

            UpdateDuringActivationOverride = false;
        }

        public override void Enable()
        {
            inGameApi = new LolInGameApiClient();
            AddTimedUpdate(TimeSpan.FromMilliseconds(100), UpdateData);
        }

        public override void Disable()
        {
            inGameApi?.Dispose();
        }

        public override void ModuleActivated(bool isOverride)
        {
            newMatch = true;
        }

        public override void ModuleDeactivated(bool isOverride)
        {
            //reset data.
            _logger.Information("Deactivating module");
            gameData = new();
            //fire and forget
            _ = SetupNewMatch();
            UpdateTickData();
            lastEventTime = 0f;
        }

        public override void Update(double deltaTime) { }

        private async Task UpdateData(double deltaTime)
        {
            try
            {
                gameData = await inGameApi.GetAllGameDataAsync();
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
            catch (Exception e)
            {
                _logger.Error(e, "Error updating LoL game data");
                return;
            }

            if (newMatch)
            {
                await SetupNewMatch();
                newMatch = false;
            }

            UpdateTickData();
        }

        /// <summary>
        /// Data that only needs to be set once per match
        /// </summary>
        private async Task SetupNewMatch()
        {
            _logger.Information("Setting up new match...");
            DataModel.SetupMatch(gameData);
            DataModel.Player.ChampionColors = await _championColorService.GetSwatch(DataModel.Player.ShortChampionName, DataModel.Player.SkinID);
        }

        /// <summary>
        /// Data that needs to be set every tick
        /// </summary>
        private void UpdateTickData()
        {
            DataModel.Update(gameData);
            FireOffEvents();
        }

        private void FireOffEvents()
        {
            foreach (LolEvent e in gameData.Events.Events.Where(ev => ev.EventTime > lastEventTime))
            {
                lastEventTime = e.EventTime;

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
}