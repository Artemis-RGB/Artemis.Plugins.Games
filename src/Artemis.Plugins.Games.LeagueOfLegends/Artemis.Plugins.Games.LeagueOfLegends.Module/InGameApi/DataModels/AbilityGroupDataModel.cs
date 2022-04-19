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

        public void SetupMatch(Abilities abilities)
        {
            Q.SetupMatch(abilities.Q);
            W.SetupMatch(abilities.W);
            E.SetupMatch(abilities.E);
            R.SetupMatch(abilities.R);
        }

        public void Update(Abilities abilities)
        {
            Q.Update(abilities.Q);
            W.Update(abilities.W);
            E.Update(abilities.E);
            R.Update(abilities.R);
        }
    }
}
