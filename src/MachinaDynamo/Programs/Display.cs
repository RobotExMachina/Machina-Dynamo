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
    //  ██████╗ ██╗███████╗██████╗ ██╗      █████╗ ██╗   ██╗
    //  ██╔══██╗██║██╔════╝██╔══██╗██║     ██╔══██╗╚██╗ ██╔╝
    //  ██║  ██║██║███████╗██████╔╝██║     ███████║ ╚████╔╝ 
    //  ██║  ██║██║╚════██║██╔═══╝ ██║     ██╔══██║  ╚██╔╝  
    //  ██████╔╝██║███████║██║     ███████╗██║  ██║   ██║   
    //  ╚═════╝ ╚═╝╚══════╝╚═╝     ╚══════╝╚═╝  ╚═╝   ╚═╝   
    //                                                      
    public partial class Programs
    {
        /// <summary>
        /// Returns a human-readable representation of a list of Actions.
        /// </summary>
        /// <param name="actions">The list of Actions that conforms a program</param>
        /// <returns name="program">Human-readable representation of the program</returns>
        public static List<string> Display(List<MAction> actions)
        {
            List<string> program = new List<string>();

            foreach (MAction a in actions)
            {
                program.Add(a.ToString());
            }

            return program;
        }
    }
}
