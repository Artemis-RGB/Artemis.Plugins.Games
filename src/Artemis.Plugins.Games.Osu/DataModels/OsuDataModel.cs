using Artemis.Core.Modules;
using OsuMemoryDataProvider.OsuMemoryModels.Abstract;

namespace Artemis.Plugins.Games.Osu.DataModels;

public class OsuDataModel : DataModel
{
    public GeneralDataModel GeneralData { get; set; } = new();
    public PlayerDataModel Player { get; set; } = new();
    public BeatmapDataModel Beatmap { get; set; } = new();
    public RulesetPlayDataModel RulesetPlayData { get; set; } = new();
}

public class RulesetPlayDataModel : DataModel
{
    public string Username { get; set; }

    public int Mode { get; set; }

    public ushort MaxCombo { get; set; }

    public int Score { get; set; }

    public ushort Hit100 { get; set; }

    public ushort Hit300 { get; set; }

    public ushort Hit50 { get; set; }

    public ushort HitGeki { get; set; }

    public ushort HitKatu { get; set; }

    public ushort HitMiss { get; set; }

    public ushort Combo { get; set; }

    public void Apply(RulesetPlayData ruleset)
    {
        Username = ruleset.Username;
        Mode = ruleset.Mode;
        MaxCombo = ruleset.MaxCombo;
        Score = ruleset.Score;
        Hit100 = ruleset.Hit100;
        Hit300 = ruleset.Hit300;
        Hit50 = ruleset.Hit50;
        HitGeki = ruleset.HitGeki;
        HitKatu = ruleset.HitKatu;
        HitMiss = ruleset.HitMiss;
        Combo = ruleset.Combo;
    }
}