using Artemis.Core.Modules;
using SkiaSharp;

namespace Artemis.Plugins.Games.RocketLeague.Module
{
    public class RocketLeagueTeam
    {
        public string Name { get; set; }
        public int Goals { get; set; }

        [DataModelIgnore]
        public RocketLeagueColor PrimaryColor { get; set; }
        public SKColor Primary => new(PrimaryColor.Red, PrimaryColor.Green, PrimaryColor.Blue);

        [DataModelIgnore]
        public RocketLeagueColor SecondaryColor { get; set; }
        public SKColor Secondary => new(SecondaryColor.Red, SecondaryColor.Green, SecondaryColor.Blue);

        [DataModelIgnore]
        public RocketLeagueColor FontColor { get; set; }
        public SKColor Font => new(FontColor.Red, FontColor.Green, FontColor.Blue);
    }
}