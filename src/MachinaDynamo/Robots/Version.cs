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
    //  ██╗   ██╗███████╗██████╗ ███████╗██╗ ██████╗ ███╗   ██╗
    //  ██║   ██║██╔════╝██╔══██╗██╔════╝██║██╔═══██╗████╗  ██║
    //  ██║   ██║█████╗  ██████╔╝███████╗██║██║   ██║██╔██╗ ██║
    //  ╚██╗ ██╔╝██╔══╝  ██╔══██╗╚════██║██║██║   ██║██║╚██╗██║
    //   ╚████╔╝ ███████╗██║  ██║███████║██║╚██████╔╝██║ ╚████║
    //    ╚═══╝  ╚══════╝╚═╝  ╚═╝╚══════╝╚═╝ ╚═════╝ ╚═╝  ╚═══╝
    //                                                         
    /// <summary>
    /// Robot creation and manipulation tools.
    /// </summary>
    public partial class Robots
    {
        /// <summary>
        /// This API's version
        /// </summary>
        internal static string MachinaDynamoAPIVersion() => "0.8.8";
        
        /// <summary>
        /// Returns version and build numbers of the Machina Core library and Dynamo API.
        /// </summary>
        /// <returns></returns>
        [MultiReturn(new[] { "Core", "Dynamo API" })]
        public static Dictionary<string, string> Version() => new Dictionary<string, string> {
            { "Core", Robot.Version },
            { "Dynamo API", Robots.MachinaDynamoAPIVersion()}
        };

       
    }
}
