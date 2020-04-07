#if DEBUG

using Forge.Net.WebSockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASPNETCoreMVCwithReact.Codes
{
    public class BrowserRefresh : WebSocketHandler
    {

        private const string mExpectedMsg = "REFRESH";

        public BrowserRefresh(WebSocketManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = WebSocketConnectionManager.GetIdBySocket(socket);
            if (result.Count == mExpectedMsg.Length)
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                if (mExpectedMsg.Equals(message))
                {
                    await BroadcastMessageToAllAsync(mExpectedMsg, socketId);
                }
            }
            else
            {
                await socket.CloseAsync(WebSocketCloseStatus.PolicyViolation, "Closed by BrowserRefresh", CancellationToken.None);
            }
        }

    }

}

#endif
