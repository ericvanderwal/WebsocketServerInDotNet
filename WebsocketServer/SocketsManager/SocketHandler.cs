using System.Net.WebSockets;
using System.Threading.Tasks;

namespace WebsocketServer.SocketsManager
{
    public class SocketHandler
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
        
        
    }
}