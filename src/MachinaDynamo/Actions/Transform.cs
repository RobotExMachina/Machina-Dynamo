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
    //  ████████╗██████╗  █████╗ ███╗   ██╗███████╗███████╗ ██████╗ ██████╗ ███╗   ███╗
    //  ╚══██╔══╝██╔══██╗██╔══██╗████╗  ██║██╔════╝██╔════╝██╔═══██╗██╔══██╗████╗ ████║
    //     ██║   ██████╔╝███████║██╔██╗ ██║███████╗█████╗  ██║   ██║██████╔╝██╔████╔██║
    //     ██║   ██╔══██╗██╔══██║██║╚██╗██║╚════██║██╔══╝  ██║   ██║██╔══██╗██║╚██╔╝██║
    //     ██║   ██║  ██║██║  ██║██║ ╚████║███████║██║     ╚██████╔╝██║  ██║██║ ╚═╝ ██║
    //     ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝╚═╝      ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝
    //                                                                                 
    partial class Actions
    {
        // ON ITS WAY TO DEPRECATION...
        ///// <summary>
        ///// Performs a compound relative transformation as a rotation + translation. Note that when performing relative transformations, the R+T versus T+R order matters.
        ///// </summary>
        ///// <param name="direction">Translation vector</param>
        ///// <param name="axis">Rotation axis</param>
        ///// <param name="angDegs">Rotation angle in degrees</param>
        ///// <param name="moveFirst">Apply translation first? Note that when performing relative transformations, the R+T versus T+R order matters.</param>
        ///// <returns name="Action">Transform Action</returns>
        //public static MAction Transform(Autodesk.DesignScript.Geometry.Vector direction, Autodesk.DesignScript.Geometry.Vector axis, double angDegs, bool moveFirst = true) =>
        //    new ActionTransformation(
        //        Utils.Vec2BPoint(direction),
        //        new Rotation(Utils.Vec2BPoint(axis), angDegs),
        //        true,
        //        moveFirst);


        /// <summary>
        /// Performs a compound absolute transformation to target Plane. The device's new absolute position and orientation will be those of the Plane.
        /// </summary>
        /// <param name="plane">Target Plane to transform to</param>
        /// <returns name="Action">Transform Action</returns>
        public static MAction TransformTo(Plane plane) =>
            new ActionTransformation(
                plane.Origin.X, plane.Origin.Y, plane.Origin.Z,
                plane.XAxis.X, plane.XAxis.Y, plane.XAxis.Z, 
                plane.YAxis.X, plane.YAxis.Y, plane.YAxis.Z,
                false,
                true);

    }
}
