using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Artemis.Plugins.Games.Dota2.DataModels.Nodes;

/// <summary>
/// Enum list for each Game State
/// </summary>
public enum DOTA_GameState
{
    /// <summary>
    /// Undefined
    /// </summary>
    Undefined,

    /// <summary>
    /// Disconnected
    /// </summary>
    DOTA_GAMERULES_STATE_DISCONNECT,

    /// <summary>
    /// Game is in progress
    /// </summary>
    DOTA_GAMERULES_STATE_GAME_IN_PROGRESS,

    /// <summary>
    /// Players are currently selecting heroes
    /// </summary>
    DOTA_GAMERULES_STATE_HERO_SELECTION,

    /// <summary>
    /// Game is starting
    /// </summary>
    DOTA_GAMERULES_STATE_INIT,

    /// <summary>
    /// Game is ending
    /// </summary>
    DOTA_GAMERULES_STATE_LAST,

    /// <summary>
    /// Game has ended, post game scoreboard
    /// </summary>
    DOTA_GAMERULES_STATE_POST_GAME,

    /// <summary>
    /// Game has started, pre game preparations
    /// </summary>
    DOTA_GAMERULES_STATE_PRE_GAME,

    /// <summary>
    /// Players are selecting/banning heroes
    /// </summary>
    DOTA_GAMERULES_STATE_STRATEGY_TIME,

    /// <summary>
    /// Waiting for everyone to connect and load
    /// </summary>
    DOTA_GAMERULES_STATE_WAIT_FOR_PLAYERS_TO_LOAD,

    /// <summary>
    /// Game is a custom game
    /// </summary>
    DOTA_GAMERULES_STATE_CUSTOM_GAME_SETUP
}

/// <summary>
/// Enum list for each player team
/// </summary>
public enum PlayerTeam
{
    /// <summary>
    /// Undefined
    /// </summary>
    Undefined,

    /// <summary>
    /// No team
    /// </summary>
    None,

    /// <summary>
    /// Dire team
    /// </summary>
    Dire,

    /// <summary>
    /// Radiant team
    /// </summary>
    Radiant
}
    
/// <summary>
/// Class representing information about the map
/// </summary>
[JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class MapDota2
{
    /// <summary>
    /// Name of the current map
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Match ID of the current game
    /// </summary>
    [JsonProperty("matchid")]
    public long MatchId { get; set; }

    /// <summary>
    /// Game time
    /// </summary>
    public int GameTime { get; set; }

    /// <summary>
    /// Clock time (time shown at the top of the game hud)
    /// </summary>
    public int ClockTime { get; set; }

    /// <summary>
    /// A boolean representing whether it is daytime
    /// </summary>
    public bool Daytime { get; set; }

    /// <summary>
    /// A boolean representing whether Nightstalker forced night time
    /// </summary>
    [JsonProperty("nightstalker_night")]
    public bool NightstalkerNight { get; set; }

    /// <summary>
    /// Current game state
    /// </summary>
    public DOTA_GameState GameState { get; set; }

    /// <summary>
    /// The winning team
    /// </summary>
    [JsonProperty("Win_team")]
    public PlayerTeam WiningTeam { get; set; }

    /// <summary>
    /// The name of the custom game
    /// </summary>
    [JsonProperty("customgamename")]
    public string CustomGameName { get; set; }

    /// <summary>
    /// The cooldown on ward purchases
    /// </summary>
    [JsonProperty("ward_purchase_cooldown")]
    public int Ward_Purchase_Cooldown { get; set; }
}