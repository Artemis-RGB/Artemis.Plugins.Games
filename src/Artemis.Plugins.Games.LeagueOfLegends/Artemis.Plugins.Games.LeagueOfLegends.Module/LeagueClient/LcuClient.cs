using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient
{
    internal sealed class LcuClient : IDisposable
    {
        private const string RIOT_USERNAME = "riot";
        private readonly Uri _uri;
        private readonly ClientWebSocket _ws;
        private readonly CancellationTokenSource _cts;
        private readonly byte[] _buffer;
        private Task _readLoopTask;

        public event EventHandler<ILcuEvent> EventReceived;

        public LcuClient(LockfileData data)
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
            var payload = $"[{(int)opCode},\"{json}\"]";

            await _ws.SendAsync(Encoding.UTF8.GetBytes(payload), WebSocketMessageType.Text, true, _cts.Token);
        }

        private async Task ReadLoop()
        {
            while (!_cts.IsCancellationRequested && _ws.State == WebSocketState.Open)
            {
                try
                {
                    int bytesRead = 0;
                    ValueWebSocketReceiveResult result;
                    do
                    {
                        result = await _ws.ReceiveAsync(_buffer.AsMemory(bytesRead), _cts.Token);
                        bytesRead += result.Count;
                    } while (!result.EndOfMessage);

                    if (bytesRead == 0)
                        continue;

                    string data = Encoding.UTF8.GetString(_buffer.AsSpan(0, bytesRead));
                    var jArray = JArray.Parse(data);

                    //0 - opcode
                    //1 - which subscription

                    var opCode = (LcuOpcode)(int)jArray[0];
                    var eventName = jArray[1].ToString();
                    var lcuEvent = jArray[2].ToObject<ILcuEvent>();

                    switch (opCode)
                    {
                        case LcuOpcode.Event:
                            EventReceived?.Invoke(this, lcuEvent);
                            break;
                        default:
                            
                            break;
                    }
                }
                catch (Exception e)
                {

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
            Dispose(disposing: true);
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
}
