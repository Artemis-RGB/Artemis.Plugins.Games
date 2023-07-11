using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.GameDataModels;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels;

public class AbilityDataModel : DataModel
{
    public string RawName { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public bool Learned => Level > 0;

    public void Update(Ability ability)
    {
        RawName = ability.RawDisplayName;
        Name = ability.DisplayName;
        Level = ability.AbilityLevel;
    }
}