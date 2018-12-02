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
        // COMMON STATIC PROPERTIES SHARED BY ALL INSTANCES OF BRIDGE-NODES.
        // @TODO: This should be abstracted into instance objects, but oh well... 
        // I can't see a good example right now of using multiple listeners, 
        // except listenting to multiple Bridges...

        

        // Internal constructor
        internal Bridge() { }
    }
}