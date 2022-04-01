using Artemis.Core;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.LolEventArgs
{
    public class MultikillEventArgs : DataModelEventArgs
    {
        public string KillerName { get; set; }
        public int KillStreak { get; set; }//TODO: replace this with double, triple etc?
    }
}
