using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Artemis.Plugins.Games.Dota2.DataModels.Nodes;

/// <summary>
/// Enum for various player activities
/// </summary>
public enum PlayerActivity
{
    /// <summary>
    /// Undefined
    /// </summary>
    Undefined,

    /// <summary>
    /// In a menu
    /// </summary>
    Menu,

    /// <summary>
    /// In a game
    /// </summary>
    Playing
}

/// <summary>
/// Class representing player information
/// </summary>
[JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class PlayerDota2
{
    /// <summary>
    /// Player's steam ID
    /// </summary>
    [JsonProperty("steamid")]
    public string SteamId { get; set; } = "";

    /// <summary>
    /// Player's name
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Player's current activity state
    /// </summary>
    public PlayerActivity Activity { get; set; }

    /// <summary>
    /// Player's amount of kills
    /// </summary>
    public int Kills { get; set; }

    /// <summary>
    /// Player's amount of deaths
    /// </summary>
    public int Deaths { get; set; }

    /// <summary>
    /// Player's amount of assists
    /// </summary>
    public int Assists { get; set; }

    /// <summary>
    /// Player's amount of last hits
    /// </summary>
    public int LastHits { get; set; }

    /// <summary>
    /// Player's amount of denies
    /// </summary>
    public int Denies { get; set; }

    /// <summary>
    /// Player's killstreak
    /// </summary>
    public int KillStreak { get; set; }

    /// <summary>
    /// Player's team
    /// </summary>
    [JsonProperty("team_name")]
    public PlayerTeam Team { get; set; }

    /// <summary>
    /// Player's amount of gold
    /// </summary>
    public int Gold { get; set; }

    /// <summary>
    /// Player's amount of reliable gold
    /// </summary>
    public int GoldReliable { get; set; }

    /// <summary>
    /// Player's amount of unreliable gold
    /// </summary>
    public int GoldUnreliable { get; set; }

    /// <summary>
    /// PLayer's gold per minute
    /// </summary>
    [JsonProperty("gpm")]
    public int GoldPerMinute { get; set; }

    /// <summary>
    /// Player's experience per minute
    /// </summary>
    [JsonProperty("xpm")]
    public int ExperiencePerMinute { get; set; }
}