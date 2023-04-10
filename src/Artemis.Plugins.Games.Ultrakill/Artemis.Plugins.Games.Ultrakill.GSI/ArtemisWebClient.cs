using BepInEx.Logging;
using System;
using System.IO;
using System.Timers;
using UnityEngine;
using UnityEngine.Networking;
using SystemTimer = System.Timers.Timer;

namespace Artemis.Plugins.Games.Ultrakill.GSI
{
    public class ArtemisWebClient
    {
        private const string CONFIG_PATH = @"C:\ProgramData\Artemis\webserver.txt";
        private const string PLUGIN_GUID = "ef19ca95-9716-406a-a708-b73c81dbc859";

        private readonly SystemTimer timer;
        private readonly string _baseUri;

        public ArtemisWebClient()
        {
            if (!File.Exists(CONFIG_PATH))
                throw new FileNotFoundException("Artemis: Webserver file not found");

            string uri;
            try
            {
                uri = File.ReadAllText(CONFIG_PATH);
            }
            catch (IOException)
            {
                Debug.Log("Artemis: Error reading webserver config file");
                throw;
            }

            Debug.Log($"Found artemis web api uri: {uri}");

            var request = UnityWebRequest.Get($"{uri}plugins");
            try
            {
                request.SendWithTimeout(TimeSpan.FromMilliseconds(500));
            }
            catch (Exception e)
            {
                Debug.Log("Artemis: Failed connecting to webserver");
                Debug.Log(e);

                throw new Exception("Failed to connect to Artemis, exiting...");
            }

            _baseUri = $"{uri}plugins/{PLUGIN_GUID}";

            Debug.Log("Connected to Artemis, starting timer.");

            timer = new SystemTimer(100);
            timer.Elapsed += OnTimerElapsed;
        }

        public void StartTimer() => timer.Start();
        public void StopTimer() => timer.Stop();

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Debug.Log("meme");
            SendJson("update", ArtemisPlayer.ToJson());
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
                Debug.Log(e);
                Debug.Log("Artemis: Stopping timer");
                StopTimer();
            }
        }

        public void SendEvent(string endpoint, string args)
        {
            SendJson(endpoint, args);
        }
    }
}
