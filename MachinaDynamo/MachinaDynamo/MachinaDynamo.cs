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
        /// Create a new Robot object.
        /// </summary>
        /// <param name="name">A name for this Robot.</param>
        /// <param name="brand">Input "ABB", "UR", "KUKA", or "HUMAN" (if you only need a human-readable representation of the actions of this Robot...)</param>
        /// <returns name="Robot">Your brand new Robot object</returns>
        public static Robot Create(string name = "MachinaRobot", string brand = "HUMAN")
        {
            return new Robot(name, brand);
        }

        /// <summary>
        /// Checks version and build numbers for the Machina library.
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

        /// <summary>
        /// Change the name of a robot's IO pin.
        /// </summary>
        /// <param name="bot"></param>
        /// <param name="name"></param>
        /// <param name="pin"></param>
        /// <param name="digital"></param>
        /// <returns></returns>
        public static Robot SetIOName(Robot bot, string name = "Digital_IO_1", int pin = 1, bool digital = true)
        {
            bot.SetIOName(name, pin, digital);
            return bot;
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
        internal Tools() { }

        /// <summary>
        /// Creates a Tool object.
        /// </summary>
        /// <param name="name">Tool name</param>
        /// <param name="basePlane">The base Plane where the Tool will be attached to the Robot</param>
        /// <param name="toolTipPlane">The Plane of the Tool Tip Center (TCP)</param>
        /// <param name="weight">Tool weight in Kg</param>
        /// <returns></returns>
        public static MTool Create(string name,
            Autodesk.DesignScript.Geometry.Plane basePlane,
            Autodesk.DesignScript.Geometry.Plane toolTipPlane,
            double weight = 0)
        {

            CoordinateSystem basePlaneCS = basePlane.ToCoordinateSystem();
            CoordinateSystem toolTipPlaneCS = toolTipPlane.ToCoordinateSystem();

            CoordinateSystem relativeCS = toolTipPlaneCS.PreMultiplyBy(basePlaneCS.Inverse());
            //CoordinateSystem relativeCS = toolTipPlaneCS.Transform(basePlaneCS.Inverse());  // this is the same thing

            MVector TCPPosition = new MVector(relativeCS.Origin.X, relativeCS.Origin.Y, relativeCS.Origin.Z);
            MVector centerOfGravity = new MVector(TCPPosition);
            centerOfGravity.Scale(0.5);
            MOrientation TCPOrientation = new MOrientation(relativeCS.XAxis.X, relativeCS.XAxis.Y, relativeCS.XAxis.Z,
                relativeCS.YAxis.X, relativeCS.YAxis.Y, relativeCS.YAxis.Z);

            // https://github.com/DynamoDS/Dynamo/wiki/Zero-Touch-Plugin-Development#dispose--using-statement
            basePlane.Dispose();
            toolTipPlaneCS.Dispose();
            relativeCS.Dispose();

            return new MTool(name, TCPPosition, TCPOrientation, weight, centerOfGravity);
        }

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
        internal Actions() { }

        /// <summary>
        /// Sets the current type of motion to be applied to future translation actions. This can be "linear" (default) for straight line movements in euclidean space, or "joint" for smooth interpolation between joint angles. NOTE: "joint" motion may produce unexpected trajectories resulting in reorientations or collisions. Use with caution!
        /// </summary>
        /// <param name="type">"linear" or "joint"</param>
        /// <returns>Set Motion Type Action</returns>
        public static MAction Motion(string type = "linear")
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
                //throw new Exception("Invalid motion type");
                DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("Invalid motion type");  // this is better messagewise, and specially if I want to return something other than null
                return null;
            }

            return new ActionMotion(t);
        }

        /// <summary>
        /// Sets the coordinate system that will be used for future relative actions. This can be "global" or "world" (default) to refer to the system's global reference coordinates, or "local" to refer to the device's local reference frame. For example, for a robotic arm, the "global" coordinate system will be the robot's base, and the "local" one will be the coordinates of the tool tip.
        /// </summary>
        /// <param name="type">"global" or "local"</param>
        /// <returns>Set Reference Coordinate System Action</returns>
        public static MAction Coordinates(string type = "global")
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
                DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("Invalid reference coordinate system");
                return null;
            }

            return new ActionCoordinates(refcs);
        }

        /// <summary>
        /// Increases the speed in mm/s at which future transformation actions will run. Default value is 20 mm.
        /// </summary>
        /// <param name="speedInc">Speed increment in mm/s</param>
        /// <returns name="action">Increase Speed Action</returns>
        public static MAction Speed(double speedInc = 0)
        {
            return new ActionSpeed((int)Math.Round(speedInc), true);
        }

        /// <summary>
        /// Sets the speed in mm/s at which future transformation actions will run. Default value is 20 mm.
        /// </summary>
        /// <param name="speed">Speed value in mm/s</param>
        /// <returns name="action">Set Speed Action</returns>
        public static MAction SpeedTo(double speed = 20)
        {
            return new ActionSpeed((int)Math.Round(speed), false);
        }

        /// <summary>
        /// Increases the zone radius in mm at which the device will start transitioning to its next target transformation. You can think of this as a 'proximity precision' parameter to blend movement along consecutive waypoints. Default value is 5 mm.
        /// </summary>
        /// <param name="zoneInc">Increease in precision radius in mm</param>
        /// <returns name="action">Increase Zone Action</returns>
        public static MAction Zone(double zoneInc = 0)
        {
            return new ActionZone((int)Math.Round(zoneInc), true);
        }

        /// <summary>
        /// Sets the zone radius in mm at which the device will start transitioning to its next target transformation. You can think of this as a 'proximity precision' parameter to blend movement along consecutive waypoints. Default value is 5 mm.
        /// </summary>
        /// <param name="zone">Precision radius in mm</param>
        /// <returns name="action">Set Zone Action</returns>
        public static MAction ZoneTo(double zone = 5)
        {
            return new ActionZone((int)Math.Round(zone), false);
        }

        /// <summary>
        /// Stores current state settings to a buffer, so that temporary changes can be made, and settings can be reverted to the stored state later with PopSettings().
        /// </summary>
        /// <returns name="action">Push Settings Action</returns>
        public static MAction PushSettings()
        {
            return new ActionPushPop(true);
        }

        /// <summary>
        /// Reverts current settings to the state store by the last call to PushSettings().
        /// </summary>
        /// <returns name="action">Pop Settings Action</returns>
        public static MAction PopSettings()
        {
            return new ActionPushPop(false);
        }





        /// <summary>
        /// Moves the device along a speficied vector relative to its current position.
        /// </summary>
        /// <param name="direction">Translation direction</param>
        /// <returns name="action">Relative Translation Action</returns>
        public static MAction Move(Autodesk.DesignScript.Geometry.Vector direction)
        {
            return new ActionTranslation(Utils.Vec2BPoint(direction), true);
        }

        /// <summary>
        /// Moves the device to an absolute position in global coordinates.
        /// </summary>
        /// <param name="location">Target position</param>
        /// <returns name="action">Absolute Translation Action</returns>
        public static MAction MoveTo(Autodesk.DesignScript.Geometry.Point location)
        {
            return new ActionTranslation(new MPoint(location.X, location.Y, location.Z), false);
        }

        /// <summary>
        /// Rotates the device a specified angle in degrees along the specified vector.
        /// </summary>
        /// <param name="axis">Rotation axis. Positive rotation direction is defined by the right-hand rule.</param>
        /// <param name="angDegs">Rotation angle in degrees</param>
        /// <returns name="action">Relative Rotation Action</returns>
        public static MAction Rotate(Autodesk.DesignScript.Geometry.Vector axis, double angDegs)
        {
            return new ActionRotation(new Rotation(Utils.Vec2BPoint(axis), angDegs), true);
        }

        /// <summary>
        /// Rotate the devices to an absolute orientation defined by the two main X and Y axes of specified Plane.
        /// </summary>
        /// <param name="refPlane">Target spatial orientation</param>
        /// <returns name="action">Absolute Rotation Action</returns>
        public static MAction RotateTo(Plane refPlane)
        {
            return new ActionRotation(
                new MOrientation(refPlane.XAxis.X, refPlane.XAxis.Y, refPlane.XAxis.Z, refPlane.YAxis.X, refPlane.YAxis.Y, refPlane.YAxis.Z),
                false);
        }

        /// <summary>
        /// Performs a compound relative rotation + translation transformation in a single action. Note that when performing relative transformations, the R+T versus T+R order matters.
        /// </summary>
        /// <param name="direction">Translation direction</param>
        /// <param name="axis">Rotation axis</param>
        /// <param name="angDegs">Rotation angle in degrees</param>
        /// <param name="moveFirst">Apply translation first? Otherwise, apply rotation first.</param>
        /// <returns name="action">Relative Transform Action</returns>
        public static MAction Transform(Autodesk.DesignScript.Geometry.Vector direction, Autodesk.DesignScript.Geometry.Vector axis, double angDegs, bool moveFirst = true)
        {
            return new ActionTransformation(
                Utils.Vec2BPoint(direction),
                new Rotation(Utils.Vec2BPoint(axis), angDegs),
                true,
                moveFirst);
        }

        /// <summary>
        /// Performs a compound absolute rotation + translation transformation, or in other words, sets both a new absolute position and orientation for the device in the same action, based on specified Plane.
        /// </summary>
        /// <param name="plane">Traget location + orientation</param>
        /// <returns name="action">Absolute Transform Action</returns>
        public static MAction TransformTo(Plane plane)
        {
            return new ActionTransformation(
                new MPoint(plane.Origin.X, plane.Origin.Y, plane.Origin.Z),
                new MOrientation(plane.XAxis.X, plane.XAxis.Y, plane.XAxis.Z, plane.YAxis.X, plane.YAxis.Y, plane.YAxis.Z),
                false,
                true);
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
        public static MAction Joints(double j1 = 0, double j2 = 0, double j3 = 0,
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
        public static MAction JointsTo(double j1 = 0, double j2 = 0, double j3 = 0,
            double j4 = 0, double j5 = 0, double j6 = 0)
        {
            return new ActionJoints(new Joints(j1, j2, j3, j4, j5, j6), false);
        }

        /// <summary>
        /// Pause program execution for a specified amount of time.
        /// </summary>
        /// <param name="millis">Pause time in milliseconds</param>
        /// <returns name="action">Wait Action</returns>
        public static MAction Wait(double millis = 0)
        {
            return new ActionWait((long)Math.Round(millis));
        }

        /// <summary>
        /// Displays a text message on the device. This will depend on the device's configuration. For example, for ABB robots it will display it on the FlexPendant's log window.
        /// </summary>
        /// <param name="message">String message to display</param>
        /// <returns name="action">Message Action</returns>
        public static MAction Message(string message = "Hello World!")
        {
            return new ActionMessage(message);
        }

        /// <summary>
        /// Displays an internal comment in a program compilation. This is useful for internal annotations or reminders, but has no effect on the Robot's behavior. 
        /// </summary>
        /// <param name="comment">The comment to be displayed on code compilation</param>
        /// <returns></returns>
        public static MAction Comment(string comment = "This is a comment")
        {
            return new ActionComment(comment);
        }

        /// <summary>
        /// Attach a Tool to the flange of the object, replacing whichever tool was on it before. Note that the Tool Center Point (TCP) will be translated/rotated according to the difference between tools.
        /// </summary>
        /// <param name="tool">A Tool object to attach to the Robot flange</param>
        /// <returns></returns>
        public static MAction Attach(MTool tool)
        {
            return new ActionAttach(tool);
        }

        /// <summary>
        /// Detach any Tool currently attached to the Robot. Note that the Tool Center Point (TCP) will now be transformed to the Robot's flange.
        /// </summary>
        /// <returns></returns>
        public static MAction Detach()
        {
            return new ActionDetach();
        }

        /// <summary>
        /// Activate/deactivate digital output. 
        /// </summary>
        /// <param name="ioNum"></param>
        /// <param name="isOn"></param>
        /// <returns></returns>
        public static MAction WriteDigital(int ioNum, bool isOn)
        {
            return new ActionIODigital(ioNum, isOn);
        }

        /// <summary>
        /// Send a value to analog output.
        /// </summary>
        /// <param name="ioNum"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MAction WriteAnalog(int ioNum, double value)
        {
            return new ActionIOAnalog(ioNum, value);
        }

        /// <summary>
        /// Turn digital output on. Alias for `WriteDigital(ioNum, true)`
        /// </summary>
        /// <param name="ioNum"></param>
        /// <returns></returns>
        public static MAction TurnOn(int ioNum)
        {
            return new ActionIODigital(ioNum, true);
        }

        /// <summary>
        /// Turn digital output off. Alias for `WriteDigital(ioNum, false)` 
        /// </summary>
        /// <param name="ioNum"></param>
        /// <returns></returns>
        public static MAction TurnOff(int ioNum)
        {
            return new ActionIODigital(ioNum, false);
        }

    }







    //  ██████╗ ██████╗  ██████╗  ██████╗ ██████╗  █████╗ ███╗   ███╗███████╗
    //  ██╔══██╗██╔══██╗██╔═══██╗██╔════╝ ██╔══██╗██╔══██╗████╗ ████║██╔════╝
    //  ██████╔╝██████╔╝██║   ██║██║  ███╗██████╔╝███████║██╔████╔██║███████╗
    //  ██╔═══╝ ██╔══██╗██║   ██║██║   ██║██╔══██╗██╔══██║██║╚██╔╝██║╚════██║
    //  ██║     ██║  ██║╚██████╔╝╚██████╔╝██║  ██║██║  ██║██║ ╚═╝ ██║███████║
    //  ╚═╝     ╚═╝  ╚═╝ ╚═════╝  ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝
    //                                                                       
    /// <summary>
    /// Program creation and visualization options.
    /// </summary>
    public class Programs
    {
        internal Programs() { }

        /// <summary>
        /// Returns a human-readable representation of a list of Actions.
        /// </summary>
        /// <param name="actions">The list of Actions that conforms a program</param>
        /// <returns></returns>
        public static List<string> DisplayProgram(List<MAction> actions)
        {
            List<string> program = new List<string>();

            foreach (MAction a in actions)
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
        /// <param name="inlineTargets">If true, targets will be declared inline with the instruction. Otherwise, the will be declared and used as independent variables.</param>
        /// <returns name="code">Device-specific program code</returns>
        public static string ExportCode(Robot bot, List<MAction> actions, bool inlineTargets = true, bool machinaComments = true)
        {
            bot.Mode("offline");

            foreach (MAction a in actions)
            {
                bot.Do(a);
            }

            List<string> codeLines = bot.Export(inlineTargets, machinaComments);
            StringWriter writer = new StringWriter();
            for (var i = 0; i < codeLines.Count; i++)
            {
                writer.WriteLine(codeLines[i]);
            }
            string code = writer.ToString();
            writer.Dispose();  // just in case ;)
            return code;
        }

        /// <summary>
        /// Writes a list of strings to a file. Make sure Dynamo has the adequate Admin rights to write files to your system. 
        /// </summary>
        /// <param name="code">A List of Strings</param>
        /// <param name="filepath">The path where the file will be saved</param>
        /// <returns name="resultMsg">Success?</returns>
        internal static string WriteToFile(List<string> code, string filepath)
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
    /// <summary>
    /// Internal utilities, mainly data type conversion.
    /// </summary>
    internal class Utils
    {
        internal static MPoint Vec2BPoint(Autodesk.DesignScript.Geometry.Vector v)
        {
            return new MPoint(v.X, v.Y, v.Z);
        }

        internal static MPoint Vec2BPoint(Autodesk.DesignScript.Geometry.Point p)
        {
            return new MPoint(p.X, p.Y, p.Z);
        }

    }

}
