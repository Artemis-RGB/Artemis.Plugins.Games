using System.Text;

namespace Artemis.Plugins.Games.Valheim.GSI.Models
{
    public class ArtemisTeleportEvent : ISerializable
    {
        public string Tag;

        public string ToJson()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('{');

            builder.AppendTypeAndValue(nameof(Tag), Tag);

            builder.Append('}');
            return builder.ToString();
        }
    }
}
