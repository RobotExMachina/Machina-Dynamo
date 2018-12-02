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
    //  ██████╗  ██████╗ ██████╗  ██████╗ ████████╗███████╗
    //  ██╔══██╗██╔═══██╗██╔══██╗██╔═══██╗╚══██╔══╝██╔════╝
    //  ██████╔╝██║   ██║██████╔╝██║   ██║   ██║   ███████╗
    //  ██╔══██╗██║   ██║██╔══██╗██║   ██║   ██║   ╚════██║
    //  ██║  ██║╚██████╔╝██████╔╝╚██████╔╝   ██║   ███████║
    //  ╚═╝  ╚═╝ ╚═════╝ ╚═════╝  ╚═════╝    ╚═╝   ╚══════╝
    //                                                     
    /// <summary>
    /// Robot creation and manipulation tools.
    /// </summary>
    public partial class Robots
    {
        /// <summary>
        /// Make the constructor internal to hide it from the menu.
        /// </summary>
        internal Robots() { }

    }
}