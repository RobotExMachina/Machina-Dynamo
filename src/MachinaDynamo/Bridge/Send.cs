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
    //  ███████╗███████╗███╗   ██╗██████╗ 
    //  ██╔════╝██╔════╝████╗  ██║██╔══██╗
    //  ███████╗█████╗  ██╔██╗ ██║██║  ██║
    //  ╚════██║██╔══╝  ██║╚██╗██║██║  ██║
    //  ███████║███████╗██║ ╚████║██████╔╝
    //  ╚══════╝╚══════╝╚═╝  ╚═══╝╚═════╝ 
    public partial class Bridge
    {
        /// <summary>
        /// Send a list of Actions to the Bridge.
        /// </summary>
        /// <param name="bridge">The (websocket) object managing connection to the Machina Bridge.</param>
        /// <param name="actions">A ist of Actions to send to the Bridge.</param>
        /// <param name="send">Send Actions?</param>
        /// <returns name="log">Status messages.</returns>
        /// <returns name="instructions">Streamed instructions.</returns>
        [MultiReturn(new[] {"log", "instructions"})]
        public static Dictionary<string, object> Send(object bridge, List<MAction> actions, bool send = false)
        {
            MachinaBridgeSocket ms = null;

            try
            {
                ms = bridge as MachinaBridgeSocket;
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

            List<string> instructions = new List<string>();

            string logMsg;

            int it = 0;
            if (send)
            {
                string ins = "";

                foreach (Machina.Action a in actions)
                {
                    ins = a.ToInstruction();
                    instructions.Add(ins);
                    ms.socket.Send(ins);
                    it++;
                }

                logMsg = it + " actions sent!";
            }
            else
            {
                logMsg = "Nothing sent";
            }

            return new Dictionary<string, object>
            {
                {"log", logMsg},
                {"instructions", instructions}
            };
        }
    }
}
