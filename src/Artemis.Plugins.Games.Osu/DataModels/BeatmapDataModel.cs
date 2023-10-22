using Artemis.Core.Modules;
using OsuMemoryDataProvider.OsuMemoryModels.Direct;

namespace Artemis.Plugins.Games.Osu.DataModels;

public class BeatmapDataModel : DataModel
{
    public int Id { get; set; }

    public int SetId { get; set; }

    public string MapString { get; set; }

    public string FolderName { get; set; }

    public string OsuFileName { get; set; }

    public string Md5 { get; set; }

    public float Ar { get; set; }

    public float Cs { get; set; }

    public float Hp { get; set; }

    public float Od { get; set; }

    public short Status { get; set; }

    public void Apply(CurrentBeatmap beatmap)
    {
        Id = beatmap.Id;
        SetId = beatmap.SetId;
        MapString = beatmap.MapString;
        FolderName = beatmap.FolderName;
        OsuFileName = beatmap.OsuFileName;
        Md5 = beatmap.Md5;
        Ar = beatmap.Ar;
        Cs = beatmap.Cs;
        Hp = beatmap.Hp;
        Od = beatmap.Od;
        Status = beatmap.Status;
    }
}