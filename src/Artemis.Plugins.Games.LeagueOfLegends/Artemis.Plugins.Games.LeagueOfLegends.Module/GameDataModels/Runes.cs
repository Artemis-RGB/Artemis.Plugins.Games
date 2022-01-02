namespace Artemis.Plugins.Games.LeagueOfLegends.GameDataModels
{
    public class Runes
    {
        public Rune Keystone { get; set; } = new();
        public Rune PrimaryRuneTree { get; set; } = new();
        public Rune SecondaryRuneTree { get; set; } = new();
    }
}
