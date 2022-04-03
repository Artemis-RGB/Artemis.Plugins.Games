using Artemis.Plugins.Games.LeagueOfLegends.Module.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Artemis.Plugins.Games.LeagueOfLegends.Module.LeagueClient
{
    internal sealed class LcuClient : IDisposable
    {
        private readonly Uri _uri;
        private readonly ClientWebSocket _ws;
        private readonly byte[] _buffer;
        private readonly CancellationTokenSource _cts;
        private Task _readLoopTask;

        public event EventHandler<ILcuEvent> EventReceived;

        public LcuClient(LockfileData data)
        {
            _buffer = new byte[32 * 1024];
            _cts = new CancellationTokenSource();
            _uri = new Uri($"wss://localhost:{data.Port}");
            _ws = new ClientWebSocket();
            _ws.Options.Credentials = new NetworkCredential("riot", data.Password);
            _ws.Options.RemoteCertificateValidationCallback = (req, cert, chain, polErrs)
                => RiotCertificateUtils.CertificateValidationCallback(req, cert, chain, polErrs);
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
            while (_ws.State == WebSocketState.Open && !_cts.IsCancellationRequested)
            {
                Array.Fill<byte>(_buffer, 0);
                var result = await _ws.ReceiveAsync(_buffer, _cts.Token);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", _cts.Token);
                    return;
                }

                if (result.Count == 0)
                    continue;
                string data = null;
                try
                {
                    data = Encoding.UTF8.GetString(_buffer, 0, result.Count);
                    var jArray = JArray.Parse(data);

                    var opCode = (LcuOpcode)(int)jArray[0];
                    var eventName = jArray[1].ToString();
                    var lcuEvent = jArray[2].ToObject<ILcuEvent>();

                    switch (opCode)
                    {
                        case LcuOpcode.Welcome:
                            break;
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
                    _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None).Wait();
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
