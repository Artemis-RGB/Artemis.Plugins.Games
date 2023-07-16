using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient.LcuEvents;
using Newtonsoft.Json;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient;

internal sealed class LcuWsClient : IDisposable
{
    private const string RIOT_USERNAME = "riot";
    private readonly Uri _uri;
    private readonly ClientWebSocket _ws;
    private readonly CancellationTokenSource _cts;
    private readonly byte[] _buffer;
    private Task? _readLoopTask;

    public event EventHandler<LcuEvent>? EventReceived;
    public event EventHandler<LcuEvent>? MessageReceived; 
    public event EventHandler<Exception>? Error;

    public LcuWsClient(Lockfile data)
    {
        _cts = new CancellationTokenSource();
        _uri = new Uri($"wss://localhost:{data.Port}");
        _ws = new ClientWebSocket();
        _ws.Options.Credentials = new NetworkCredential(RIOT_USERNAME, data.Password);
        _ws.Options.RemoteCertificateValidationCallback = (req, cert, chain, polErrs)
            => RiotCertificateUtils.CertificateValidationCallback(req, cert, chain, polErrs);
        _buffer = new byte[1024 * 1024];
    }

    public async Task Connect()
    {
        await _ws.ConnectAsync(_uri, _cts.Token);
        if (_ws.State != WebSocketState.Open)
            throw new Exception("Could not connect to LCU");

        _readLoopTask = Task.Run(ReadLoop);
    }

    public async Task Subscribe(string eventName)
    {
        await Send(LcuOpcode.Subscribe, eventName);
    }

    public async Task Unsubscribe(string eventName)
    {
        await Send(LcuOpcode.Unsubscribe, eventName);
    }

    private async Task Send(LcuOpcode opCode, string json)
    {
        if (_ws.State != WebSocketState.Open)
            throw new Exception("Could not send message, LCU is not connected");
        
        var payload = $"[{(int)opCode},\"{json}\"]";

        await _ws.SendAsync(Encoding.UTF8.GetBytes(payload), WebSocketMessageType.Text, true, _cts.Token);
    }

    private async Task ReadLoop()
    {
        while (!_cts.IsCancellationRequested && _ws.State == WebSocketState.Open)
        {
            try
            {
                var bytesRead = 0;
                ValueWebSocketReceiveResult result;
                do
                {
                    result = await _ws.ReceiveAsync(_buffer.AsMemory(bytesRead), _cts.Token);
                    bytesRead += result.Count;
                } while (!result.EndOfMessage);

                if (bytesRead == 0)
                    continue;

                var data = Encoding.UTF8.GetString(_buffer.AsSpan(0, bytesRead));
                var jArray = JArray.Parse(data);
                
                var opCode = (LcuOpcode)jArray[0].Value<int>();
                var eventName = jArray[1].ToString();
                var lcuEventString = jArray[2].ToString();
                
                var lcuEvent = JsonConvert.DeserializeObject<LcuEvent>(lcuEventString);
                
                if (lcuEvent == null)
                    throw new Exception("Could not deserialize LCU event");

                switch (opCode)
                {
                    case LcuOpcode.Event:
                        EventReceived?.Invoke(this, lcuEvent);
                        break;
                    default:
                        MessageReceived?.Invoke(this, lcuEvent);
                        break;
                }
            }
            catch (Exception e)
            {
                Error?.Invoke(this, e);
            }
        }
    }

    #region IDisposable Support

    private bool disposedValue;

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _cts.Cancel();
                _readLoopTask.Wait();
                _readLoopTask.Dispose();
                _cts.Dispose();
                try
                {
                    _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None).Wait();
                }
                catch
                {
                    //oops
                }

                _ws.Dispose();
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

internal enum LcuOpcode
{
    Welcome = 0,
    Prefix = 1,
    Call = 2,
    CallResult = 3,
    CallError = 4,
    Subscribe = 5,
    Unsubscribe = 6,
    Publish = 7,
    Event = 8
}