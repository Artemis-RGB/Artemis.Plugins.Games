using Artemis.Plugins.Games.Valheim.GSI.Models;
using BepInEx.Logging;
using System;
using System.IO;
using System.Timers;
using UnityEngine.Networking;
using SystemTimer = System.Timers.Timer;

namespace Artemis.Plugins.Games.Valheim.GSI
{
    public class ArtemisWebClient
    {
        private const string CONFIG_PATH = @"C:\ProgramData\Artemis\webserver.txt";
        private const string PLUGIN_GUID = "f2c7c7cc-0ef8-4836-aa81-1fedb6836892";

        private readonly ManualLogSource _logger;
        private readonly SystemTimer timer;
        private readonly string _baseUri;

        public ArtemisWebClient(ManualLogSource logger)
        {
            _logger = logger;

            if (!File.Exists(CONFIG_PATH))
                throw new FileNotFoundException("Artemis: Webserver file not found");

            string uri;
            try
            {
                uri = File.ReadAllText(CONFIG_PATH);
            }
            catch (IOException)
            {
                _logger.LogError("Artemis: Error reading webserver config file");
                throw;
            }

            _logger.LogInfo($"Found artemis web api uri: {uri}");

            var request = UnityWebRequest.Get($"{uri}plugins");
            try
            {
                request.SendWithTimeout(TimeSpan.FromMilliseconds(500));
            }
            catch (Exception e)
            {
                _logger.LogError("Artemis: Failed connecting to webserver");
                _logger.LogError(e);

                throw new Exception("Failed to connect to Artemis, exiting...");
            }

            _baseUri = $"{uri}plugins/{PLUGIN_GUID}";

            _logger.LogInfo("Connected to Artemis, starting timer.");

            timer = new SystemTimer(100);
            timer.Elapsed += OnTimerElapsed;
        }

        public void StartTimer() => timer.Start();
        public void StopTimer() => timer.Stop();

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            SendPlayerUpdate(Patches.PlayerUpdatePatch.Player);
            SendEnvironmentUpdate(Patches.EnvManUpdatePatch.Environment);
        }

        private void SendJson(string endpoint, string json)
        {
            try
            {
                UnityWebRequest request = UnityWebRequest.Put($"{_baseUri}/{endpoint}", json);
                request.method = "POST";
                request.SetRequestHeader("Content-Type", "application/json");
                request.SendWithTimeout(TimeSpan.FromMilliseconds(100));
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                _logger.LogError("Artemis: Stopping timer");
                StopTimer();
            }
        }

        private string lastPlayerJson;
        public void SendPlayerUpdate(ArtemisPlayer player)
        {
            var json = player.ToJson();
            if (json != lastPlayerJson)
            {
                lastPlayerJson = json;
                SendJson("player", lastPlayerJson);
            }
        }

        private string lastEnvironmentJson;
        public void SendEnvironmentUpdate(ArtemisEnvironment env)
        {
            var json = env.ToJson();
            if (json != lastEnvironmentJson)
            {
                lastEnvironmentJson = json;
                SendJson("environment", lastEnvironmentJson);
            }
        }

        public void SendEvent(string endpoint, string args)
        {
            SendJson(endpoint, args);
        }
    }
}
