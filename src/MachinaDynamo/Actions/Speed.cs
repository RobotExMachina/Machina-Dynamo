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
    //  ███████╗██████╗ ███████╗███████╗██████╗ 
    //  ██╔════╝██╔══██╗██╔════╝██╔════╝██╔══██╗
    //  ███████╗██████╔╝█████╗  █████╗  ██║  ██║
    //  ╚════██║██╔═══╝ ██╔══╝  ██╔══╝  ██║  ██║
    //  ███████║██║     ███████╗███████╗██████╔╝
    //  ╚══════╝╚═╝     ╚══════╝╚══════╝╚═════╝ 
    //            
    partial class Actions
    {
        /// <summary>
        /// Increases the speed at which future actions will execute.
        /// </summary>
        /// <param name="speedInc">Speed increment in mm/s or deg/sec</param>
        /// <returns name="Action">Speed Action</returns>
        public static MAction Speed(double speedInc = 0) 
            => new ActionSpeed(speedInc, true);


        /// <summary>
        /// Sets the speed at which future actions will execute.
        /// </summary>
        /// <param name="speed">Speed value in mm/s or deg/sec</param>
        /// <returns name="Action">Speed Action</returns>
        public static MAction SpeedTo(double speed = 0)
        {
            if (speed < 0)
            {
                DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("The speed value cannot be negative");
                return null;
            }
            return new ActionSpeed(speed, false);
        }

    }
}
