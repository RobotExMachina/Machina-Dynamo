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
    //  ██╗    ██╗██████╗ ██╗████████╗███████╗ █████╗ ███╗   ██╗ █████╗ ██╗      ██████╗  ██████╗ 
    //  ██║    ██║██╔══██╗██║╚══██╔══╝██╔════╝██╔══██╗████╗  ██║██╔══██╗██║     ██╔═══██╗██╔════╝ 
    //  ██║ █╗ ██║██████╔╝██║   ██║   █████╗  ███████║██╔██╗ ██║███████║██║     ██║   ██║██║  ███╗
    //  ██║███╗██║██╔══██╗██║   ██║   ██╔══╝  ██╔══██║██║╚██╗██║██╔══██║██║     ██║   ██║██║   ██║
    //  ╚███╔███╔╝██║  ██║██║   ██║   ███████╗██║  ██║██║ ╚████║██║  ██║███████╗╚██████╔╝╚██████╔╝
    //   ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝   ╚═╝   ╚══════╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝╚══════╝ ╚═════╝  ╚═════╝ 
    //                                                                                            
    partial class Actions
    {
        /// <summary>
        /// Send a value to analog output.
        /// </summary>
        /// <param name="analogPin">Analog pin name or number</param>
        /// <param name="value">Value to send to pin</param>
        /// <param name="toolPin">Is this pin on the robot's tool?</param>
        /// <returns name="Action"></returns>
        public static MAction WriteAnalog(string analogPin = "1", double value = 0, bool toolPin = false) 
            => new ActionIOAnalog(analogPin, value, toolPin);

        // Why did I have the two overloads? Everything should work fine with the string version... 
        ///// <summary>
        ///// Send a value to analog output.
        ///// </summary>
        ///// <param name="analogPin">Analog pin name or number</param>
        ///// <param name="value">Value to send to pin</param>
        ///// <param name="toolPin">Is this pin on the robot's tool?</param>
        ///// <returns name="Action"></returns>
        //public static MAction WriteAnalog(int analogPin = 1, double value = 0, bool toolPin = false) => new ActionIOAnalog(analogPin.ToString(), value, toolPin);

    }
}
