using System.Text;

namespace Artemis.Plugins.Games.Valheim.GSI.Models
{
    public class ArtemisLevelUpEvent : ISerializable
    {
        public Skills.SkillType SkillType;
        public float Level;

        public string ToJson()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('{');

            builder.AppendTypeAndValue(nameof(SkillType), SkillType.ToString());
            builder.Append(',');
            builder.AppendTypeAndValue(nameof(Level), Level);

            builder.Append('}');
            return builder.ToString();
        }
    }
}
