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
    //  ██╗    ██╗██████╗ ██╗████████╗███████╗██████╗ ██╗ ██████╗ ██╗████████╗ █████╗ ██╗     
    //  ██║    ██║██╔══██╗██║╚══██╔══╝██╔════╝██╔══██╗██║██╔════╝ ██║╚══██╔══╝██╔══██╗██║     
    //  ██║ █╗ ██║██████╔╝██║   ██║   █████╗  ██║  ██║██║██║  ███╗██║   ██║   ███████║██║     
    //  ██║███╗██║██╔══██╗██║   ██║   ██╔══╝  ██║  ██║██║██║   ██║██║   ██║   ██╔══██║██║     
    //  ╚███╔███╔╝██║  ██║██║   ██║   ███████╗██████╔╝██║╚██████╔╝██║   ██║   ██║  ██║███████╗
    //   ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝   ╚═╝   ╚══════╝╚═════╝ ╚═╝ ╚═════╝ ╚═╝   ╚═╝   ╚═╝  ╚═╝╚══════╝
    //                                                                                        
    partial class Actions
    {
        /// <summary>
        /// Activate/deactivate digital output. 
        /// </summary>
        /// <param name="digitalPin">Digital pin name or number</param>
        /// <param name="isOn">Turn on?</param>
        /// <param name="toolPin">Is this pin on the robot's tool?</param>
        /// <returns name="Action"></returns>
        public static MAction WriteDigital(string digitalPin = "1", bool isOn = false, bool toolPin = false) 
            => new ActionIODigital(digitalPin, isOn, toolPin);

        // Why did I have the two overloads? Everything should work fine with the string version... 
        ///// <summary>
        ///// Activate/deactivate digital output. 
        ///// </summary>
        ///// <param name="digitalPin">Digital pin name or number</param>
        ///// <param name="isOn">Turn on?</param>
        ///// <param name="toolPin">Is this pin on the robot's tool?</param>
        ///// <returns name="Action"></returns>
        //public static MAction WriteDigital(int digitalPin = 1, bool isOn = false, bool toolPin = false) => new ActionIODigital(digitalPin.ToString(), isOn, toolPin);
    }
}
