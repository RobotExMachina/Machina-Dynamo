using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Interfaces;
using Autodesk.DesignScript.Geometry;

using BRobot;
using BAction = BRobot.Action;
using BPoint = BRobot.Point;

namespace BRobotForDynamo
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
        /// Create a new Robot object.
        /// </summary>
        /// <param name="brand">Input "ABB", "UR", "KUKA", or "HUMAN" (if you only need a human-readable representation of the actions of this BRobot...)</param>
        /// <returns name="BRobot">Your brand new Robot object</returns>
        public static Robot Create(string brand = "HUMAN")
        {
            return new Robot(brand);
        }

        /// <summary>
        /// Checks version and build numbers for this BRobot.
        /// </summary>
        /// <returns></returns>
        [MultiReturn(new[] { "version", "build" })]
        public static Dictionary<string, string> Version()
        {
            string v = Robot.Version;
            string b = Robot.Build.ToString();

            return new Dictionary<string, string>
            {
                { "version", v },
                { "build", b }
            };
        }

        

    }





    //  ████████╗ ██████╗  ██████╗ ██╗     
    //  ╚══██╔══╝██╔═══██╗██╔═══██╗██║     
    //     ██║   ██║   ██║██║   ██║██║     
    //     ██║   ██║   ██║██║   ██║██║     
    //     ██║   ╚██████╔╝╚██████╔╝███████╗
    //     ╚═╝    ╚═════╝  ╚═════╝ ╚══════╝
    //                                     
    /// <summary>
    /// Tool creation and manipulation tools.
    /// </summary>
    public class Tools
    {

    }





    //   █████╗  ██████╗████████╗██╗ ██████╗ ███╗   ██╗███████╗
    //  ██╔══██╗██╔════╝╚══██╔══╝██║██╔═══██╗████╗  ██║██╔════╝
    //  ███████║██║        ██║   ██║██║   ██║██╔██╗ ██║███████╗
    //  ██╔══██║██║        ██║   ██║██║   ██║██║╚██╗██║╚════██║
    //  ██║  ██║╚██████╗   ██║   ██║╚██████╔╝██║ ╚████║███████║
    //  ╚═╝  ╚═╝ ╚═════╝   ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝
    //                                                         
    /// <summary>
    /// All possible Actions.
    /// </summary>
    public class Actions
    {

        /// <summary>
        /// Sets the current type of motion to be applied to future translation actions. This can be "linear" (default) for straight line movements in euclidean space, or "joint" for smooth interpolation between joint angles. NOTE: "joint" motion may produce unexpected trajectories resulting in reorientations or collisions. Use with caution!
        /// </summary>
        /// <param name="type">"linear" or "joint"</param>
        /// <returns>Set Motion Type Action</returns>
        public static BAction Motion(string type = "linear")
        {
            MotionType t = MotionType.Undefined;

            type = type.ToLower();
            if (type.Equals("linear"))
            {
                t = MotionType.Linear;
            }
            else if (type.Equals("joint"))
            {
                t = MotionType.Joint;
            }

            if (t == MotionType.Undefined)
            {
                Console.WriteLine("Invalid motion type");
                return null;
            }

            return new ActionMotion(t);
        }

        ///// <summary>
        ///// Sets the coordinate system that will be used for future relative actions. This can be "global" or "world" (default) to refer to the system's global reference coordinates, or "local" to refer to the device's local reference frame. For example, for a robotic arm, the "global" coordinate system will be the robot's base, and the "local" one will be the coordinates of the end effector, after all translation and rotation transformations.
        ///// </summary>
        ///// <returns></returns>
        //[MultiReturn(new[] { "global", "local" })]
        //public static Dictionary<string, BAction> Coordinates()
        //{
        //    ActionCoordinates global = new ActionCoordinates(ReferenceCS.World);
        //    ActionCoordinates local = new ActionCoordinates(ReferenceCS.Local);

        //    return new Dictionary<string, BAction>
        //    {
        //        { "global", global },
        //        { "local", local }
        //    };
        //}

        /// <summary>
        /// Sets the coordinate system that will be used for future relative actions. This can be "global" or "world" (default) to refer to the system's global reference coordinates, or "local" to refer to the device's local reference frame. For example, for a robotic arm, the "global" coordinate system will be the robot's base, and the "local" one will be the coordinates of the tool tip.
        /// </summary>
        /// <param name="type">"global" or "local"</param>
        /// <returns>Set Reference Coordinate System Action</returns>
        public static BAction Coordinates(string type = "global")
        {
            ReferenceCS refcs;
            type = type.ToLower();
            if (type.Equals("global") || type.Equals("world"))
            {
                refcs = ReferenceCS.World;
            }
            else if (type.Equals("local"))
            {
                refcs = ReferenceCS.Local;
            }
            else
            {
                Console.WriteLine("Invalid reference coordinate system");
                return null;
            }

            return new ActionCoordinates(refcs);
        }

        /// <summary>
        /// Increases the speed in mm/s at which future transformation actions will run. Default value is 20 mm.
        /// </summary>
        /// <param name="speedInc">Speed increment in mm/s</param>
        /// <returns name="action">Increase Speed Action</returns>
        public static BAction Speed(double speedInc = 0)
        {
            return new ActionSpeed((int)Math.Round(speedInc), true);
        }

        /// <summary>
        /// Sets the speed in mm/s at which future transformation actions will run. Default value is 20 mm.
        /// </summary>
        /// <param name="speed">Speed value in mm/s</param>
        /// <returns name="action">Set Speed Action</returns>
        public static BAction SpeedTo(double speed = 20)
        {
            return new ActionSpeed((int)Math.Round(speed), false);
        }

        /// <summary>
        /// Increases the zone radius in mm at which the device will start transitioning to its next target transformation. You can think of this as a 'proximity precision' parameter to blend movement along consecutive waypoints. Default value is 5 mm.
        /// </summary>
        /// <param name="zoneInc">Increease in precision radius in mm</param>
        /// <returns name="action">Increase Zone Action</returns>
        public static BAction Zone(double zoneInc = 0)
        {
            return new ActionZone((int)Math.Round(zoneInc), true);
        }

        /// <summary>
        /// Sets the zone radius in mm at which the device will start transitioning to its next target transformation. You can think of this as a 'proximity precision' parameter to blend movement along consecutive waypoints. Default value is 5 mm.
        /// </summary>
        /// <param name="zone">Precision radius in mm</param>
        /// <returns name="action">Set Zone Action</returns>
        public static BAction ZoneTo(double zone = 5)
        {
            return new ActionZone((int)Math.Round(zone), false);
        }

        /// <summary>
        /// Stores current state settings to a buffer, so that temporary changes can be made, and settings can be reverted to the stored state later with PopSettings().
        /// </summary>
        /// <returns name="action">Push Settings Action</returns>
        public static BAction PushSettings()
        {
            return new ActionPushPop(true);
        }

        /// <summary>
        /// Reverts current settings to the state store by the last call to PushSettings().
        /// </summary>
        /// <returns name="action">Pop Settings Action</returns>
        public static BAction PopSettings()
        {
            return new ActionPushPop(false);
        }

       



        /// <summary>
        /// Moves the device along a speficied vector relative to its current position.
        /// </summary>
        /// <param name="direction">Translation direction</param>
        /// <returns name="action">Relative Translation Action</returns>
        public static BAction Move(Autodesk.DesignScript.Geometry.Vector direction)
        {
            return new ActionTranslation(Utils.Vec2BPoint(direction), true);
        }

        /// <summary>
        /// Moves the device to an absolute position in global coordinates.
        /// </summary>
        /// <param name="location">Target position</param>
        /// <returns name="action">Absolute Translation Action</returns>
        public static BAction MoveTo(Autodesk.DesignScript.Geometry.Point location)
        {
            return new ActionTranslation(new BPoint(location.X, location.Y, location.Z), false);
        }

        /// <summary>
        /// Rotates the device a specified angle in degrees along the specified vector.
        /// </summary>
        /// <param name="axis">Rotation axis. Positive rotation direction is defined by the right-hand rule.</param>
        /// <param name="angDegs">Rotation angle in degrees</param>
        /// <returns name="action">Relative Rotation Action</returns>
        public static BAction Rotate(Autodesk.DesignScript.Geometry.Vector axis, double angDegs)
        {
            return new ActionRotation(new Rotation(Utils.Vec2BPoint(axis), angDegs), true);
        }

        /// <summary>
        /// Rotate the devices to an absolute orientation defined by the two main X and Y axes of specified Plane.
        /// </summary>
        /// <param name="refPlane">Target spatial orientation</param>
        /// <returns name="action">Absolute Rotation Action</returns>
        public static BAction RotateTo(Plane refPlane)
        {
            BPoint vecX = Utils.Vec2BPoint(refPlane.XAxis);
            BPoint vecY = Utils.Vec2BPoint(refPlane.YAxis);

            return new ActionRotation(new Orientation(vecX, vecY), false);
        }

        /// <summary>
        /// Performs a compound relative rotation + translation transformation in a single action. Note that when performing relative transformations, the R+T versus T+R order matters.
        /// </summary>
        /// <param name="direction">Translation direction</param>
        /// <param name="axis">Rotation axis</param>
        /// <param name="angDegs">Rotation angle in degrees</param>
        /// <param name="moveFirst">Apply translation first? Otherwise, apply rotation first.</param>
        /// <returns name="action">Relative Transform Action</returns>
        public static BAction Transform(Autodesk.DesignScript.Geometry.Vector direction, Autodesk.DesignScript.Geometry.Vector axis, double angDegs, bool moveFirst = true)
        {
            return new ActionTransformation(Utils.Vec2BPoint(direction), new Rotation(Utils.Vec2BPoint(axis), angDegs), true, moveFirst);
        }

        /// <summary>
        /// Performs a compound absolute rotation + translation transformation, or in other words, sets both a new absolute position and orientation for the device in the same action, based on specified Plane.
        /// </summary>
        /// <param name="plane">Traget location + orientation</param>
        /// <returns name="action">Absolute Transform Action</returns>
        public static BAction TransformTo(Plane plane)
        {
            BPoint origin = Utils.Vec2BPoint(plane.Origin);
            BPoint vecX = Utils.Vec2BPoint(plane.XAxis);
            BPoint vecY = Utils.Vec2BPoint(plane.YAxis);

            return new ActionTransformation(origin, new Orientation(vecX, vecY), false, true);
        }

        /// <summary>
        /// Increase the rotation angle in degrees of the joints in mechanical devices, specially robotic arms.
        /// </summary>
        /// <param name="j1">Rotational increment in degrees for Joint 1</param>
        /// <param name="j2">Rotational increment in degrees for Joint 2</param>
        /// <param name="j3">Rotational increment in degrees for Joint 3</param>
        /// <param name="j4">Rotational increment in degrees for Joint 4</param>
        /// <param name="j5">Rotational increment in degrees for Joint 5</param>
        /// <param name="j6">Rotational increment in degrees for Joint 6</param>
        /// <returns name="action">Increase Joint Angles Action</returns>
        public static BAction Joints(double j1 = 0, double j2 = 0, double j3 = 0,
            double j4 = 0, double j5 = 0, double j6 = 0)
        {
            return new ActionJoints(new Joints(j1, j2, j3, j4, j5, j6), true);
        }

        /// <summary>
        /// Sets the rotation angle in degrees of the joints in mechanical devices, specially robotic arms.
        /// </summary>
        /// <param name="j1">Angular value in degrees for Joint 1</param>
        /// <param name="j2">Angular value in degrees for Joint 2</param>
        /// <param name="j3">Angular value in degrees for Joint 3</param>
        /// <param name="j4">Angular value in degrees for Joint 4</param>
        /// <param name="j5">Angular value in degrees for Joint 5</param>
        /// <param name="j6">Angular value in degrees for Joint 6</param>
        /// <returns name="action">Set Joint Angles Action</returns>
        public static BAction JointsTo(double j1 = 0, double j2 = 0, double j3 = 0,
            double j4 = 0, double j5 = 0, double j6 = 0)
        {
            return new ActionJoints(new Joints(j1, j2, j3, j4, j5, j6), false);
        }

        /// <summary>
        /// Pause program execution for a specified amount of time.
        /// </summary>
        /// <param name="millis">Pause time in milliseconds</param>
        /// <returns name="action">Wait Action</returns>
        public static BAction Wait(double millis = 0)
        {
            return new ActionWait((long)Math.Round(millis));
        }

        /// <summary>
        /// Displays a text message on the device. This will depend on the device's configuration. For example, for ABB robots it will display it on the FlexPendant's log window.
        /// </summary>
        /// <param name="message">String message to display</param>
        /// <returns name="action">Message Action</returns>
        public static BAction Message(string message = "Hello World!")
        {
            return new ActionMessage(message);
        }
    }






    //  ██████╗ ██████╗  ██████╗  ██████╗ ██████╗  █████╗ ███╗   ███╗███████╗
    //  ██╔══██╗██╔══██╗██╔═══██╗██╔════╝ ██╔══██╗██╔══██╗████╗ ████║██╔════╝
    //  ██████╔╝██████╔╝██║   ██║██║  ███╗██████╔╝███████║██╔████╔██║███████╗
    //  ██╔═══╝ ██╔══██╗██║   ██║██║   ██║██╔══██╗██╔══██║██║╚██╔╝██║╚════██║
    //  ██║     ██║  ██║╚██████╔╝╚██████╔╝██║  ██║██║  ██║██║ ╚═╝ ██║███████║
    //  ╚═╝     ╚═╝  ╚═╝ ╚═════╝  ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝
    //                                                                       

    public class Programs
    {
        /// <summary>
        /// Returns a human-readable representation of a list of Actions.
        /// </summary>
        /// <param name="actions">The list of Actions that conforms a program</param>
        /// <returns></returns>
        public static List<string> DisplayProgram(List<BAction> actions)
        {
            List<string> program = new List<string>();

            foreach (BAction a in actions)
            {
                program.Add(a.ToString());
            }

            return program;
        }

        /// <summary>
        /// Returns a representation of these Actions written on the device's native language. This is the code you would typically save as a file and manually load on the device's controller.
        /// </summary>
        /// <param name="bot">The Robot instance that will export this program.</param>
        /// <param name="actions">A program in the form of a list of Actions.</param>
        /// <param name="inlineTargets">If true, targets will be declared inline with the instruction. Otherwise, the will be declared and usedas independent variables.</param>
        /// <returns name="code">Device-specific program code</returns>
        public static List<string> ExportCode(Robot bot, List<BAction> actions, bool inlineTargets = true)
        {
            bot.Mode("offline");

            foreach (BAction a in actions)
            {
                bot.Do(a);
            }

            return bot.Export(inlineTargets);
        }

        /// <summary>
        /// Writes a list of strings to a file. Make sure Dynamo has the adequate Admin rights to write files to your system. 
        /// </summary>
        /// <param name="code">A List of Strings</param>
        /// <param name="filepath">The path where the file will be saved</param>
        /// <returns name="resultMsg">Success?</returns>
        public static string WriteToFile(List<string> code, string filepath)
        {
            string result;
            try
            {
                System.IO.File.WriteAllLines(filepath, code, System.Text.Encoding.ASCII);
                result = "Successfuly saved to " + filepath;
            }
            catch (Exception ex)
            {
                result = "Could not save to file " + filepath + ", ERROR: " + ex;
            }
            return result;
        }

    }





    //  ██╗   ██╗████████╗██╗██╗     ███████╗
    //  ██║   ██║╚══██╔══╝██║██║     ██╔════╝
    //  ██║   ██║   ██║   ██║██║     ███████╗
    //  ██║   ██║   ██║   ██║██║     ╚════██║
    //  ╚██████╔╝   ██║   ██║███████╗███████║
    //   ╚═════╝    ╚═╝   ╚═╝╚══════╝╚══════╝
    //                                       
    internal class Utils
    {
        internal static BPoint Vec2BPoint(Autodesk.DesignScript.Geometry.Vector v)
        {
            return new BPoint(v.X, v.Y, v.Z);
        }

        internal static BPoint Vec2BPoint(Autodesk.DesignScript.Geometry.Point p)
        {
            return new BPoint(p.X, p.Y, p.Z);
        }

    }

}
