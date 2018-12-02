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
    //  ██████╗ ██████╗ ██╗██████╗  ██████╗ ███████╗
    //  ██╔══██╗██╔══██╗██║██╔══██╗██╔════╝ ██╔════╝
    //  ██████╔╝██████╔╝██║██║  ██║██║  ███╗█████╗  
    //  ██╔══██╗██╔══██╗██║██║  ██║██║   ██║██╔══╝  
    //  ██████╔╝██║  ██║██║██████╔╝╚██████╔╝███████╗
    //  ╚═════╝ ╚═╝  ╚═╝╚═╝╚═════╝  ╚═════╝ ╚══════╝
    //                                              
    /// <summary>
    /// Bridge connection and communication.
    /// </summary>
    public partial class Bridge
    {
           

        //// This will be shared by all instances... but oh well... will implement a better API later
        //private static bool _sentOnce = false;
        //private static WebSocket _ws;
        //private static string _url;


        ///// <summary>
        ///// Send a list of Actions to the Machina Bridge App to be streamed to a robot.
        ///// /// </summary>
        ///// <param name="actions">A program in the form of a list of Actions.</param>
        ///// <param name="url">The URL of the Machina Bridge App.</param>
        ///// <param name="connect">Connect to Machina Bridge App?</param>
        ///// <param name="send">Send Actions?</param>
        ///// <returns></returns>
        //[MultiReturn(new[] { "Connected?", "Sent?", "Instructions" })]
        //public static Dictionary<string, object> SendToBridge(List<MAction> actions, string url = "ws://127.0.0.1:6999/Bridge", bool connect = false, bool send = false)
        //{
        //    List<string> instructions = new List<string>();

        //    bool connectedResult;
        //    if (connect)
        //    {
        //        if (_ws == null || !_ws.IsAlive)
        //        {
        //            _ws = new WebSocket(url);
        //            _ws.Connect();
        //        }
        //        connectedResult = _ws.IsAlive;

        //        if (!connectedResult)
        //        {
        //            DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("Could not connect to Machina Bridge App");
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        if (_ws != null)
        //        {
        //            _ws.Close();
        //        }
        //        connectedResult = _ws.IsAlive;
        //    }

        //    string sentResult = "Nothing sent";
        //    if (send && connectedResult)
        //    {
        //        string ins = "";

        //        foreach (MAction a in actions)
        //        {
        //            // If attaching a tool, send the tool description first.
        //            // This is quick and dirty, a result of this component not taking the robot object as an input.
        //            // How coud this be improved...? Should tool creation be an action?
        //            if (a.type == Machina.ActionType.Attach)
        //            {
        //                ActionAttach aa = (ActionAttach)a;
        //                ins = aa.tool.ToInstruction();
        //                instructions.Add(ins);
        //                _ws.Send(ins);
        //            }

        //            ins = a.ToInstruction();
        //            instructions.Add(ins);
        //            _ws.Send(ins);
        //        }
        //        sentResult = "Sent!";
        //    }

        //    return new Dictionary<string, object>
        //    {
        //        { "Connected?", connectedResult },
        //        { "Sent?", sentResult },
        //        { "Instructions", instructions }
        //    };
        //}
    }
}
