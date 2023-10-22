namespace Artemis.Plugins.Games.Osu.DataModels;

public enum OsuStatus
{
    Unknown = -2, // 0xFFFFFFFE
    NotRunning = -1, // 0xFFFFFFFF
    MainMenu = 0,
    EditingMap = 1,
    Playing = 2,
    GameShutdownAnimation = 3,
    SongSelectEdit = 4,
    SongSelect = 5,
    WIP_NoIdeaWhatThisIs = 6,
    ResultsScreen = 7,
    GameStartupAnimation = 10, // 0x0000000A
    MultiplayerRooms = 11, // 0x0000000B
    MultiplayerRoom = 12, // 0x0000000C
    MultiplayerSongSelect = 13, // 0x0000000D
    MultiplayerResultsscreen = 14, // 0x0000000E
    OsuDirect = 15, // 0x0000000F
    RankingTagCoop = 17, // 0x00000011
    RankingTeam = 18, // 0x00000012
    ProcessingBeatmaps = 19, // 0x00000013
    Tourney = 22, // 0x00000016
}