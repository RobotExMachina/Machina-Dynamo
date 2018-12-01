using System;
using System.Collections.Generic;
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


namespace MachinaDynamo
{
    //   █████╗ ████████╗████████╗ █████╗  ██████╗██╗  ██╗████████╗ ██████╗  ██████╗ ██╗     
    //  ██╔══██╗╚══██╔══╝╚══██╔══╝██╔══██╗██╔════╝██║  ██║╚══██╔══╝██╔═══██╗██╔═══██╗██║     
    //  ███████║   ██║      ██║   ███████║██║     ███████║   ██║   ██║   ██║██║   ██║██║     
    //  ██╔══██║   ██║      ██║   ██╔══██║██║     ██╔══██║   ██║   ██║   ██║██║   ██║██║     
    //  ██║  ██║   ██║      ██║   ██║  ██║╚██████╗██║  ██║   ██║   ╚██████╔╝╚██████╔╝███████╗
    //  ╚═╝  ╚═╝   ╚═╝      ╚═╝   ╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝   ╚═╝    ╚═════╝  ╚═════╝ ╚══════╝
    //                                                                           
    partial class Actions
    {
        /// <summary>
        /// Attach a Tool to the flange of the Robot, replacing whichever tool was on it before. Note that the Tool Center Point (TCP) will be translated/rotated according to the tool change.
        /// </summary>
        /// <param name="toolName">The name of the Tool to attach to the Robot flange. Please note it should have been previously defined with "DefineTool"</param>
        /// <returns name="Action">AttachTool Action</returns>
        public static MAction AttachTool(string toolName)
        {
            if (toolName == "")
            {
                DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("No tool name specified...");
                return null;
            }
            return new ActionAttachTool(toolName);
        } 

    }
}
