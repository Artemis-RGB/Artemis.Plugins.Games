using Artemis.Core;
using Artemis.Core.Modules;
using SkiaSharp;
using System.Collections.Generic;

namespace Artemis.Plugins.Games.Valheim.Module.DataModels
{
    public class ValheimDataModel : DataModel
    {
        public PlayerData Player { get; set; } = new PlayerData();
        public Enviroment Environment { get; set; } = new Enviroment();

        public DataModelEvent Teleport { get; } = new DataModelEvent();
        public DataModelEvent<SkillLevelUpEventArgs> SkillLevelUp { get; } = new DataModelEvent<SkillLevelUpEventArgs>();
        public DataModelEvent ForsakenActivated { get; } = new DataModelEvent();
    }

    public class SkillLevelUpEventArgs : DataModelEventArgs
    {
        public SkillType SkillType { get; set; }
        public float Level { get; set; }
    }

    public enum Biome
    {
        None = 0,
        Meadows = 1,
        Swamp = 2,
        Mountain = 4,
        BlackForest = 8,
        Plains = 16,
        AshLands = 32,
        DeepNorth = 64,
        Ocean = 256,
        Mistlands = 512,
        BiomesMax = 513
    }

    public class PlayerData
    {
        public float HealthCurrent { get; set; }
        public float HealthMax { get; set; }
        public float StaminaCurrent { get; set; }
        public float StaminaMax { get; set; }
        public float WeightCurrent { get; set; }
        public float WeightMax { get; set; }
        public bool InShelter { get; set; }
        public List<string> Effects { get; set; } = new List<string>();
        public string ForsakenPower { get; set; }
    }

    public class Enviroment
    {
        public bool IsWet { get; set; }
        public bool IsCold { get; set; }
        public bool IsDaylight { get; set; }
        public float WindAngle { get; set; }
        public Biome Biome { get; set; }
        public SKColor SunFogColor => new SKColor(SunFog.Red, SunFog.Green, SunFog.Blue);

        //SKColor can't be deserialized apparently so let's do this instead
        [DataModelIgnore]
        public TempColor SunFog { get; set; }
    }

    public struct TempColor
    {
        public byte Red;
        public byte Green;
        public byte Blue;
    }

    public enum SkillType
    {
        None = 0,
        Swords = 1,
        Knives = 2,
        Clubs = 3,
        Polearms = 4,
        Spears = 5,
        Blocking = 6,
        Axes = 7,
        Bows = 8,
        FireMagic = 9,
        FrostMagic = 10,
        Unarmed = 11,
        Pickaxes = 12,
        WoodCutting = 13,
        Jump = 100,
        Sneak = 101,
        Run = 102,
        Swim = 103,
        All = 999
    }
}