using Artemis.Plugins.Games.Valheim.GSI.Models;
using HarmonyLib;

namespace Artemis.Plugins.Games.Valheim.GSI.Patches
{
    [HarmonyPatch(typeof(Player), "Update")]
    public static class PlayerUpdatePatch
    {
        public static readonly ArtemisPlayer Player = new ArtemisPlayer();

        public static void Postfix(ref Player __instance)
        {
            if (__instance.IsOwner())
            {
                Player.HealthCurrent = __instance.GetHealth();
                Player.HealthMax = __instance.GetMaxHealth();
                Player.StaminaCurrent = __instance.GetStamina();
                Player.StaminaMax = __instance.GetMaxStamina();
                Player.WeightCurrent = __instance.GetInventory().GetTotalWeight();
                Player.WeightMax = __instance.GetMaxCarryWeight();
                Player.InShelter = __instance.InShelter();
                Player.Effects = __instance.GetSEMan().GetStatusEffects().ConvertAll(se => se.name);
                Player.ForsakenPower = __instance.GetGuardianPowerName();
            }
        }
    }

    [HarmonyPatch(typeof(Player), "OnSkillLevelup")]
    public static class PlayerOnSkillLevelupPatch
    {
        public static void Postfix(Skills.SkillType skill, float level)
        {
            ArtemisGsiPlugin.ArtemisWebClient.SendEvent("levelUp", new ArtemisLevelUpEvent { SkillType = skill, Level = level }.ToJson());
        }
    }

    [HarmonyPatch(typeof(Player), "StartGuardianPower")]
    public static class PlayerStartGuardianPowerPatch
    {
        public static void Postfix(ref Player __instance, ref bool __result)
        {
            if (__result)
            {
                ArtemisGsiPlugin.ArtemisWebClient.SendEvent("forsakenActivated", "{}");
            }
        }
    }

}
