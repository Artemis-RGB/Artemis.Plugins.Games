using Artemis.Core;
using Artemis.Core.Services;
using Newtonsoft.Json.Linq;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Artemis.Core.ColorScience;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.Services;

public class ChampionColorService : IPluginService, IDisposable
{
    private readonly PluginSetting<Dictionary<string, ColorSwatch>> _colorCache;
    private readonly PluginSetting<Dictionary<string, int>> _skinIdCache;
    private readonly HttpClient _httpClient;
    private readonly SemaphoreSlim _semaphore;

    public ChampionColorService(PluginSettings pluginSettings)
    {
        _colorCache = pluginSettings.GetSetting("championColors", new Dictionary<string, ColorSwatch>());
        _skinIdCache = pluginSettings.GetSetting("skinIds", new Dictionary<string, int>());
        _httpClient = new()
        {
            Timeout = TimeSpan.FromSeconds(5)
        };
        _semaphore = new(1, 1);
    }

    public async Task<ColorSwatch> GetSwatch(string championShortName, int skinId)
    {
        if (string.IsNullOrWhiteSpace(championShortName))
            throw new ArgumentNullException(nameof(championShortName));

        await _semaphore.WaitAsync();

        try
        {
            //we need this step tp get rid of chromas
            var baseSkinId = await GetBaseSkinIdAsync(championShortName, skinId);

            var key = GetDataDragonSplashUrl(championShortName, baseSkinId);

            if (_colorCache.Value!.TryGetValue(key, out var s))
                return s;

            await using var stream = await _httpClient.GetStreamAsync(key);
            using var skBitmap = SKBitmap.Decode(stream);
            var skClrs = ColorQuantizer.Quantize(skBitmap.Pixels, 256);

            var swatch = ColorQuantizer.FindAllColorVariations(skClrs, true);
            _colorCache.Value[key] = swatch;
            _colorCache.Save();

            return swatch;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private async Task<int> GetBaseSkinIdAsync(string championShortName, int skinId)
    {
        const int SKIN_CLASSIFICATION_NONCHROMA = 1;
        const int SKIN_CLASSIFICATION_CHROMA = 2;
        var key = GetCommunityDragonSkinJsonUrl(championShortName, skinId);

        if (_skinIdCache.Value!.TryGetValue(key, out var id))
            return id;

        var championSkinJson = await _httpClient.GetStringAsync(key);
        var championSkinInfo = JObject.Parse(championSkinJson);
        var usefulInfo = championSkinInfo.First?.First;
        var skinClassification = usefulInfo?.Value<int>("skinClassification");
        if (skinClassification == null)
            throw new Exception("Skin classification is null.");

        var baseSkinId = skinClassification switch
        {
            SKIN_CLASSIFICATION_NONCHROMA => skinId,
            SKIN_CLASSIFICATION_CHROMA => usefulInfo?.Value<int>("skinParent"),
            _ => throw new Exception("Unknown skin classification.")
        };
        
        if (!baseSkinId.HasValue)
            throw new Exception("Base skin id is null.");

        _skinIdCache.Value[key] = baseSkinId.Value;
        _skinIdCache.Save();

        return baseSkinId.Value;
    }

    private static string GetDataDragonSplashUrl(string championShortName, int skinId)
    {
        const string BASE_URL = "http://ddragon.leagueoflegends.com/cdn/img/champion/splash/";
        return $"{BASE_URL}{championShortName}_{skinId}.jpg";
    }

    private static string GetCommunityDragonSkinJsonUrl(string championShortName, int skinId)
    {
        const string BASE_URL = "https://raw.communitydragon.org/latest/game/data/characters/";
        return $"{BASE_URL}{championShortName.ToLowerInvariant()}/skins/skin{skinId}.bin.json";
    }

    #region IDisposable Support

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing) _httpClient.Dispose();

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}
