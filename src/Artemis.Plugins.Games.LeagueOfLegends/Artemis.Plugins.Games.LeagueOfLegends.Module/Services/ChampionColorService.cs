using Artemis.Core;
using Artemis.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artemis.Plugins.Games.LeagueOfLegends.Module.InGameApi.DataModels.Enums;
using System.Net.Http;
using SkiaSharp;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.Services
{
    public class ChampionColorService : IPluginService, IDisposable
    {
        private readonly IColorQuantizerService _colorQuantizerService;
        private readonly PluginSetting<Dictionary<string, ColorSwatch>> _colorCache;
        private readonly HttpClient _httpClient;

        public ChampionColorService(IColorQuantizerService colorQuantizerService, PluginSettings pluginSettings)
        {
            _colorQuantizerService = colorQuantizerService;
            _colorCache = pluginSettings.GetSetting("championColors", new Dictionary<string, ColorSwatch>());
            _httpClient = new HttpClient();
        }

        public Task<ColorSwatch> GetSwatch(string championShortName)
        {
            return GetSwatch(championShortName, 0);
        }

        public async Task<ColorSwatch> GetSwatch(string championShortName, int skinId)
        {
            var key = GetKey(championShortName, skinId);
            lock (_colorCache)
            {
                if (_colorCache.Value.TryGetValue(key, out var s))
                    return s;
            }

            using var stream = await _httpClient.GetStreamAsync(key);
            using SKBitmap skbm = SKBitmap.Decode(stream);
            SKColor[] skClrs = _colorQuantizerService.Quantize(skbm.Pixels, 256);

            ColorSwatch swatch;
            lock (_colorCache)
            {
                swatch = _colorQuantizerService.FindAllColorVariations(skClrs);
                _colorCache.Value[key] = swatch;
                _colorCache.Save();
            }

            return swatch;
        }

        private static string GetKey(string championShortName, int skinId)
        {
            //return the league of legends splash image for the given champion and skin id
            const string BASE_URL = "http://ddragon.leagueoflegends.com/cdn/img/champion/splash/";
            return $"{BASE_URL}{championShortName}_{skinId}.jpg";
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
