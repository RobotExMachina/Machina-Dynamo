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
    //   █████╗ ██████╗ ███╗   ███╗ █████╗ ███╗   ██╗ ██████╗ ██╗     ███████╗
    //  ██╔══██╗██╔══██╗████╗ ████║██╔══██╗████╗  ██║██╔════╝ ██║     ██╔════╝
    //  ███████║██████╔╝██╔████╔██║███████║██╔██╗ ██║██║  ███╗██║     █████╗  
    //  ██╔══██║██╔══██╗██║╚██╔╝██║██╔══██║██║╚██╗██║██║   ██║██║     ██╔══╝  
    //  ██║  ██║██║  ██║██║ ╚═╝ ██║██║  ██║██║ ╚████║╚██████╔╝███████╗███████╗
    //  ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚══════╝╚══════╝
    //                                                                        
    partial class Actions
    {
        // Relative ArmAngle is still not supported by Machina.

        /// <summary>
        /// Set the value of the arm-angle parameter. This value represents the planar offset around the 7th axis for 7-dof robotic arms.
        /// </summary>
        /// <param name="angle">Angular value in degrees.</param>
        /// <returns name="Action">ArmAngle Action</returns>
        public static MAction ArmAngleTo(double angle) 
            => new ActionArmAngle(angle, false);
    }
}
