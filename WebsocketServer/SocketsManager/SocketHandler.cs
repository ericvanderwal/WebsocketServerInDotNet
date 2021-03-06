using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebsocketServer.SocketsManager
{
    public abstract class SocketHandler
    {
        public ConnectionManager Connections { get; set; }

        public SocketHandler(ConnectionManager connectionManager)
        {
            Connections = connectionManager;
        }

        public virtual async Task OnConnected(WebSocket socket)
        {
            await Task.Run(() => { Connections.AddSocket(socket); });
        }

        public virtual async Task OnDisConnect(WebSocket socket)
        {
            await Connections.RemoveWebsocketAsync(Connections.GetId(socket));
        }

        public async Task SendMessage(WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open) return;
            if (string.IsNullOrWhiteSpace(message)) return;

            await socket.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(message), 0, message.Length),
                WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task SendMessage(string id, string message)
        {
            await SendMessage(Connections.GetSocketsById(id), message);
        }

        public async Task SendMessageAll(string message)
        {
            foreach (var con in Connections.GetAllConnections())
            {
                await SendMessage(con.Value, message);
            }
        }

        public abstract Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}