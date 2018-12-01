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
    //   ██████╗ ██████╗ ███╗   ███╗███╗   ███╗███████╗███╗   ██╗████████╗
    //  ██╔════╝██╔═══██╗████╗ ████║████╗ ████║██╔════╝████╗  ██║╚══██╔══╝
    //  ██║     ██║   ██║██╔████╔██║██╔████╔██║█████╗  ██╔██╗ ██║   ██║   
    //  ██║     ██║   ██║██║╚██╔╝██║██║╚██╔╝██║██╔══╝  ██║╚██╗██║   ██║   
    //  ╚██████╗╚██████╔╝██║ ╚═╝ ██║██║ ╚═╝ ██║███████╗██║ ╚████║   ██║   
    //   ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚═╝     ╚═╝╚══════╝╚═╝  ╚═══╝   ╚═╝   
    //                                                                    
    partial class Actions
    {

        /// <summary>
        /// Displays an internal comment in a program compilation. This is useful for internal annotations or reminders, but has no effect on the Robot's behavior.
        /// </summary>
        /// <param name="comment">Comment to be displayed on code compilation</param>
        /// <returns name="Action">Comment Action</returns>
        public static MAction Comment(string comment = "Comment goes here") 
            => new ActionComment(comment);


    }
}
