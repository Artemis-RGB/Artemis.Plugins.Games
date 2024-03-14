using System.Text.Json.Serialization;

namespace Artemis.Plugins.Games.Dota2.DataModels;

[JsonSerializable(typeof(Dota2DataModel))]
internal partial class JsonSourceContext : JsonSerializerContext;
