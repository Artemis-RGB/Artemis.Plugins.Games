using Artemis.Core;
using Artemis.Core.Services;
using Newtonsoft.Json.Linq;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Artemis.Core.ColorScience;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.Services
{
    public class ChampionColorService : IPluginService, IDisposable
    {
        private readonly PluginSetting<Dictionary<string, ColorSwatch>> _colorCache;
        private readonly PluginSetting<Dictionary<string, int>> _skinIdCache;
        private readonly HttpClient _httpClient;

        public ChampionColorService(PluginSettings pluginSettings)
        {
            _colorCache = pluginSettings.GetSetting("championColors", new Dictionary<string, ColorSwatch>());
            _skinIdCache = pluginSettings.GetSetting("skinIds", new Dictionary<string, int>());
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(5)
            };
        }

        public async Task<ColorSwatch> GetSwatch(string championShortName, int skinId)
        {
            if (string.IsNullOrWhiteSpace(championShortName))
                return new ColorSwatch();

            //we need this step tp get rid of chromas
            var baseSkinId = await GetBaseSkinIdAsync(championShortName, skinId);

            var key = GetDataDragonSplashUrl(championShortName, baseSkinId);
            lock (_colorCache)
            {
                if (_colorCache.Value.TryGetValue(key, out var s))
                    return s;
            }

            using Stream stream = await _httpClient.GetStreamAsync(key);
            using SKBitmap skbm = SKBitmap.Decode(stream);
            SKColor[] skClrs = ColorQuantizer.Quantize(skbm.Pixels, 256);

            ColorSwatch swatch = ColorQuantizer.FindAllColorVariations(skClrs);
            lock (_colorCache)
            {
                _colorCache.Value[key] = swatch;
                _colorCache.Save();
            }

            return swatch;
        }

        private async Task<int> GetBaseSkinIdAsync(string championShortName, int skinId)
        {
            const int SKIN_CLASSIFICATION_NONCHROMA = 1;
            const int SKIN_CLASSIFICATION_CHROMA = 2;
            string key = GetCommunityDragonSkinJsonUrl(championShortName, skinId);

            lock (_skinIdCache)
            {
                if (_skinIdCache.Value.TryGetValue(key, out var id))
                    return id;
            }

            string championSkinJson = await _httpClient.GetStringAsync(key);
            var championSkinInfo = JObject.Parse(championSkinJson);

            var usefulInfo = championSkinInfo.First.First;
            var skinClassificationJson = usefulInfo["skinClassification"];
            if (skinClassificationJson == null)
                throw new Exception();

            int skinClassification = (int)skinClassificationJson;
            int baseSkinId;
            if (skinClassification == SKIN_CLASSIFICATION_NONCHROMA)
            {
                baseSkinId = skinId;
            }
            else if (skinClassification == SKIN_CLASSIFICATION_CHROMA)
            {
                var parentId = usefulInfo["skinParent"];
                if (parentId == null)
                    throw new Exception();

                baseSkinId = (int)parentId;
            }
            else
            {
                throw new Exception();
            }

            lock (_skinIdCache)
            {
                _skinIdCache.Value[key] = baseSkinId;
                _skinIdCache.Save();
            }

            return baseSkinId;
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
                if (disposing)
                {
                    _httpClient.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
