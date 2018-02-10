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
    //  ██████╗  ██████╗ ██████╗  ██████╗ ████████╗███████╗
    //  ██╔══██╗██╔═══██╗██╔══██╗██╔═══██╗╚══██╔══╝██╔════╝
    //  ██████╔╝██║   ██║██████╔╝██║   ██║   ██║   ███████╗
    //  ██╔══██╗██║   ██║██╔══██╗██║   ██║   ██║   ╚════██║
    //  ██║  ██║╚██████╔╝██████╔╝╚██████╔╝   ██║   ███████║
    //  ╚═╝  ╚═╝ ╚═════╝ ╚═════╝  ╚═════╝    ╚═╝   ╚══════╝
    //                                                     
    /// <summary>
    /// Robot creation and manipulation tools.
    /// </summary>
    public class Robots
    {
        /// <summary>
        /// Make the constructor internal to hide it from the menu.
        /// </summary>
        internal Robots() { }

        /// <summary>
        /// Creates a new Robot instance.
        /// </summary>
        /// <param name="name">A name for this Robot</param>
        /// <param name="brand">Input "ABB", "UR", "KUKA", "ZMorph" or "HUMAN" (if you only need a human-readable representation of the actions of this Robot...)</param>
        /// <returns name="Robot">Your brand new Machina Robot object</returns>
        public static Robot New(string name = "RobotExMachina", string brand = "HUMAN") => new Robot(name, brand);

        /// <summary>
        /// Change the name of a Robot's IO pin.
        /// </summary>
        /// <param name="bot">Robot to change the IO name to</param>
        /// <param name="name">New IO name</param>
        /// <param name="pin">Pin number</param>
        /// <param name="digital">Is this a digital pin?</param>
        /// <returns name="Robot">Robot with named IO</returns>
        public static Robot SetIOName(Robot bot, string name = "Digital_IO_1", int pin = 1, bool digital = true)
        {
            bot.SetIOName(name, pin, digital);
            return bot;
        }

        /// <summary>
        /// This API's version
        /// </summary>
        internal static string MachinaDynamoAPIVersion() => "0.5.0";
        
        /// <summary>
        /// Returns version and build numbers of the Machina Core library and Dynamo API.
        /// </summary>
        /// <returns></returns>
        [MultiReturn(new[] { "Core", "Dynamo API" })]
        public static Dictionary<string, string> Version() => new Dictionary<string, string> {
            { "Core", Robot.Version },
            { "Dynamo API", Robots.MachinaDynamoAPIVersion()}
        };

    }
}
