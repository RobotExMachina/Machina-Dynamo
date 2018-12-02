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
    //  ██╗     ██╗███████╗████████╗███████╗███╗   ██╗
    //  ██║     ██║██╔════╝╚══██╔══╝██╔════╝████╗  ██║
    //  ██║     ██║███████╗   ██║   █████╗  ██╔██╗ ██║
    //  ██║     ██║╚════██║   ██║   ██╔══╝  ██║╚██╗██║
    //  ███████╗██║███████║   ██║   ███████╗██║ ╚████║
    //  ╚══════╝╚═╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═══╝
    //                                                
    public partial class Bridge
    {
        /// <summary>
        /// Listen to messages from the Machina Bridge.
        /// </summary>
        /// <param name="Bridge">The (websocket) object managing connection to the Machina Bridge.</param>
        /// <returns name="log">Status messages.</returns>
        /// <returns name="bridgeMessages">Last messags received from the bridge. Will only update once per received message.</returns>
        [CanUpdatePeriodically(true)]
        [MultiReturn(new[] { "log", "bridgeMessages" })]
        public static Dictionary<string, object> Listen(object Bridge)
        {
            MachinaBridgeSocket ms = null;

            try
            {
                ms = Bridge as MachinaBridgeSocket;
            }
            catch
            {
                DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("Invalid Bridge object.");
                return null;
            }

            if (ms == null || ms.socket == null || !ms.socket.IsAlive)
            {
                DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("Invalid Bridge connection.");
                return null;
            }

            List<string> msgBuffer;
            string logMsg;
            int size = ms.BufferSize();
            if (size > 0)
            {
                logMsg = "Received " + size + " messages.";
                msgBuffer = ms.FetchBuffer(true);
            }
            else
            {
                logMsg = "No messages received.";
                msgBuffer = new List<string>();
            }
        
            return new Dictionary<string, object>
            {
                {"log", logMsg},
                {"bridgeMessages", msgBuffer}
            };
        }
    }
}
