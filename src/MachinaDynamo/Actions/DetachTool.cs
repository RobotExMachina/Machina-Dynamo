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
    //  ██████╗ ███████╗████████╗ █████╗  ██████╗██╗  ██╗████████╗ ██████╗  ██████╗ ██╗     
    //  ██╔══██╗██╔════╝╚══██╔══╝██╔══██╗██╔════╝██║  ██║╚══██╔══╝██╔═══██╗██╔═══██╗██║     
    //  ██║  ██║█████╗     ██║   ███████║██║     ███████║   ██║   ██║   ██║██║   ██║██║     
    //  ██║  ██║██╔══╝     ██║   ██╔══██║██║     ██╔══██║   ██║   ██║   ██║██║   ██║██║     
    //  ██████╔╝███████╗   ██║   ██║  ██║╚██████╗██║  ██║   ██║   ╚██████╔╝╚██████╔╝███████╗
    //  ╚═════╝ ╚══════╝   ╚═╝   ╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝   ╚═╝    ╚═════╝  ╚═════╝ ╚══════╝
    //                                                                           
    partial class Actions
    {
        /// <summary>
        /// Detach any Tool currently attached to the Robot. Note that the Tool Center Point (TCP) will now be reverted back to the Robot's flange.
        /// </summary>
        /// <returns name="Action">DetachTool Action</returns>
        public static MAction DetachTool() 
            => new ActionDetachTool();
    }
}
