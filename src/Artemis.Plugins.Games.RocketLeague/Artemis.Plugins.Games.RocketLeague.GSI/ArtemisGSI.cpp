#include "pch.h"
#include "ArtemisGSI.h"
#include <optional>

constexpr auto WEBSERVER_FILE_PATH = "C:\\ProgramData\\Artemis\\webserver.txt";
constexpr auto ARTEMIS_PLUGIN_GUID = "945dc0aa-7ee3-47ec-9be6-f378fb7cb7b0";
constexpr auto UPDATE_ENDPOINT = "/plugins/945dc0aa-7ee3-47ec-9be6-f378fb7cb7b0/update";
constexpr auto PLUGINLIST_ENDPOINT = "/plugins/endpoints";

BAKKESMOD_PLUGIN(ArtemisGSI, "Artemis RGB integration", plugin_version, PLUGINTYPE_THREADED)

void ArtemisGSI::onLoad()
{
	auto endpointOptional = FindEndpoint();
	if (!endpointOptional.has_value())
	{
		cvarManager->executeCommand("sleep 0; plugin unload artemisgsi");
		return;
	}

	auto endpoint = endpointOptional.value();
	cvarManager->log("Artemis client starting with url " + endpoint);

	artemisClient = std::make_unique<httplib::Client>(endpoint.data());
	artemisClient->set_connection_timeout(0, 50000);

	auto testRequest = artemisClient->Get(PLUGINLIST_ENDPOINT);
	if (!testRequest)
	{
		cvarManager->log("Test request failed, Artemis not running.");
		cvarManager->executeCommand("sleep 0; plugin unload artemisgsi");
		return;
	}
	if (testRequest.value().body.find(ARTEMIS_PLUGIN_GUID) == std::string::npos)
	{
		cvarManager->log("Test request failed, Artemis does not have the required plugin loaded.");
		cvarManager->executeCommand("sleep 0; plugin unload artemisgsi");
		return;
	}

	canSendUpdates = true;
	std::thread t(&ArtemisGSI::StartLoop, this);
	t.detach();
}

void ArtemisGSI::onUnload()
{
	canSendUpdates = false;
}

std::optional<std::string> ArtemisGSI::FindEndpoint() {
	if (!std::filesystem::exists(WEBSERVER_FILE_PATH)) {
		cvarManager->log("Artemis webserver file not found, exiting...");
		return std::nullopt;
	}

	std::ifstream file(WEBSERVER_FILE_PATH);
	if (!file.good()) {
		cvarManager->log("Artemis webserver file read error, exiting...");
		return std::nullopt;
	}

	std::string artemisServerUri;
	if (!std::getline(file, artemisServerUri)) {
		cvarManager->log("Artemis webserver file was empty, exiting...");
		return std::nullopt;
	}

	//remove trailing slash
	if (artemisServerUri.back() == '/')
		artemisServerUri.pop_back();

	return artemisServerUri;
}

void ArtemisGSI::StartLoop() {
	cvarManager->log("Starting dedicated thread...");

	while (canSendUpdates) {
		gameWrapper->Execute(std::bind(&ArtemisGSI::Update, this));
		std::string newJson = GameState.GetJson().dump();
		if (newJson != json) {
			json = newJson;
			SendToArtemis(UPDATE_ENDPOINT, json.data());
		}
		std::this_thread::sleep_for(std::chrono::milliseconds(1000 / 30));
	}

	cvarManager->log("Stopping dedicated thread...");
}

void ArtemisGSI::Update() {
	ServerWrapper wrapper = GetCurrentGameWrapper();

	if (!wrapper.IsNull())
		this->UpdateGameState(wrapper);
	else
		GameState.Reset();
}

void ArtemisGSI::SendToArtemis(const char* endpoint, const char* data) {
	auto response = artemisClient->Post(endpoint, data, "application/json");

	if (!response) {
		cvarManager->log("Error sending data to Artemis, stopping...");
		cvarManager->executeCommand("sleep 0; plugin unload artemisgsi");
	}
}

void ArtemisGSI::UpdateGameState(ServerWrapper wrapper)
{
	GameState.Match.Overtime = wrapper.GetbOverTime();
	GameState.Match.UnlimitedTime = wrapper.GetbUnlimitedTime();
	if (!GameState.Match.Overtime && !GameState.Match.UnlimitedTime)
		GameState.Match.Time = wrapper.GetSecondsRemaining();
	else
		GameState.Match.Time = -1;

	auto teams = wrapper.GetTeams();
	for (int i = 0; i < teams.Count(); i++) {

		auto team = teams.Get(i);
		if (team.IsNull()) continue;

		GameState.Match.Teams[i].Goals = team.GetScore();

		if (!team.GetSanitizedTeamName().IsNull())
			GameState.Match.Teams[i].Name = team.GetSanitizedTeamName().ToString();
		else
			GameState.Match.Teams[i].Name = i == 0 ? "Blue" : "Orange";

		GameState.Match.Teams[i].PrimaryColor.SetValues(team.GetPrimaryColor());
		GameState.Match.Teams[i].SecondaryColor.SetValues(team.GetSecondaryColor());
		GameState.Match.Teams[i].FontColor.SetValues(team.GetFontColor());
	}

	auto localController = this->gameWrapper->GetPlayerController();
	if (!localController.IsNull()) {
		auto local = localController.GetPRI();
		if (!local.IsNull()) {
			GameState.Player.Score = local.GetMatchScore();
			GameState.Player.Goals = local.GetMatchGoals();
			GameState.Player.Assists = local.GetMatchAssists();
			GameState.Player.Saves = local.GetMatchSaves();
			GameState.Player.Shots = local.GetMatchShots();
			GameState.Player.BallTouches = local.GetBallTouches();
			GameState.Player.CarTouches = local.GetCarTouches();
			GameState.Player.Demolishes = local.GetMatchDemolishes();
			GameState.Player.Team = local.GetTeamNum();

			auto car = local.GetCar();
			if (!car.IsNull()) {
				auto boostComponent = car.GetBoostComponent();
				if (!boostComponent.IsNull()) {
					GameState.Car.Boost = boostComponent.GetCurrentBoostAmount();
				}
				else {
					GameState.Car.Boost = -1;
				}

				GameState.Car.SuperSonic = car.GetbSuperSonic();
				GameState.Car.IsOnWall = car.IsOnWall();
				GameState.Car.IsOnGround = car.IsOnGround();
				GameState.Car.SpeedKph = (car.GetVelocity().magnitude() * 0.036f) + 0.5f;
			}
		}
		else {
			GameState.Player.Score = -1;
			GameState.Player.Goals = -1;
			GameState.Player.Assists = -1;
			GameState.Player.Saves = -1;
			GameState.Player.Shots = -1;
			GameState.Player.Team = -1;
		}
	}

	GameSettingPlaylistWrapper playlistWrapper = wrapper.GetPlaylist();
	if (!playlistWrapper.IsNull())
		GameState.Match.Playlist = playlistWrapper.GetPlaylistId();
	else
		GameState.Match.Playlist = -1;
}

ServerWrapper ArtemisGSI::GetCurrentGameWrapper() {
	if (gameWrapper->IsSpectatingInOnlineGame()) {
		GameState.Status = GameStatus::Spectate;
		return gameWrapper->GetOnlineGame();
	}
	else  if (gameWrapper->IsInCustomTraining()) {
		GameState.Status = GameStatus::Training;
		return gameWrapper->GetGameEventAsServer();
	}
	else if (gameWrapper->IsInReplay()) {
		GameState.Status = GameStatus::Replay;
		return gameWrapper->GetGameEventAsReplay();
	}
	else if (gameWrapper->IsInFreeplay()) {
		GameState.Status = GameStatus::Freeplay;
		return gameWrapper->GetGameEventAsServer();
	}
	else if (gameWrapper->IsInOnlineGame()) {
		GameState.Status = GameStatus::InGame;
		return gameWrapper->GetOnlineGame();
	}
	else if (gameWrapper->IsInGame()) {
		GameState.Status = GameStatus::InGame;
		return gameWrapper->GetGameEventAsServer();
	}
	else {
		GameState.Status = GameStatus::Menu;
		return NULL;
	}
}