using Artemis.Core.Modules;
using Artemis.Plugins.Games.LeagueOfLegends.GameDataModels;
using System;

namespace Artemis.Plugins.Games.LeagueOfLegends.DataModels
{
    public class AbilityDataModel : DataModel
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public bool Learned => Level > 0;

        public void Apply(Ability ability)
        {
            Name = ability.DisplayName;
            Level = ability.AbilityLevel;
        }
    }
}
