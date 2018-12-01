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
    //  ██████╗  ██████╗ ██████╗ ███████╗███████╗████████╗████████╗██╗███╗   ██╗ ██████╗ ███████╗
    //  ██╔══██╗██╔═══██╗██╔══██╗██╔════╝██╔════╝╚══██╔══╝╚══██╔══╝██║████╗  ██║██╔════╝ ██╔════╝
    //  ██████╔╝██║   ██║██████╔╝███████╗█████╗     ██║      ██║   ██║██╔██╗ ██║██║  ███╗███████╗
    //  ██╔═══╝ ██║   ██║██╔═══╝ ╚════██║██╔══╝     ██║      ██║   ██║██║╚██╗██║██║   ██║╚════██║
    //  ██║     ╚██████╔╝██║     ███████║███████╗   ██║      ██║   ██║██║ ╚████║╚██████╔╝███████║
    //  ╚═╝      ╚═════╝ ╚═╝     ╚══════╝╚══════╝   ╚═╝      ╚═╝   ╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚══════╝
    //                                                                                           
    partial class Actions
    {
        /// <summary>
        /// Reverts current settings to the state store by the last call to PushSettings().
        /// </summary>
        /// <returns name="Action">PopSettings Action</returns>
        public static MAction PopSettings() 
            => new ActionPushPop(false);
    }
}
