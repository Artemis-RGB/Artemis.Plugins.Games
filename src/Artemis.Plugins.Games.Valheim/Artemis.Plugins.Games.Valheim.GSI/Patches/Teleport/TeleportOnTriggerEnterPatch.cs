using HarmonyLib;

namespace Artemis.Plugins.Games.Valheim.GSI.Patches
{
    [HarmonyPatch(typeof(TeleportWorld), "Teleport")]
    public static class TeleportOnTriggerEnterPatch
    {
        public static void Postfix(ref TeleportWorld __instance, Player player)
        {
            ArtemisGsiPlugin.ArtemisWebClient.SendEvent("teleport", "{}");
        }
    }
}
