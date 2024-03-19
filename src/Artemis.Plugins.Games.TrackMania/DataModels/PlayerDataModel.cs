using Artemis.Plugins.Games.TrackMania.Telemetry;
using SkiaSharp;

namespace Artemis.Plugins.Games.TrackMania.DataModels;

public class PlayerDataModel
{
    public bool IsLocalPlayer { get; set; }
    public string Trigram { get; set; }
    public string DossardNumber { get; set; }
    public SKColor FavoriteColor { get; set; }
    public string UserName { get; set; }

    public void Apply(in SPlayerState Player)
    {
        IsLocalPlayer = Player.IsLocalPlayer;
        Trigram = Player.Trigram;
        DossardNumber = Player.DossardNumber;
        FavoriteColor = SKColor.FromHsv(360f * Player.Hue, 100, 100);
        UserName = Player.UserName;
    }
}