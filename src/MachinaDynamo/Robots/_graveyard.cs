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
    //   ██████╗ ██████╗  █████╗ ██╗   ██╗███████╗██╗   ██╗ █████╗ ██████╗ ██████╗ 
    //  ██╔════╝ ██╔══██╗██╔══██╗██║   ██║██╔════╝╚██╗ ██╔╝██╔══██╗██╔══██╗██╔══██╗
    //  ██║  ███╗██████╔╝███████║██║   ██║█████╗   ╚████╔╝ ███████║██████╔╝██║  ██║
    //  ██║   ██║██╔══██╗██╔══██║╚██╗ ██╔╝██╔══╝    ╚██╔╝  ██╔══██║██╔══██╗██║  ██║
    //  ╚██████╔╝██║  ██║██║  ██║ ╚████╔╝ ███████╗   ██║   ██║  ██║██║  ██║██████╔╝
    //   ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝  ╚═══╝  ╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═════╝ 
    //                                                                             
    /// <summary>
    /// Robot creation and manipulation tools.
    /// </summary>
    public partial class Robots
    {

        // Legacy components, deprecated or on their way to change. 
        /// <summary>
        /// Change the name of a Robot's IO pin.
        /// </summary>
        /// <param name="bot">Robot to change the IO name to</param>
        /// <param name="name">New IO name</param>
        /// <param name="pin">Pin number</param>
        /// <param name="digital">Is this a digital pin?</param>
        /// <returns name="Robot">Robot with named IO</returns>
        [IsVisibleInDynamoLibrary(false)]
        public static Robot SetIOName(Robot bot, string name = "Digital_IO_1", int pin = 1, bool digital = true)
        {
            //bot.SetIOName(name, pin, digital);
            //return bot;
            DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("Deprecated component: you can now use named IOs in `WriteDigital` and `WriteAnalog`");
            return null;
        }

    }
}
