using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;

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
using System.Windows.Interop;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;
using WebSocketSharp.Server;

namespace MachinaDynamo
{
    //   █████╗  ██████╗████████╗██╗ ██████╗ ███╗   ██╗                   
    //  ██╔══██╗██╔════╝╚══██╔══╝██║██╔═══██╗████╗  ██║                   
    //  ███████║██║        ██║   ██║██║   ██║██╔██╗ ██║                   
    //  ██╔══██║██║        ██║   ██║██║   ██║██║╚██╗██║                   
    //  ██║  ██║╚██████╗   ██║   ██║╚██████╔╝██║ ╚████║                   
    //  ╚═╝  ╚═╝ ╚═════╝   ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝                   
    //                                                                    
    //  ███████╗██╗  ██╗███████╗ ██████╗██╗   ██╗████████╗███████╗██████╗ 
    //  ██╔════╝╚██╗██╔╝██╔════╝██╔════╝██║   ██║╚══██╔══╝██╔════╝██╔══██╗
    //  █████╗   ╚███╔╝ █████╗  ██║     ██║   ██║   ██║   █████╗  ██║  ██║
    //  ██╔══╝   ██╔██╗ ██╔══╝  ██║     ██║   ██║   ██║   ██╔══╝  ██║  ██║
    //  ███████╗██╔╝ ██╗███████╗╚██████╗╚██████╔╝   ██║   ███████╗██████╔╝
    //  ╚══════╝╚═╝  ╚═╝╚══════╝ ╚═════╝ ╚═════╝    ╚═╝   ╚══════╝╚═════╝ 
    //                                                                    
    public partial class Bridge
    {

        // Outputs: use persistent data to avoid null outcomes when most recent message is other than ActionExecuted.
        private static List<int> _ids = new List<int>();
        private static List<string> _logMsgs = new List<string>();
        private static List<string> _instructions = new List<string>();
        private static List<int> _pendingExecutionOnDevice = new List<int>();
        private static List<int> _pendingExecutionTotal = new List<int>();
        private static List<Plane> _tcps = new List<Plane>();
        private static List<double?[]> _axes = new List<double?[]>();
        private static List<double?[]> _externalAxes = new List<double?[]>();

        // Parsing stuff and buffers.
        private static readonly string EVENT_NAME = "action-executed";
        private static JavaScriptSerializer ser = new JavaScriptSerializer();
        private static List<dynamic> objBuff = new List<dynamic>();



        [MultiReturn(new[] {"log", "lastAction", "actionTCP", "actionAxes", "actionExternalAxes", "pendingActions", "pendingActionsOnDevice" })]
        public static Dictionary<string, object> ActionExecuted(List<string> bridgeMessages, bool onlyMostRecent = false)
        {
            
            if (bridgeMessages == null || bridgeMessages.Count == 0)
            {
                _logMsgs.Clear();
                _logMsgs.Add("No new messages to parse");
                return GetOutputs();
            }

            ParseIncomingMessages(bridgeMessages, onlyMostRecent);
            
            return GetOutputs();
        }



        private static bool ParseIncomingMessages(List<string> messages, bool keepOnlyMostRecent)
        {
            objBuff.Clear();
            _logMsgs.Clear();

            dynamic json;
            var it = 0;
            foreach (var msg in messages)
            {
                try
                {
                    json = ser.Deserialize<dynamic>(msg);

                    // Keep this message if it is of the right type and has not been received before. 
                    if (json["event"].Equals(EVENT_NAME) && !_ids.Contains(json["id"]))
                    {
                        it++;
                        objBuff.Add(json);
                    }
                }
                catch
                {
                    _logMsgs.Add("Received badly formatted message");
                }
            }

            if (objBuff.Count == 0)
            {
                _logMsgs.Add("No new messages to parse.");
                return false;
            }
            else
            {
                _logMsgs.Add("Received " + it + " messages.");
            }

            // If received any matching event not recorded before, clear all outputs, 
            // parse incoming messages and output them. 
            if (keepOnlyMostRecent)
            {
                dynamic last = objBuff.Last();
                objBuff.Clear();
                objBuff.Add(last);
            }

            ClearOutputs();
            it = 0;
            foreach (var obj in objBuff)
            {
                ParseMessage(obj);
                it++;
            }

            _logMsgs.Add("Parsed " + it + " messages.");

            return it != 0;
        }

        private static bool ParseMessage(dynamic json)
        {
            try
            {
                // @TODO: make this more programmatic, tie it to ActionExecutedArgs props
                _ids.Add(json["id"]);
                _instructions.Add(json["last"]);

                var pos = Machina.Utilities.Conversion.NullableDoublesFromObjects(json["pos"]);
                var ori = Machina.Utilities.Conversion.NullableDoublesFromObjects(json["ori"]);

                if (pos == null || ori == null)
                {
                    _tcps.Add(null);
                }
                else
                {
                    Plane p = Plane.ByOriginXAxisYAxis(
                        Autodesk.DesignScript.Geometry.Point.ByCoordinates(Convert.ToDouble(pos[0]),
                            Convert.ToDouble(pos[1]), Convert.ToDouble(pos[2])),
                        Autodesk.DesignScript.Geometry.Vector.ByCoordinates(Convert.ToDouble(ori[0]),
                            Convert.ToDouble(ori[1]), Convert.ToDouble(ori[2])),
                        Autodesk.DesignScript.Geometry.Vector.ByCoordinates(Convert.ToDouble(ori[3]), Convert.ToDouble(ori[4]), Convert.ToDouble(ori[5]))
                        );
                    _tcps.Add(p);
                }

                _axes.Add(Machina.Utilities.Conversion.NullableDoublesFromObjects(json["axes"]));
                _externalAxes.Add(Machina.Utilities.Conversion.NullableDoublesFromObjects(json["extax"]));

                _pendingExecutionTotal.Add(json["pendTot"]);
                _pendingExecutionOnDevice.Add(json["pendDev"]);
            }
            catch
            {
                _logMsgs.Add("Received badly formatted message");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Clear all output lists except log messages.
        /// </summary>
        private static void ClearOutputs()
        {
            _ids.Clear();
            _instructions.Clear();
            _pendingExecutionOnDevice.Clear();
            _pendingExecutionTotal.Clear();
            _tcps.Clear();
            _axes.Clear();
            _externalAxes.Clear();
        }

        // Build the object and return it. 
        private static Dictionary<string, object> GetOutputs() 
            => new Dictionary<string, object> {
            { "log", _logMsgs },
            { "lastAction", _instructions},
            { "actionTCP", _tcps},
            { "actionAxes", _axes},
            { "actionExternalAxes", _externalAxes},
            { "pendingActions", _pendingExecutionTotal},
            { "pendingActionsOnDevice", _pendingExecutionOnDevice}
        };

    }
}
