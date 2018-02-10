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

    //  ████████╗ ██████╗  ██████╗ ██╗     
    //  ╚══██╔══╝██╔═══██╗██╔═══██╗██║     
    //     ██║   ██║   ██║██║   ██║██║     
    //     ██║   ██║   ██║██║   ██║██║     
    //     ██║   ╚██████╔╝╚██████╔╝███████╗
    //     ╚═╝    ╚═════╝  ╚═════╝ ╚══════╝
    //                                     
    /// <summary>
    /// Tool creation and manipulation tools.
    /// </summary>
    public class Tools
    {
        // Internal constructor.
        internal Tools() { }

        /// <summary>
        /// Create a Tool object.
        /// </summary>
        /// <param name="name">Tool name</param>
        /// <param name="basePlane">Base Plane where the Tool will be attached to the Robot</param>
        /// <param name="toolTipPlane">Plane of the Tool Tip Center (TCP)</param>
        /// <param name="weight">Tool weight in Kg</param>
        /// <returns name="Tool">New Tool object</returns>
        public static MTool New(string name, 
            Autodesk.DesignScript.Geometry.Plane basePlane, 
            Autodesk.DesignScript.Geometry.Plane toolTipPlane,
            double weight = 1)
        {

            CoordinateSystem basePlaneCS = basePlane.ToCoordinateSystem();
            CoordinateSystem toolTipPlaneCS = toolTipPlane.ToCoordinateSystem();

            CoordinateSystem relativeCS = toolTipPlaneCS.PreMultiplyBy(basePlaneCS.Inverse());
            //CoordinateSystem relativeCS = toolTipPlaneCS.Transform(basePlaneCS.Inverse());  // this is the same thing

            MVector TCPPosition = new MVector(relativeCS.Origin.X, relativeCS.Origin.Y, relativeCS.Origin.Z);
            MVector centerOfGravity = new MVector(TCPPosition);
            centerOfGravity.Scale(0.5);
            MOrientation TCPOrientation = new MOrientation(relativeCS.XAxis.X, relativeCS.XAxis.Y, relativeCS.XAxis.Z,
                relativeCS.YAxis.X, relativeCS.YAxis.Y, relativeCS.YAxis.Z);

            // https://github.com/DynamoDS/Dynamo/wiki/Zero-Touch-Plugin-Development#dispose--using-statement
            basePlane.Dispose();
            toolTipPlaneCS.Dispose();
            relativeCS.Dispose();

            return new MTool(name, TCPPosition, TCPOrientation, weight, centerOfGravity);
        }

    }
}
