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
    //  ███████╗██╗  ██╗████████╗██████╗ ██╗   ██╗██████╗ ███████╗
    //  ██╔════╝╚██╗██╔╝╚══██╔══╝██╔══██╗██║   ██║██╔══██╗██╔════╝
    //  █████╗   ╚███╔╝    ██║   ██████╔╝██║   ██║██║  ██║█████╗  
    //  ██╔══╝   ██╔██╗    ██║   ██╔══██╗██║   ██║██║  ██║██╔══╝  
    //  ███████╗██╔╝ ██╗   ██║   ██║  ██║╚██████╔╝██████╔╝███████╗
    //  ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝  ╚═╝ ╚═════╝ ╚═════╝ ╚══════╝
    //                                                            
    partial class Actions
    {
        /// <summary>
        /// Turns extrusion in 3D printers on/off.
        /// </summary>
        /// <param name="extrude">True/false for on/off</param>
        /// <returns name="Action">Extrude Action</returns>
        public static MAction Extrude(bool extrude = false) 
            => new ActionExtrusion(extrude);


    }
}
