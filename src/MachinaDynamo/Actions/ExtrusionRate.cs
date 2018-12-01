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
    //  ███████╗██╗  ██╗████████╗██████╗ ██╗   ██╗███████╗██╗ ██████╗ ███╗   ██╗██████╗  █████╗ ████████╗███████╗
    //  ██╔════╝╚██╗██╔╝╚══██╔══╝██╔══██╗██║   ██║██╔════╝██║██╔═══██╗████╗  ██║██╔══██╗██╔══██╗╚══██╔══╝██╔════╝
    //  █████╗   ╚███╔╝    ██║   ██████╔╝██║   ██║███████╗██║██║   ██║██╔██╗ ██║██████╔╝███████║   ██║   █████╗  
    //  ██╔══╝   ██╔██╗    ██║   ██╔══██╗██║   ██║╚════██║██║██║   ██║██║╚██╗██║██╔══██╗██╔══██║   ██║   ██╔══╝  
    //  ███████╗██╔╝ ██╗   ██║   ██║  ██║╚██████╔╝███████║██║╚██████╔╝██║ ╚████║██║  ██║██║  ██║   ██║   ███████╗
    //  ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝  ╚═╝ ╚═════╝ ╚══════╝╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ╚══════╝
    //                                                                                                           
    partial class Actions
    {
        /// <summary>
        /// Increases the extrusion rate of filament for 3D printers.
        /// </summary>
        /// <param name="rateInc">Increment of mm of filament per mm of movement</param>
        /// <returns name="Action">ExtrusionRate Action</returns>
        public static MAction ExtrusionRate(double rateInc = 0) 
            => new ActionExtrusionRate(rateInc, true);


        /// <summary>
        /// Sets the extrusion rate of filament for 3D printers.
        /// </summary>
        /// <param name="rate">Value of mm of filament per mm of movement</param>
        /// <returns name="Action">ExtrusionRate Action</returns>
        public static MAction ExtrusionRateTo(double rate = 0) 
            => new ActionExtrusionRate(rate, true);

    }
}
