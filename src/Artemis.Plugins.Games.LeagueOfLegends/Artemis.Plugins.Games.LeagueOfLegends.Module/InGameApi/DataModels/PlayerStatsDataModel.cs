using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.GameDataModels;
using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels
{
    public class PlayerStatsDataModel : DataModel
    {
        public float AbilityHaste { get; set; }
        public float AbilityPower { get; set; }
        public float Armor { get; set; }
        public float ArmorPenetrationFlat { get; set; }
        public float ArmorPenetrationPercent { get; set; }
        public float AttackDamage { get; set; }
        public float AttackRange { get; set; }
        public float AttackSpeed { get; set; }
        public float BonusArmorPenetrationPercent { get; set; }
        public float BonusMagicPenetrationPercent { get; set; }
        public float CooldownReduction => AbilityHaste / (AbilityHaste + 100) * 100;
        public float CritChance { get; set; }
        public float CritDamagePercent { get; set; }
        public float HealthCurrent { get; set; }
        public float HealthMax { get; set; }
        public float HealthRegenRate { get; set; }
        public float HealShieldPower { get; set; }
        public float LifeSteal { get; set; }
        public float MagicLethality { get; set; }
        public float MagicPenetrationFlat { get; set; }
        public float MagicPenetrationPercent { get; set; }
        public float MagicResist { get; set; }
        public float MoveSpeed { get; set; }
        public float Omnivamp { get; set; }
        public float PhysicalLethality { get; set; }
        public float PhysicalVamp { get; set; }
        public float ResourceCurrent { get; set; }
        public float ResourceMax { get; set; }
        public float ResourceRegenRate { get; set; }
        public ResourceType ResourceType { get; set; }
        public float SpellVamp { get; set; }
        public float Tenacity { get; set; }

        public void SetupMatch(ChampionStats championStats)
        {
            ResourceType = ParseEnum<ResourceType>.TryParseOr(championStats.ResourceType, ResourceType.Unknown);
        }

        public void Update(ChampionStats championStats)
        {
            AbilityHaste = championStats.AbilityHaste;
            AbilityPower = championStats.AbilityPower;
            Armor = championStats.Armor;
            ArmorPenetrationFlat = championStats.ArmorPenetrationFlat;
            ArmorPenetrationPercent = championStats.ArmorPenetrationPercent * 100f;
            AttackDamage = championStats.AttackDamage;
            AttackRange = championStats.AttackRange;
            AttackSpeed = championStats.AttackSpeed;
            BonusArmorPenetrationPercent = championStats.BonusArmorPenetrationPercent * 100f;
            BonusMagicPenetrationPercent = championStats.BonusMagicPenetrationPercent * 100f;
            CritChance = championStats.CritChance * 100f;
            CritDamagePercent = championStats.CritDamage;
            HealthCurrent = championStats.CurrentHealth;
            HealthMax = championStats.MaxHealth;
            HealthRegenRate = championStats.HealthRegenRate;
            HealShieldPower = championStats.HealShieldPower;
            LifeSteal = championStats.LifeSteal * 100f;
            MagicLethality = championStats.MagicLethality;
            MagicPenetrationFlat = championStats.MagicPenetrationFlat;
            MagicPenetrationPercent = championStats.MagicPenetrationPercent * 100f;
            MagicResist = championStats.MagicResist;
            MoveSpeed = championStats.MoveSpeed;
            Omnivamp = championStats.Omnivamp;
            PhysicalLethality = championStats.PhysicalLethality;
            PhysicalVamp = championStats.PhysicalVamp;
            ResourceCurrent = championStats.ResourceValue;
            ResourceMax = championStats.ResourceMax;
            ResourceRegenRate = championStats.ResourceRegenRate;
            SpellVamp = championStats.SpellVamp;
            Tenacity = championStats.Tenacity * 100f;
        }
    }
}
