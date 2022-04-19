using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.GameDataModels;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels
{
    public class AbilityDataModel : DataModel
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public bool Learned => Level > 0;

        public void SetupMatch(Ability ability)
        {
            Name = ability.DisplayName;
        }

        public void Update(Ability ability)
        {
            Level = ability.AbilityLevel;
        }
    }
}
