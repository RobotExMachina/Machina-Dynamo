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
    //   ██████╗ ██████╗  █████╗ ██╗   ██╗███████╗██╗   ██╗ █████╗ ██████╗ ██████╗ 
    //  ██╔════╝ ██╔══██╗██╔══██╗██║   ██║██╔════╝╚██╗ ██╔╝██╔══██╗██╔══██╗██╔══██╗
    //  ██║  ███╗██████╔╝███████║██║   ██║█████╗   ╚████╔╝ ███████║██████╔╝██║  ██║
    //  ██║   ██║██╔══██╗██╔══██║╚██╗ ██╔╝██╔══╝    ╚██╔╝  ██╔══██║██╔══██╗██║  ██║
    //  ╚██████╔╝██║  ██║██║  ██║ ╚████╔╝ ███████╗   ██║   ██║  ██║██║  ██║██████╔╝
    //   ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝  ╚═══╝  ╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═════╝ 
    //                                                                             
    // Legacy components, deprecated or on their way to change. 
    partial class Actions
    {
        /// <summary>
        /// LET'S PUT A PIN ON THIS ONE... SOME DEEP CHANGES NEED TO HAPPEN AT CORE, SO THIS IS STAYING HERE
        /// FOR LEGACY PURPOSES, AND WILL BE REWRITTEN AS SOON AS CORE BRINGS IN A NEW MODEL
        /// </summary>
        /// <summary>
        /// Sets the coordinate system that will be used for future relative actions. This can be "global" or "world" (default) to refer to the system's global reference coordinates, or "local" to refer to the device's local reference frame. For example, for a robotic arm, the "global" coordinate system will be the robot's base, and the "local" one will be the coordinates of the tool tip.
        /// </summary>
        /// <param name="type">"global" or "local"</param>
        /// <returns>Set Reference Coordinate System Action</returns>
        [IsVisibleInDynamoLibrary(false)]
        public static MAction Coordinates(string type = "global")
        {
            ReferenceCS refcs;
            type = type.ToLower();
            if (type.Equals("global") || type.Equals("world"))
            {
                refcs = ReferenceCS.World;
            }
            else if (type.Equals("local"))
            {
                refcs = ReferenceCS.Local;
            }
            else
            {
                DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("Invalid reference coordinate system");
                return null;
            }

            return new ActionCoordinates(refcs);
        }

        /// <summary>
        /// Turn digital output off. Alias for `WriteDigital(ioNum, false)` 
        /// </summary>
        /// <param name="ioNum"></param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(false)]
        public static MAction TurnOff(int ioNum)
        {
            //return new ActionIODigital(ioNum, false);
            DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("Deprecated component: use WriteDigital(ioNum, false) instead");
            return null;
        }

        /// <summary>
        /// Turn digital output on. Alias for `WriteDigital(ioNum, true)`
        /// </summary>
        /// <param name="ioNum">Digital pin number</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(false)]
        public static MAction TurnOn(int ioNum)
        {
            //return new ActionIODigital(ioNum, true);
            DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("Deprecated component: use WriteDigital(ioNum, true) instead");
            return null;
        }
    }
}
