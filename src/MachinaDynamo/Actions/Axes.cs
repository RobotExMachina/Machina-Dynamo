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
    //   █████╗ ██╗  ██╗███████╗███████╗
    //  ██╔══██╗╚██╗██╔╝██╔════╝██╔════╝
    //  ███████║ ╚███╔╝ █████╗  ███████╗
    //  ██╔══██║ ██╔██╗ ██╔══╝  ╚════██║
    //  ██║  ██║██╔╝ ██╗███████╗███████║
    //  ╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚══════╝
    //                                  
    partial class Actions
    {
        /// <summary>
        /// Increase the axes' rotation angle in degrees at the joints of mechanical devices, specially robotic arms.
        /// </summary>
        /// <param name="a1inc">Rotational increment in degrees for Axis 1</param>
        /// <param name="a2inc">Rotational increment in degrees for Axis 2</param>
        /// <param name="a3inc">Rotational increment in degrees for Axis 3</param>
        /// <param name="a4inc">Rotational increment in degrees for Axis 4</param>
        /// <param name="a5inc">Rotational increment in degrees for Axis 5</param>
        /// <param name="a6inc">Rotational increment in degrees for Axis 6</param>
        /// <returns name="Action">Axes Action</returns>
        public static MAction Axes(double a1inc = 0, double a2inc = 0, double a3inc = 0, double a4inc = 0, double a5inc = 0, double a6inc = 0) 
            => new ActionAxes(new Joints(a1inc, a2inc, a3inc, a4inc, a5inc, a6inc), true);


        /// <summary>
        /// Set the axes' rotation angle in degrees at the joints of mechanical devices, specially robotic arms.
        /// </summary>
        /// <param name="a1">Angular value in degrees for Axis 1</param>
        /// <param name="a2">Angular value in degrees for Axis 2</param>
        /// <param name="a3">Angular value in degrees for Axis 3</param>
        /// <param name="a4">Angular value in degrees for Axis 4</param>
        /// <param name="a5">Angular value in degrees for Axis 5</param>
        /// <param name="a6">Angular value in degrees for Axis 6</param>
        /// <returns name="Action">Axes Action</returns>
        public static MAction AxesTo(double a1, double a2, double a3, double a4, double a5, double a6) 
            => new ActionAxes(new Joints(a1, a2, a3, a4, a5, a6), false);

    }
}
