using System.Text.Json.Serialization;
using Artemis.Plugins.Games.CSGO.GameDataModels;

namespace Artemis.Plugins.Games.CSGO;

[JsonSerializable(typeof(RootGameData))]
internal partial class JsonSourceContext : JsonSerializerContext;
