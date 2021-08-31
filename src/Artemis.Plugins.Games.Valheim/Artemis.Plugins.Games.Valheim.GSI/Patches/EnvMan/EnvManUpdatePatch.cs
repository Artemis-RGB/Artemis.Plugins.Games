using Artemis.Plugins.Games.Valheim.GSI.Models;
using HarmonyLib;
using System;
using UnityEngine;

namespace Artemis.Plugins.Games.Valheim.GSI.Patches
{
    [HarmonyPatch(typeof(EnvMan), "Update")]
    public static class EnvManUpdatePatch
    {
        public static readonly ArtemisEnvironment Environment = new ArtemisEnvironment();
        public static void Postfix(ref EnvMan __instance)
        {
            Environment.IsWet = __instance.IsWet();

            Vector3 vecDir = __instance.GetWindDir();
            float rad = Mathf.Atan2(vecDir.x, vecDir.z);
            Environment.WindAngle = (rad >= 0 ? rad : (2 * (float)Math.PI + rad)) * 360 / (2 * (float)Math.PI);

            Environment.Biome = __instance.GetCurrentBiome();
            Environment.IsCold = __instance.IsCold();
            Environment.IsDaylight = __instance.IsDaylight();
            Environment.SunFog = __instance.GetSunFogColor();
        }
    }
}
