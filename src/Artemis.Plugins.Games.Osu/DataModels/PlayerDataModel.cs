using System.Collections.Generic;
using System.Linq;
using Artemis.Core.Modules;
using OsuMemoryDataProvider.OsuMemoryModels.Direct;

namespace Artemis.Plugins.Games.Osu.DataModels;

public class PlayerDataModel : DataModel
{
    public double HPSmooth { get; set; }

    public double HP { get; set; }

    public double Accuracy { get; set; }

    public List<int> HitErrors { get; set; }

    public bool IsReplay { get; set; }
    
    public void Apply(Player player)
    {
        HPSmooth = player.HPSmooth;
        HP = player.HP;
        Accuracy = player.Accuracy;
        HitErrors = player.HitErrors.ToList();
        IsReplay = player.IsReplay;
    }
}