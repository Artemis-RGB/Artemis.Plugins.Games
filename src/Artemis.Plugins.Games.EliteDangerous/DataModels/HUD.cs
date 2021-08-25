using Artemis.Plugins.Modules.EliteDangerous.Status;

namespace Artemis.Plugins.Modules.EliteDangerous.DataModels
{
    public class HUD
    {
        public GuiPanel FocusedPanel { get; internal set; }
        public bool AnalysisModeActive { get; internal set; }
        public bool NightVisionActive { get; internal set; }
    }
}
