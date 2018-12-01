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
    //  ██████╗ ██████╗ ███████╗ ██████╗██╗███████╗██╗ ██████╗ ███╗   ██╗
    //  ██╔══██╗██╔══██╗██╔════╝██╔════╝██║██╔════╝██║██╔═══██╗████╗  ██║
    //  ██████╔╝██████╔╝█████╗  ██║     ██║███████╗██║██║   ██║██╔██╗ ██║
    //  ██╔═══╝ ██╔══██╗██╔══╝  ██║     ██║╚════██║██║██║   ██║██║╚██╗██║
    //  ██║     ██║  ██║███████╗╚██████╗██║███████║██║╚██████╔╝██║ ╚████║
    //  ╚═╝     ╚═╝  ╚═╝╚══════╝ ╚═════╝╚═╝╚══════╝╚═╝ ╚═════╝ ╚═╝  ╚═══╝
    //  
    partial class Actions
    {
        /// <summary>
        /// Increase the precision at which future actions will execute. Precision is measured as the radius of the smooth interpolation between motion targets. This is refered to as "Zone", "Approximate Positioning" or "Blending Radius" in different platforms. 
        /// </summary>
        /// <param name="radiusInc">Radius increment in mm</param>
        /// <returns name="Action">Precision Action</returns>
        public static MAction Precision(double radiusInc = 0) 
            => new ActionPrecision(radiusInc, true);


        /// <summary>
        /// Set the precision at which future actions will execute. Precision is measured as the radius of the smooth interpolation between motion targets. This is refered to as "Zone", "Approximate Positioning" or "Blending Radius" in different platforms. 
        /// </summary>
        /// <param name="radius">Radius value in mm</param>
        /// <returns name="Action">Precision Action</returns>
        public static MAction PrecisionTo(double radius = 5) 
            => new ActionPrecision(radius, false);


    }
}
