using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.GameDataModels;
using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi;

public sealed class LolInGameApiClient : IDisposable
{
    private const string BASE_URI = "https://127.0.0.1:2999/liveclientdata";
    private readonly HttpClient _httpClient;
    private readonly HttpClientHandler _httpClientHandler;
    private readonly JsonSerializerSettings _debugSerializerSettings;

    public LolInGameApiClient()
    {
        _httpClientHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (req, cert, chain, polErrs) =>
                RiotCertificateUtils.CertificateValidationCallback(req, cert, chain, polErrs)
        };
        _httpClient = new HttpClient(_httpClientHandler)
        {
            Timeout = TimeSpan.FromMilliseconds(75)
        };
        _debugSerializerSettings = new JsonSerializerSettings()
        {
            MissingMemberHandling = MissingMemberHandling.Error,
            ContractResolver = new RequireObjectPropertiesContractResolver()
        };
    }

    private async Task<T> GetAndDeserialize<T>(string endpoint)
    {
        var response = await _httpClient.GetStringAsync(endpoint);
#if DEBUG
        return JsonConvert.DeserializeObject<T>(response, _debugSerializerSettings);
#else
            return JsonConvert.DeserializeObject<T>(response);
#endif
    }

    public Task<string> GetActivePlayerNameAsync()
    {
        return _httpClient.GetStringAsync($"{BASE_URI}/activeplayername");
    }

    public Task<RootGameData> GetAllGameDataAsync()
    {
        return GetAndDeserialize<RootGameData>($"{BASE_URI}/allgamedata");
    }

    public Task<ActivePlayer> GetActivePlayerAsync()
    {
        return GetAndDeserialize<ActivePlayer>($"{BASE_URI}/activeplayer");
    }

    public Task<Abilities> GetActivePlayerAbilitiesAsync()
    {
        return GetAndDeserialize<Abilities>($"{BASE_URI}/activeplayerabilities");
    }

    public Task<FullRunes> GetActivePlayerRunesAsync()
    {
        return GetAndDeserialize<FullRunes>($"{BASE_URI}/activeplayerrunes");
    }

    public Task<List<AllPlayer>> GetPlayerListAsync()
    {
        return GetAndDeserialize<List<AllPlayer>>($"{BASE_URI}/playerlist");
    }

    public Task<List<LolEvent>> GetEventsAsync()
    {
        return GetAndDeserialize<List<LolEvent>>($"{BASE_URI}/eventdata");
    }

    public Task<GameStats> GetGameStatsAsync()
    {
        return GetAndDeserialize<GameStats>($"{BASE_URI}/gamestats");
    }

    public Task<Scores> GetPlayerScoresAsync(string summonerName)
    {
        return GetAndDeserialize<Scores>($"{BASE_URI}/playerscores?summonerName={summonerName}");
    }

    public Task<SummonerSpells> GetPlayerSummonerSpellsAsync(string summonerName)
    {
        return GetAndDeserialize<SummonerSpells>($"{BASE_URI}/playersummonerspells?summonerName={summonerName}");
    }

    public Task<Runes> GetPlayerRunesAsync(string summonerName)
    {
        return GetAndDeserialize<Runes>($"{BASE_URI}/playermainrunes?summonerName={summonerName}");
    }

    public Task<List<Item>> GetPlayerItemsAsync(string summonerName)
    {
        return GetAndDeserialize<List<Item>>($"{BASE_URI}/playeritems?summonerName={summonerName}");
    }

    #region IDisposable

    private bool disposedValue;

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _httpClient?.Dispose();
                _httpClientHandler?.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}

public class RequireObjectPropertiesContractResolver : DefaultContractResolver
{
    protected override JsonObjectContract CreateObjectContract(Type objectType)
    {
        var contract = base.CreateObjectContract(objectType);
        contract.ItemRequired = Required.Always;
        return contract;
    }
}