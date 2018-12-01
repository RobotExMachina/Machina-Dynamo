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
    //  ██████╗ ███████╗███████╗██╗███╗   ██╗███████╗████████╗ ██████╗  ██████╗ ██╗     
    //  ██╔══██╗██╔════╝██╔════╝██║████╗  ██║██╔════╝╚══██╔══╝██╔═══██╗██╔═══██╗██║     
    //  ██║  ██║█████╗  █████╗  ██║██╔██╗ ██║█████╗     ██║   ██║   ██║██║   ██║██║     
    //  ██║  ██║██╔══╝  ██╔══╝  ██║██║╚██╗██║██╔══╝     ██║   ██║   ██║██║   ██║██║     
    //  ██████╔╝███████╗██║     ██║██║ ╚████║███████╗   ██║   ╚██████╔╝╚██████╔╝███████╗
    //  ╚═════╝ ╚══════╝╚═╝     ╚═╝╚═╝  ╚═══╝╚══════╝   ╚═╝    ╚═════╝  ╚═════╝ ╚══════╝
    //                                                                                  
    partial class Actions
    {
        /// <summary>
        /// Define a Tool object on the Robot's internal library to make it avaliable for future Attach/Detach actions.
        /// </summary>
        /// <param name="name">Tool name</param>
        /// <param name="basePlane">Base Plane where the Tool will be attached to the Robot</param>
        /// <param name="toolTipPlane">Plane of the Tool Tip Center (TCP)</param>
        /// <param name="weight">Tool weight in Kg</param>
        /// <returns name="Action">DefineTool Action</returns>
        public static MAction DefineTool(string name,
            Autodesk.DesignScript.Geometry.Plane basePlane,
            Autodesk.DesignScript.Geometry.Plane toolTipPlane,
            double weight = 0.1)
        {

            // https://github.com/DynamoDS/Dynamo/wiki/Zero-Touch-Plugin-Development#dispose--using-statement
            using (CoordinateSystem basePlaneCS = basePlane.ToCoordinateSystem())
            {
                using (CoordinateSystem toolTipPlaneCS = toolTipPlane.ToCoordinateSystem())
                {
                    using (CoordinateSystem relativeCS = toolTipPlaneCS.PreMultiplyBy(basePlaneCS.Inverse()))
                    {
                        //MVector TCPPosition = new MVector(relativeCS.Origin.X, relativeCS.Origin.Y, relativeCS.Origin.Z);
                        //MVector centerOfGravity = new MVector(TCPPosition);
                        //centerOfGravity.Scale(0.5);
                        //MOrientation TCPOrientation = new MOrientation(relativeCS.XAxis.X, relativeCS.XAxis.Y, relativeCS.XAxis.Z,
                        //    relativeCS.YAxis.X, relativeCS.YAxis.Y, relativeCS.YAxis.Z);

                        return new ActionDefineTool(name,
                            relativeCS.Origin.X, relativeCS.Origin.Y, relativeCS.Origin.Z,
                            relativeCS.XAxis.X, relativeCS.XAxis.Y, relativeCS.XAxis.Z,
                            relativeCS.YAxis.X, relativeCS.YAxis.Y, relativeCS.YAxis.Z,
                            weight,
                            0.5 * relativeCS.Origin.X, 0.5 * relativeCS.Origin.Y, 0.5 * relativeCS.Origin.Z);  // cog as quick fix
                    }
                }
            }



            // FOR SOME REASON, DOING IT LIKE BELOW CAUSED A WEIRD BUG:
            // - If creating a new Tool, the component will always yield error. But if saving the file with the error component and opening it again, it would work. 
            // - Using the 'using' statements fixes this issue and allows to create a new Tool without having to restart the definition...

            //CoordinateSystem basePlaneCS = basePlane.ToCoordinateSystem();
            //CoordinateSystem toolTipPlaneCS = toolTipPlane.ToCoordinateSystem();

            //CoordinateSystem relativeCS = toolTipPlaneCS.PreMultiplyBy(basePlaneCS.Inverse());
            ////CoordinateSystem relativeCS = toolTipPlaneCS.Transform(basePlaneCS.Inverse());  // this is the same thing

            //MVector TCPPosition = new MVector(relativeCS.Origin.X, relativeCS.Origin.Y, relativeCS.Origin.Z);
            //MVector centerOfGravity = new MVector(TCPPosition);
            //centerOfGravity.Scale(0.5);
            //MOrientation TCPOrientation = new MOrientation(relativeCS.XAxis.X, relativeCS.XAxis.Y, relativeCS.XAxis.Z,
            //    relativeCS.YAxis.X, relativeCS.YAxis.Y, relativeCS.YAxis.Z);

            //// https://github.com/DynamoDS/Dynamo/wiki/Zero-Touch-Plugin-Development#dispose--using-statement
            //basePlane.Dispose();
            //toolTipPlaneCS.Dispose();
            //relativeCS.Dispose();

            //return MTool.Create(name, TCPPosition, TCPOrientation, weight, centerOfGravity);
        }
    }
}
