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
        /// Creates a new Robot instance.
        /// </summary>
        /// <param name="name">A name for this Robot</param>
        /// <param name="brand">Input "ABB", "UR", "KUKA", "ZMorph" or "HUMAN" (if you only need a human-readable representation of the actions of this Robot...)</param>
        /// <returns name="Robot">Your brand new Machina Robot object</returns>
        public static Robot RobotCreate(string name = "RobotExMachina", string brand = "ABB") 
            => Robot.Create(name, brand);

       
    }
}
