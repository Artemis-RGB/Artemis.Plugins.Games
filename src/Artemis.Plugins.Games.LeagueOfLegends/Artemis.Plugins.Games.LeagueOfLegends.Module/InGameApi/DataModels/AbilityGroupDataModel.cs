using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.GameDataModels;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels
{
    public class AbilityGroupDataModel : DataModel
    {
        public AbilityDataModel Q { get; } = new();
        public AbilityDataModel W { get; } = new();
        public AbilityDataModel E { get; } = new();
        public AbilityDataModel R { get; } = new();

        public void Apply(Abilities abilities)
        {
            Q.Apply(abilities.Q);
            W.Apply(abilities.W);
            E.Apply(abilities.E);
            R.Apply(abilities.R);
        }
    }
}
