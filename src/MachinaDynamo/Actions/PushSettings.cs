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
    //  ██████╗ ██╗   ██╗███████╗██╗  ██╗███████╗███████╗████████╗████████╗██╗███╗   ██╗ ██████╗ ███████╗
    //  ██╔══██╗██║   ██║██╔════╝██║  ██║██╔════╝██╔════╝╚══██╔══╝╚══██╔══╝██║████╗  ██║██╔════╝ ██╔════╝
    //  ██████╔╝██║   ██║███████╗███████║███████╗█████╗     ██║      ██║   ██║██╔██╗ ██║██║  ███╗███████╗
    //  ██╔═══╝ ██║   ██║╚════██║██╔══██║╚════██║██╔══╝     ██║      ██║   ██║██║╚██╗██║██║   ██║╚════██║
    //  ██║     ╚██████╔╝███████║██║  ██║███████║███████╗   ██║      ██║   ██║██║ ╚████║╚██████╔╝███████║
    //  ╚═╝      ╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝   ╚═╝      ╚═╝   ╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚══════╝
    //                                                                                                   
    partial class Actions
    {
        /// <summary>
        /// Stores current state settings to a buffer, so that temporary changes can be made, and settings can be reverted to the stored state later with PopSettings().
        /// </summary>
        /// <returns name="Action">PushSettings Action</returns>
        public static MAction PushSettings() 
            => new ActionPushPop(true);
    }
}
