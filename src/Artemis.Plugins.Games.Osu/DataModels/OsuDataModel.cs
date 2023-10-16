using Artemis.Core.Modules;
using OsuMemoryDataProvider.OsuMemoryModels.Direct;

namespace Artemis.Plugins.Games.Osu.DataModels;

public class OsuDataModel : DataModel
{
    public GeneralDataModel GeneralData { get; set; } = new();
}

public class GeneralDataModel : DataModel
{
    public int RawStatus { get; set; }

    public int GameMode { get; set; }

    public int Retries { get; set; }

    public int AudioTime { get; set; }

    public double TotalAudioTime { get; set; }

    public bool ChatIsExpanded { get; set; }

    public int Mods { get; set; }

    public bool ShowPlayingInterface { get; set; }

    public string OsuVersion { get; set; }

    public void Apply(GeneralData generalData)
    {
        RawStatus = generalData.RawStatus;
        GameMode = generalData.GameMode;
        Retries = generalData.Retries;
        AudioTime = generalData.AudioTime;
        TotalAudioTime = generalData.TotalAudioTime;
        ChatIsExpanded = generalData.ChatIsExpanded;
        Mods = generalData.Mods;
        ShowPlayingInterface = generalData.ShowPlayingInterface;
        OsuVersion = generalData.OsuVersion;
    }
}