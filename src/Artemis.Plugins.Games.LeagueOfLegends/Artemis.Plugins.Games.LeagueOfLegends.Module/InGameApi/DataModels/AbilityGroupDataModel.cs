using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.GameDataModels;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels;

public class AbilityGroupDataModel : DataModel
{
    public AbilityDataModel Q { get; } = new();
    public AbilityDataModel W { get; } = new();
    public AbilityDataModel E { get; } = new();
    public AbilityDataModel R { get; } = new();

    public void Update(Abilities abilities)
    {
        Q.Update(abilities.Q);
        W.Update(abilities.W);
        E.Update(abilities.E);
        R.Update(abilities.R);
    }
}