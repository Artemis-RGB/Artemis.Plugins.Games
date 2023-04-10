using HarmonyLib;

namespace Artemis.Plugins.Games.Ultrakill.GSI.Patches
{
    [HarmonyPatch(typeof(HealthBar), "Update")]
    public static class Patches
    {
        public static float MovementSpeed { get; set; }
        public static float Health { get; set; }
        
        public static void Postfix(ref float ___hp)
        {
            Health = ___hp;
        }
    }

    [HarmonyPatch(typeof(NewMovement), "Update")]
    public static class PlayerIThink
    {
        public static void PostFix()
        {
            
        }
    }
}
