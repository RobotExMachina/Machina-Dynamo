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
    //  ███████╗██╗  ██╗████████╗███████╗██████╗ ███╗   ██╗ █████╗ ██╗      █████╗ ██╗  ██╗██╗███████╗
    //  ██╔════╝╚██╗██╔╝╚══██╔══╝██╔════╝██╔══██╗████╗  ██║██╔══██╗██║     ██╔══██╗╚██╗██╔╝██║██╔════╝
    //  █████╗   ╚███╔╝    ██║   █████╗  ██████╔╝██╔██╗ ██║███████║██║     ███████║ ╚███╔╝ ██║███████╗
    //  ██╔══╝   ██╔██╗    ██║   ██╔══╝  ██╔══██╗██║╚██╗██║██╔══██║██║     ██╔══██║ ██╔██╗ ██║╚════██║
    //  ███████╗██╔╝ ██╗   ██║   ███████╗██║  ██║██║ ╚████║██║  ██║███████╗██║  ██║██╔╝ ██╗██║███████║
    //  ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚══════╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝╚══════╝
    //                                                                                                
    partial class Actions
    {
        /// <summary>
        /// Increase the value of one of the robot's external axis. Values expressed in degrees or milimeters, depending on the nature of the external axis. Note that the effect of this change of external axis will go in effect on the next motion Action.
        /// </summary>
        /// <param name="axisNumber">Axis number from 1 to 6.</param>
        /// <param name="increment">Value in mm or degrees.</param>
        /// <param name="target">Which set of External Axes to target? "All", "Cartesian" or "Joint"?</param>
        /// <returns name="Action">ExternalAxis Action</returns>
        public static MAction ExternalAxis(int axisNumber, double increment, string target = "All")
        {
            if (axisNumber < 1 || axisNumber > 6)
            {
                DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("AxisNumber must be between 1 and 6");
                return null;
            }

            ExternalAxesTarget eat;
            try
            {
                eat = (ExternalAxesTarget)Enum.Parse(typeof(ExternalAxesTarget), target, true);
                if (Enum.IsDefined(typeof(ExternalAxesTarget), eat))
                {
                    //DA.SetData(0, new ActionExternalAxis(axisNumber, val, ExternalAxesTarget.All, this.Relative));
                    return new ActionExternalAxis(axisNumber, increment, eat, true);
                }
            }
            catch { }


            DynamoServices.LogWarningMessageEvents.OnLogWarningMessage(
                $"{target} is not a valid ExternalAxesTarget type, please specify one of the following: {Utils.EnumerateList(Enum.GetNames(typeof(ExternalAxesTarget)), "or")}");
            return null;
        }

        /// <summary>
        /// Set the value of one of the robot's external axis. Values expressed in degrees or milimeters, depending on the nature of the external axis. Note that the effect of this change of external axis will go in effect on the next motion Action.
        /// </summary>
        /// <param name="axisNumber">Axis number from 1 to 6.</param>
        /// <param name="value">Value in mm or degrees.</param>
        /// <param name="target">Which set of External Axes to target? "All", "Cartesian" or "Joint"?</param>
        /// <returns name="Action">ExternalAxis Action</returns>
        public static MAction ExternalAxisTo(int axisNumber, double value, string target = "All")
        {
            if (axisNumber < 1 || axisNumber > 6)
            {
                DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("AxisNumber must be between 1 and 6");
                return null;
            }

            ExternalAxesTarget eat;
            try
            {
                eat = (ExternalAxesTarget)Enum.Parse(typeof(ExternalAxesTarget), target, true);
                if (Enum.IsDefined(typeof(ExternalAxesTarget), eat))
                {
                    //DA.SetData(0, new ActionExternalAxis(axisNumber, val, ExternalAxesTarget.All, this.Relative));
                    return new ActionExternalAxis(axisNumber, value, eat, false);
                }
            }
            catch { }


            DynamoServices.LogWarningMessageEvents.OnLogWarningMessage(
                $"{target} is not a valid ExternalAxesTarget type, please specify one of the following: {Utils.EnumerateList(Enum.GetNames(typeof(ExternalAxesTarget)), "or")}");
            return null;
        }






    }
}
