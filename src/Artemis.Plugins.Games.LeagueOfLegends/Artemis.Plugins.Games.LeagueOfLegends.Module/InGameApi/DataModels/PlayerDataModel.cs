using Artemis.Core.Modules;
using Artemis.Core.Services;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.GameDataModels;
using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;
using System;
using System.Linq;
using SummonerSpell = Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums.SummonerSpell;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels
{
    public class PlayerDataModel : DataModel
    {
        public AbilityGroupDataModel Abilities { get; } = new();
        public PlayerStatsDataModel ChampionStats { get; } = new();
        public InventoryDataModel Inventory { get; } = new();
        public string SummonerName { get; set; }
        public int Level { get; set; }
        public float Gold { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public int CreepScore { get; set; }
        public float WardScore { get; set; }
        public float RespawnTimer { get; set; }
        public bool IsDead { get; set; }
        public Team Team { get; set; }
        public Champion Champion { get; set; }
        public Position Position { get; set; }
        public SummonerSpell SpellD { get; set; }
        public SummonerSpell SpellF { get; set; }
        public ColorSwatch ChampionColors { get; set; }
        
        [DataModelIgnore]
        public string ShortChampionName { get; set; }
        [DataModelIgnore]
        public int SkinID { get; set; }

        public void SetupMatch(RootGameData rootGameData)
        {
            var allPlayer = Array.Find(rootGameData.AllPlayers, p => p.SummonerName == rootGameData.ActivePlayer.SummonerName);
            if (allPlayer == null)
                return;

            SummonerName = rootGameData.ActivePlayer.SummonerName;
            Team = ParseEnum<Team>.TryParseOr(allPlayer.Team, Team.Unknown);
            Champion = ParseEnum<Champion>.TryParseOr(allPlayer.RawChampionName, Champion.Unknown);
            Position = ParseEnum<Position>.TryParseOr(allPlayer.Position, Position.Unknown);

            SkinID = allPlayer.SkinID;
            ShortChampionName = allPlayer.RawChampionName.Split('_').Last();

            ChampionStats.SetupMatch(rootGameData.ActivePlayer.ChampionStats);
        }

        public void Update(RootGameData rootGameData)
        {
            var allPlayer = Array.Find(rootGameData.AllPlayers, p => p.SummonerName == rootGameData.ActivePlayer.SummonerName);
            if (allPlayer == null)
                return;

            Abilities.Update(rootGameData.ActivePlayer.Abilities);
            ChampionStats.Update(rootGameData.ActivePlayer.ChampionStats);
            Inventory.Update(allPlayer.Items);

            Level = rootGameData.ActivePlayer.Level;
            Gold = rootGameData.ActivePlayer.CurrentGold;

            Kills = allPlayer.Scores.Kills;
            Deaths = allPlayer.Scores.Deaths;
            Assists = allPlayer.Scores.Assists;
            CreepScore = allPlayer.Scores.CreepScore;
            WardScore = allPlayer.Scores.WardScore;
            RespawnTimer = allPlayer.RespawnTimer;
            IsDead = allPlayer.IsDead;

            SpellD = ParseEnum<SummonerSpell>.TryParseOr(allPlayer.SummonerSpells.SummonerSpellOne.RawDisplayName, SummonerSpell.Unknown);
            SpellF = ParseEnum<SummonerSpell>.TryParseOr(allPlayer.SummonerSpells.SummonerSpellTwo.RawDisplayName, SummonerSpell.Unknown);
        }
    }
}
