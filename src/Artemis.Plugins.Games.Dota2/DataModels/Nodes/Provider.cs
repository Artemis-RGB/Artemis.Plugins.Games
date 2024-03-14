
using System.Text.Json.Serialization;

namespace Artemis.Plugins.Games.Dota2.DataModels.Nodes;

/// <summary>
/// Information about the provider of this GameState
/// </summary>
public class ProviderDota2
{
    /// <summary>
    /// Game name
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Game's Steam AppID
    /// </summary>
    [JsonPropertyName("appid")]
    public int AppId { get; set; }

    /// <summary>
    /// Game's version
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// Current timestamp
    /// </summary>
    [JsonPropertyName("timestamp")]
    public int TimeStamp { get; set; }
}