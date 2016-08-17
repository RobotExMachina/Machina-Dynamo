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

namespace BRobot
{
    public class BRobot
    {



        //  ██████╗  █████╗ ███████╗███████╗
        //  ██╔══██╗██╔══██╗██╔════╝██╔════╝
        //  ██████╔╝███████║███████╗█████╗  
        //  ██╔══██╗██╔══██║╚════██║██╔══╝  
        //  ██████╔╝██║  ██║███████║███████╗
        //  ╚═════╝ ╚═╝  ╚═╝╚══════╝╚══════╝
        //                                  
        /// <summary>
        /// Create a new BRobot object.
        /// </summary>
        /// <returns name="BRobot">Your brand new BRobot</returns>
        public static Robot Create()
        {
            return new Robot();
        }

        /// <summary>
        /// Checks version and build numbers for this BRobot
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









        //   █████╗  ██████╗████████╗██╗ ██████╗ ███╗   ██╗███████╗
        //  ██╔══██╗██╔════╝╚══██╔══╝██║██╔═══██╗████╗  ██║██╔════╝
        //  ███████║██║        ██║   ██║██║   ██║██╔██╗ ██║███████╗
        //  ██╔══██║██║        ██║   ██║██║   ██║██║╚██╗██║╚════██║
        //  ██║  ██║╚██████╗   ██║   ██║╚██████╔╝██║ ╚████║███████║
        //  ╚═╝  ╚═╝ ╚═════╝   ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝
        //                                                         

        /// <summary>
        /// Sets the current type of motion to be applied to future translation actions. This can be "linear" (default) for straight line movements in euclidean space, or "joint" for linear transitions between joint angles (linear movement in robot configuration space).
        /// </summary>
        /// <returns></returns>
        [MultiReturn(new[] { "linear", "joint" })]
        public static Dictionary<string, BAction> Motion()
        {
            ActionMotion lin = new ActionMotion(MotionType.Linear);
            ActionMotion jnt = new ActionMotion(MotionType.Joint);

            return new Dictionary<string, BAction>
            {
                { "linear", lin },
                { "joint", jnt }
            };
        }

        /// <summary>
        /// Sets the coordinate system that will be used for future relative actions. This can be "global" or "world" (default) to refer to the system's global reference coordinates, or "local" to refer to the device's local reference frame. For example, for a robotic arm, the "global" coordinate system will be the robot's base, and the "local" one will be the coordinates of the end effector, after all translation and rotation transformations.
        /// </summary>
        /// <returns></returns>
        [MultiReturn(new[] { "global", "local" })]
        public static Dictionary<string, BAction> Coordinates()
        {
            ActionCoordinates global = new ActionCoordinates(ReferenceCS.World);
            ActionCoordinates local = new ActionCoordinates(ReferenceCS.Local);

            return new Dictionary<string, BAction>
            {
                { "global", global },
                { "local", local }
            };
        }

        /// <summary>
        /// Increases the speed in mm/s at which future transformation actions will run. Default value is 20.
        /// </summary>
        /// <param name="speedInc"></param>
        /// <returns name="action">Increase Speed Action</returns>
        public static BAction Speed(double speedInc)
        {
            return new ActionSpeed((int)Math.Round(speedInc), true);
        }

        /// <summary>
        /// Sets the speed in mm/s at which future transformation actions will run. Default value is 20.
        /// </summary>
        /// <param name="speed"></param>
        /// <returns name="action">Set Speed Action</returns>
        public static BAction SpeedTo(double speed)
        {
            return new ActionSpeed((int)Math.Round(speed), false);
        }

        /// <summary>
        /// Increases the zone radius in mm at which the device will start transitioning to its next target transformation. You can think of this as a 'proximity precision' parameter to blend movement along consecutive waypoints. Default value is 5 mm.
        /// </summary>
        /// <param name="zoneInc"></param>
        /// <returns name="action">Increase Zone Action</returns>
        public static BAction Zone(double zoneInc)
        {
            return new ActionZone((int)Math.Round(zoneInc), true);
        }

        /// <summary>
        /// Sets the zone radius in mm at which the device will start transitioning to its next target transformation. You can think of this as a 'proximity precision' parameter to blend movement along consecutive waypoints. Default value is 5 mm.
        /// </summary>
        /// <param name="zone"></param>
        /// <returns name="action">Set Zone Action</returns>
        public static BAction ZoneTo(double zone)
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
        /// <param name="direction"></param>
        /// <returns name="action">Relative Translation Action</returns>
        public static BAction Move(Vector direction)
        {
            return new ActionTranslation(Vec2BPoint(direction), true);
        }

        /// <summary>
        /// Moves the device to an absolute position in global coordinates.
        /// </summary>
        /// <param name="location"></param>
        /// <returns name="action">Absolute Translation Action</returns>
        public static BAction MoveTo(Autodesk.DesignScript.Geometry.Point location)
        {
            return new ActionTranslation(new BPoint(location.X, location.Y, location.Z), false);
        }

        /// <summary>
        /// Rotates the device a specified angle in degrees along the specified vector.
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="angDegs"></param>
        /// <returns name="action">Relative Rotation Action</returns>
        public static BAction Rotate(Vector axis, double angDegs)
        {
            return new ActionRotation(new Rotation(Vec2BPoint(axis), angDegs), true);
        }

        /// <summary>
        /// Rotate the devices to an absolute orientation defined by the two main X and Y axes of specified Plane.
        /// </summary>
        /// <param name="refPlane"></param>
        /// <returns name="action">Absolute Rotation Action</returns>
        public static BAction RotateTo(Plane refPlane)
        {
            BPoint vecX = Vec2BPoint(refPlane.XAxis);
            BPoint vecY = Vec2BPoint(refPlane.YAxis);

            return new ActionRotation(new Rotation(vecX, vecY), false);
        }

        /// <summary>
        /// Performs a compound relative rotation + translation transformation in a single action. Note that when performing relative transformations, the R+T versus T+R order matters.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="axis"></param>
        /// <param name="angDegs"></param>
        /// <param name="moveFirst">Apply translation first? Otherwise, apply rotation first.</param>
        /// <returns name="action">Relative Transform Action</returns>
        public static BAction Transform(Vector direction, Vector axis, double angDegs, bool moveFirst = true)
        {
            return new ActionTransformation(Vec2BPoint(direction), new Rotation(Vec2BPoint(axis), angDegs), true, moveFirst);
        }

        /// <summary>
        /// Performs a compound absolute rotation + translation transformation, or in other words, sets both a new absolute position and orientation for the device in the same action, based on specified Plane.
        /// </summary>
        /// <param name="plane"></param>
        /// <returns name="action">Absolute Transform Action</returns>
        public static BAction TransformTo(Plane plane)
        {
            BPoint origin = Vec2BPoint(plane.Origin);
            BPoint vecX = Vec2BPoint(plane.XAxis);
            BPoint vecY = Vec2BPoint(plane.YAxis);

            return new ActionTransformation(origin, new Rotation(vecX, vecY), false, true);
        }

        /// <summary>
        /// Increase the rotation angle in degrees of the joints in mechanical devices, specially robotic arms.
        /// </summary>
        /// <param name="j1"></param>
        /// <param name="j2"></param>
        /// <param name="j3"></param>
        /// <param name="j4"></param>
        /// <param name="j5"></param>
        /// <param name="j6"></param>
        /// <returns name="action">Increase Joint Angles Action</returns>
        public static BAction Joints(double j1 = 0, double j2 = 0, double j3 = 0, 
            double j4 = 0, double j5 = 0, double j6 = 0)
        {
            return new ActionJoints(new Joints(j1, j2, j3, j4, j5, j6), true);
        }

        /// <summary>
        /// Sets the rotation angle in degrees of the joints in mechanical devices, specially robotic arms.
        /// </summary>
        /// <param name="j1"></param>
        /// <param name="j2"></param>
        /// <param name="j3"></param>
        /// <param name="j4"></param>
        /// <param name="j5"></param>
        /// <param name="j6"></param>
        /// <returns name="action">Set Joint Angles Action</returns>
        public static BAction JointsTo(double j1 = 0, double j2 = 0, double j3 = 0,
            double j4 = 0, double j5 = 0, double j6 = 0)
        {
            return new ActionJoints(new Joints(j1, j2, j3, j4, j5, j6), false);
        }

        /// <summary>
        /// Pause program execution for specified milliseconds.
        /// </summary>
        /// <param name="millis"></param>
        /// <returns name="action">Wait Action</returns>
        public static BAction Wait(double millis)
        {
            return new ActionWait((long)Math.Round(millis));
        }

        /// <summary>
        /// Displays a text message on the device. This will depend on the device's configuration. For example, for ABB robots it will display it on the FlexPendant's log window.
        /// </summary>
        /// <param name="message"></param>
        /// <returns name="action">Message Action</returns>
        public static BAction Message(string message)
        {
            return new ActionMessage(message);
        }

        







        //  ██╗   ██╗████████╗██╗██╗     ███████╗
        //  ██║   ██║╚══██╔══╝██║██║     ██╔════╝
        //  ██║   ██║   ██║   ██║██║     ███████╗
        //  ██║   ██║   ██║   ██║██║     ╚════██║
        //  ╚██████╔╝   ██║   ██║███████╗███████║
        //   ╚═════╝    ╚═╝   ╚═╝╚══════╝╚══════╝
        //                                       
        /// <summary>
        /// Returns a human-readable representation of a list of Actions.
        /// </summary>
        /// <param name="actions"></param>
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
        /// Generates a program following the speficied Actions written on the device's native language.  
        /// </summary>
        /// <param name="bot"></param>
        /// <param name="actions"></param>
        /// <returns name="code">Device-specific program code</returns>
        public static List<string> ExportCode(Robot bot, List<BAction> actions)
        {
            bot.Mode("offline");

            foreach (BAction a in actions)
            {
                bot.Do(a);
            }

            return bot.Export();
        }

        /// <summary>
        /// Writes a list of strings to a file.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="filepath"></param>
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







        //  ██╗███╗   ██╗████████╗███████╗██████╗ ███╗   ██╗ █████╗ ██╗     
        //  ██║████╗  ██║╚══██╔══╝██╔════╝██╔══██╗████╗  ██║██╔══██╗██║     
        //  ██║██╔██╗ ██║   ██║   █████╗  ██████╔╝██╔██╗ ██║███████║██║     
        //  ██║██║╚██╗██║   ██║   ██╔══╝  ██╔══██╗██║╚██╗██║██╔══██║██║     
        //  ██║██║ ╚████║   ██║   ███████╗██║  ██║██║ ╚████║██║  ██║███████╗
        //  ╚═╝╚═╝  ╚═══╝   ╚═╝   ╚══════╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝╚══════╝
        //                     
        /// <summary>
        /// Internal constructor
        /// </summary>
        internal BRobot() { }

        internal static BPoint Vec2BPoint(Vector v)
        {
            return new BPoint(v.X, v.Y, v.Z);
        }

        internal static BPoint Vec2BPoint(Autodesk.DesignScript.Geometry.Point p)
        {
            return new BPoint(p.X, p.Y, p.Z);
        }
    }
}
