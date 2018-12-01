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
    //  ███╗   ███╗███████╗███████╗███████╗ █████╗  ██████╗ ███████╗
    //  ████╗ ████║██╔════╝██╔════╝██╔════╝██╔══██╗██╔════╝ ██╔════╝
    //  ██╔████╔██║█████╗  ███████╗███████╗███████║██║  ███╗█████╗  
    //  ██║╚██╔╝██║██╔══╝  ╚════██║╚════██║██╔══██║██║   ██║██╔══╝  
    //  ██║ ╚═╝ ██║███████╗███████║███████║██║  ██║╚██████╔╝███████╗
    //  ╚═╝     ╚═╝╚══════╝╚══════╝╚══════╝╚═╝  ╚═╝ ╚═════╝ ╚══════╝
    //                                                              
    partial class Actions
    {
        /// <summary>
        /// Displays a text message on the device. This will depend on the device's configuration, e.g. ABB robots it will display it on the FlexPendant's log window.
        /// </summary>
        /// <param name="message">Text message to display</param>
        /// <returns name="Action">Message Action</returns>
        public static MAction Message(string message = "Hello Machina!") 
            => new ActionMessage(message);

    }
}
