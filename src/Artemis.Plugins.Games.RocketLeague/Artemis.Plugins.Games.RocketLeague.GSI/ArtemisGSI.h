#pragma once

#include "bakkesmod/plugin/bakkesmodplugin.h"
#include "bakkesmod/plugin/pluginwindow.h"
#include "ArtemisGame.h"
#include "lib/httplib.h"
#include "version.h"
#include <optional>

constexpr auto plugin_version = stringify(VERSION_MAJOR) "." stringify(VERSION_MINOR) "." stringify(VERSION_PATCH) "." stringify(VERSION_BUILD);

class ArtemisGSI: public BakkesMod::Plugin::BakkesModPlugin/*, public BakkesMod::Plugin::PluginWindow*/
{
	virtual void onLoad();
	virtual void onUnload();

	ArtemisGame GameState;
	std::unique_ptr<httplib::Client> artemisClient;
	std::string json;
	bool canSendUpdates;

	std::optional<std::string> FindEndpoint();
	void StartLoop();
	void Update();
	void SendToArtemis(const char* endpoint, const char* data);
	void UpdateGameState(ServerWrapper wrapper);
	ServerWrapper GetCurrentGameWrapper();

	// Inherited via PluginWindow
	/*

	bool isWindowOpen_ = false;
	bool isMinimized_ = false;
	std::string menuTitle_ = "ArtemisGSI";

	virtual void Render() override;
	virtual std::string GetMenuName() override;
	virtual std::string GetMenuTitle() override;
	virtual void SetImGuiContext(uintptr_t ctx) override;
	virtual bool ShouldBlockInput() override;
	virtual bool IsActiveOverlay() override;
	virtual void OnOpen() override;
	virtual void OnClose() override;
	
	*/
};

