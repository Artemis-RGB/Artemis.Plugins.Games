using Artemis.Core;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.DataModels;
using Artemis.Plugins.Games.LeagueOfLegends.DataModels.Enums;
using Artemis.Plugins.Games.LeagueOfLegends.DataModels.EventArgs;
using Artemis.Plugins.Games.LeagueOfLegends.GameDataModels;
using Artemis.Plugins.Games.LeagueOfLegends.Utils;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Artemis.Plugins.Games.LeagueOfLegends
{
    [PluginFeature(AlwaysEnabled = true, Icon = "icon.svg", Name = "League Of Legends")]
    public class LeagueOfLegendsModule : Module<LeagueOfLegendsDataModel>
    {
        public override List<IModuleActivationRequirement> ActivationRequirements { get; }
            = new() { new ProcessActivationRequirement("League Of Legends") };

        private readonly ILogger _logger;

        private LolClient lolClient;
        private RootGameData gameData;
        private float lastEventTime;

        public LeagueOfLegendsModule(ILogger logger)
        {
            _logger = logger;

            UpdateDuringActivationOverride = false;
        }

        public override void Enable()
        {
            lolClient = new LolClient();
            AddTimedUpdate(TimeSpan.FromMilliseconds(100), UpdateData);
        }

        public override void Disable()
        {
            lolClient?.Dispose();
        }

        public override void ModuleActivated(bool isOverride)
        {
        }

        public override void ModuleDeactivated(bool isOverride)
        {
            //reset data.
            DataModel.Apply(new RootGameData());
        }

        public override void Update(double deltaTime) { }

        private async Task UpdateData(double deltaTime)
        {
            try
            {
                gameData = await lolClient.GetAllDataAsync();
                DataModel.Apply(gameData);
            }
            catch (Exception e)
            {
                _logger.Error("Error updating LoL game data", e);
                return;
            }

            FireOffEvents();
        }

        private void FireOffEvents()
        {
            if (gameData.Events.Events.Length == 0)
            {
                lastEventTime = 0f;
                return;
            }

            foreach (LolEvent e in gameData.Events.Events)
            {
                if (e.EventTime <= lastEventTime)
                    continue;

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
                        DataModel.Match.FirstBrick.Trigger();
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

                lastEventTime = e.EventTime;
            }
        }
    }
}