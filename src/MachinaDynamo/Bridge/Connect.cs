using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Interfaces;
using Autodesk.DesignScript.Geometry;

using Machina;
using MAction = Machina.Action;
using MPoint = Machina.Point;
using MVector = Machina.Vector;
using MOrientation = Machina.Orientation;
using MTool = Machina.Tool;
using System.IO;
using System.Collections.Generic;
using System;

using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;
using WebSocketSharp.Server;

namespace MachinaDynamo
{
    //   ██████╗ ██████╗ ███╗   ██╗███╗   ██╗███████╗ ██████╗████████╗
    //  ██╔════╝██╔═══██╗████╗  ██║████╗  ██║██╔════╝██╔════╝╚══██╔══╝
    //  ██║     ██║   ██║██╔██╗ ██║██╔██╗ ██║█████╗  ██║        ██║   
    //  ██║     ██║   ██║██║╚██╗██║██║╚██╗██║██╔══╝  ██║        ██║   
    //  ╚██████╗╚██████╔╝██║ ╚████║██║ ╚████║███████╗╚██████╗   ██║   
    //   ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═══╝╚══════╝ ╚═════╝   ╚═╝   
    /// <summary>
    /// Bridge connection and communication.
    /// </summary>
    public partial class Bridge
    {
        // The main socket object to handle communication and message buffering.
        private static MachinaBridgeSocket _ms;
        
        /// <summary>
        /// Establish connection with the Machina Bridge.
        /// </summary>
        /// <param name="url">The URL of the Machina Bridge App. Use default unless you know what you are doing ;)</param>
        /// <param name="name">The name of this connecting client.</param>
        /// <param name="connect">Connect to Machina Bridge App?</param>
        /// <returns name="log">Status messages.</returns>
        /// <returns name="Bridge">The (websocket) object managing connection to the Machina Bridge.</returns>
        [MultiReturn(new[] {"log", "Bridge"})]
        public static Dictionary<string, object> Connect(string url = "ws://127.0.0.1:6999/Bridge",
            string name = "DynamoBIM", bool connect = false)
        {
            url += "?name=" + name;

            _ms = _ms ?? new MachinaBridgeSocket(name);

            bool connectedResult = false;
            string logMsg = "Not connected";

            // @TODO: move all socket management inside the wrapper
            if (connect)
            {
                if (_ms.socket == null)
                {
                    _ms.socket = new WebSocket(url);
                }

                if (!_ms.socket.IsAlive)
                {
                    _ms.socket.Connect();
                    _ms.socket.OnMessage += Socket_OnMessage;
                    _ms.socket.OnClose += Socket_OnClose;
                }

                connectedResult = _ms.socket.IsAlive;

                if (connectedResult)
                {
                    logMsg = "Connected to Machina Bridge";
                }
                else
                {
                    DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("Could not connect to Machina Bridge");
                    return null;
                }
            }
            else
            {
                if (_ms.socket != null)
                {
                    _ms.socket.Close(CloseStatusCode.Normal, "k thx bye!");
                    _ms.socket = null;
                    _ms.Flush();

                    logMsg = "Disconnected from the bridge";
                }
                connectedResult = false;
            }

            return new Dictionary<string, object>
            {
                {"log", logMsg},
                {"Bridge", connectedResult ? _ms : null}
            };
        }

        [IsVisibleInDynamoLibrary(false)]
        private static void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            _ms.Log(e.Data);
        }

        [IsVisibleInDynamoLibrary(false)]
        private static void Socket_OnClose(object sender, CloseEventArgs e)
        {
            // Was getting duplicate logging when connecting/disconneting/connecting again...
            // When closing, remove all handlers.
            // Apparently, this is safe (although not thread-safe) even if no handlers were attached: https://stackoverflow.com/a/7065771/1934487
            _ms.socket.OnMessage -= Socket_OnMessage;
            _ms.socket.OnClose -= Socket_OnClose;
        }
        
    }
}
