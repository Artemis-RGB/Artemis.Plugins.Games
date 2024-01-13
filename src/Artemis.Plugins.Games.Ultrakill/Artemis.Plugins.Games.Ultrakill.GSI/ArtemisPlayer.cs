using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Artemis.Plugins.Games.Ultrakill.GSI
{
    public static class ArtemisPlayer
    {
        public static float Health;
        public static float Speed;
        public static float Stamina;
        public static bool Jumping;
        public static bool Dead;
        public static int CurrentGun;
        public static int CurrentGunVariation;

        public static string ToJson()
        {
            var b = new StringBuilder();
            b.Append('{');

            b.AppendTypeAndValue("health", Health);
            b.Append(',');
            b.AppendTypeAndValue("speed", Speed);
            b.Append(',');
            b.AppendTypeAndValue("stamina", Stamina);
            b.Append(',');
            b.AppendTypeAndValue("jumping", Jumping);
            b.Append(',');
            b.AppendTypeAndValue("dead", Dead);
            b.Append(',');
            b.AppendTypeAndValue("currentGun", CurrentGun);
            b.Append(',');
            b.AppendTypeAndValue("currentGunVariation", CurrentGunVariation);

            b.Append('}');

            return b.ToString();
        }
    }
}
