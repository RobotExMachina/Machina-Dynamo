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
    //  ██████╗  ██████╗ ████████╗ █████╗ ████████╗███████╗
    //  ██╔══██╗██╔═══██╗╚══██╔══╝██╔══██╗╚══██╔══╝██╔════╝
    //  ██████╔╝██║   ██║   ██║   ███████║   ██║   █████╗  
    //  ██╔══██╗██║   ██║   ██║   ██╔══██║   ██║   ██╔══╝  
    //  ██║  ██║╚██████╔╝   ██║   ██║  ██║   ██║   ███████╗
    //  ╚═╝  ╚═╝ ╚═════╝    ╚═╝   ╚═╝  ╚═╝   ╚═╝   ╚══════╝
    //        
    partial class Actions
    {
        /// <summary>
        /// Rotates the device a specified angle in degrees along the specified vector.
        /// </summary>
        /// <param name="axis">Rotation axis, with positive rotation direction is defined by the right-hand rule.</param>
        /// <param name="angDegs">Rotation angle in degrees</param>
        /// <returns name="Action">Rotate Action</returns>
        public static MAction Rotate(Autodesk.DesignScript.Geometry.Vector axis, double angDegs) 
            => new ActionRotation(axis.X, axis.Y, axis.Z, angDegs, true);


        /// <summary>
        /// Rotates the device to a specified orientation without translating the TCP.
        /// </summary>
        /// <param name="targetPlane">Target spatial orientation</param>
        /// <returns name="Action">Rotate Action</returns>
        public static MAction RotateTo(Plane targetPlane) 
            => new ActionRotation(new MOrientation(targetPlane.XAxis.X, targetPlane.XAxis.Y, targetPlane.XAxis.Z, targetPlane.YAxis.X, targetPlane.YAxis.Y, targetPlane.YAxis.Z), false);

    }
}
