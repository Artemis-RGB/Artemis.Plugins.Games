using Artemis.Core.Modules;

namespace Artemis.Plugins.Games.Osu.DataModels;

public class OsuDataModel : DataModel
{
    public GeneralDataModel GeneralData { get; set; } = new();
    public PlayerDataModel Player { get; set; } = new();
    public BeatmapDataModel Beatmap { get; set; } = new();
}