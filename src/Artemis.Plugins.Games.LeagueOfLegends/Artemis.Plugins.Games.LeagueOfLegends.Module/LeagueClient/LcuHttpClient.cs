using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient;

public sealed class LcuHttpClient : IDisposable
{
    private readonly HttpClientHandler _httpClientHandler;
    private readonly HttpClient _httpClient;
    private readonly string _authorization;

    public LcuHttpClient(int port, string password)
    {
        _authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"riot:{password}"));
        _httpClientHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (req, cert, chain, polErrs) =>
                RiotCertificateUtils.CertificateValidationCallback(req, cert, chain, polErrs)
        };
        _httpClient = new HttpClient(_httpClientHandler)
        {
            BaseAddress = new Uri($"https://127.0.0.1:{port}/")
        };
    }

    public async Task<string> GetAsync(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", _authorization);
        var response = await _httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public void Dispose()
    {
        _httpClientHandler.Dispose();
        _httpClient.Dispose();
    }
}