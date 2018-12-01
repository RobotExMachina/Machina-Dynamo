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
        /// Sets the current motion mode to be applied to future actions. This can be "linear" (default) for straight line movements in euclidean space, or "joint" for smooth interpolation between axes angles. NOTE: "joint" motion may produce unexpected trajectories resulting in reorientations or collisions; use with caution!
        /// </summary>
        /// <param name="mode">"linear" or "joint"</param>
        /// <returns name="Action">MotionMode Action</returns>
        public static MAction MotionMode(string mode = "linear")
        {
            MotionType mt;
            try
            {
                mt = (MotionType)Enum.Parse(typeof(MotionType), mode, true);
                if (Enum.IsDefined(typeof(MotionType), mt))
                {
                    return new ActionMotionMode(mt);
                }
            }
            catch { }

            DynamoServices.LogWarningMessageEvents.OnLogWarningMessage(
                $"{mode} is not a valid option for MotionMode changes, please specify one of the following: {Utils.EnumerateList(Enum.GetNames(typeof(MotionType)), "or")}");
            return null;
        }

    }
}
