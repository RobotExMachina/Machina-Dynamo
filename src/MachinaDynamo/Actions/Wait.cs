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
    //  ██╗    ██╗ █████╗ ██╗████████╗
    //  ██║    ██║██╔══██╗██║╚══██╔══╝
    //  ██║ █╗ ██║███████║██║   ██║   
    //  ██║███╗██║██╔══██║██║   ██║   
    //  ╚███╔███╔╝██║  ██║██║   ██║   
    //   ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝   ╚═╝   
    //                                
    partial class Actions
    {
        /// <summary>
        /// Moves the device along a speficied vector relative to its current position.
        /// </summary>
        /// <param name="direction">Translation vector</param>
        /// <returns name="Action">Move Action</returns>
        public static MAction Move(Autodesk.DesignScript.Geometry.Vector direction) 
            => new ActionTranslation(direction.X, direction.Y, direction.Z, true);


        /// <summary>
        /// Moves the device to an absolute location.
        /// </summary>
        /// <param name="location">Target Point</param>
        /// <returns name="Action">Move Action</returns>
        public static MAction MoveTo(Autodesk.DesignScript.Geometry.Point location) 
            => new ActionTranslation(location.X, location.Y, location.Z, false);

    }
}
