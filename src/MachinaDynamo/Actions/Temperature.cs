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
    //  ████████╗███████╗███╗   ███╗██████╗ ███████╗██████╗  █████╗ ████████╗██╗   ██╗██████╗ ███████╗
    //  ╚══██╔══╝██╔════╝████╗ ████║██╔══██╗██╔════╝██╔══██╗██╔══██╗╚══██╔══╝██║   ██║██╔══██╗██╔════╝
    //     ██║   █████╗  ██╔████╔██║██████╔╝█████╗  ██████╔╝███████║   ██║   ██║   ██║██████╔╝█████╗  
    //     ██║   ██╔══╝  ██║╚██╔╝██║██╔═══╝ ██╔══╝  ██╔══██╗██╔══██║   ██║   ██║   ██║██╔══██╗██╔══╝  
    //     ██║   ███████╗██║ ╚═╝ ██║██║     ███████╗██║  ██║██║  ██║   ██║   ╚██████╔╝██║  ██║███████╗
    //     ╚═╝   ╚══════╝╚═╝     ╚═╝╚═╝     ╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝
    //                                                                                                
    partial class Actions
    {
        /// <summary>
        /// Increment the working temperature of one of the device's parts. Useful for 3D printing operations.
        /// </summary>
        /// <param name="tempInc">Temperature increment in °C</param>
        /// <param name="part">Device's part that will change temperature, e.g. "extruder", "bed", etc.</param>
        /// <param name="wait">If true, execution will wait for the part to heat up and resume when reached the target.</param>
        /// <returns name="Action">Temperature Action</returns>
        public static MAction Temperature(double tempInc = 0, string part = "bed", bool wait = true)
        {
            RobotPartType tt;
            try
            {
                tt = (RobotPartType)Enum.Parse(typeof(RobotPartType), part, true);
                if (Enum.IsDefined(typeof(RobotPartType), tt))
                {
                    return new ActionTemperature(tempInc, tt, wait, true);
                }
            }
            catch { }

            DynamoServices.LogWarningMessageEvents.OnLogWarningMessage($"\"{part}\" is not a valid part for temperature changes, please specify one of the following: {Utils.EnumerateList(Enum.GetNames(typeof(RobotPartType)), "or")}.");
            return null;
        }


        /// <summary>
        /// Set the working temperature of one of the device's parts. Useful for 3D printing operations.
        /// </summary>
        /// <param name="temp">Temperature value in °C</param>
        /// <param name="part">Device's part that will change temperature, e.g. "extruder", "bed", etc.</param>
        /// <param name="wait">If true, execution will wait for the part to heat up and resume when reached the target.</param>
        /// <returns name="Action">Temperature Action</returns>
        public static MAction TemperatureTo(double temp = 0, string part = "bed", bool wait = true)
        {
            RobotPartType tt;
            try
            {
                tt = (RobotPartType)Enum.Parse(typeof(RobotPartType), part, true);
                if (Enum.IsDefined(typeof(RobotPartType), tt))
                {
                    return new ActionTemperature(temp, tt, wait, false);
                }
            }
            catch { }

            DynamoServices.LogWarningMessageEvents.OnLogWarningMessage($"\"{part}\" is not a valid part for temperature changes, please specify one of the following: {Utils.EnumerateList(Enum.GetNames(typeof(RobotPartType)), "or")}.");
            return null;
        }

    }
}
