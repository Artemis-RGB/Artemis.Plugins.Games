using Artemis.Plugins.Games.EliteDangerous.Status;

namespace Artemis.Plugins.Games.EliteDangerous.DataModels
{
    public class HUD
    {
        public GuiPanel FocusedPanel { get; internal set; }
        public bool AnalysisModeActive { get; internal set; }
        public bool NightVisionActive { get; internal set; }
    }
}
