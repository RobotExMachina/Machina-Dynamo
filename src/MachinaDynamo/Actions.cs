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
        /// Internal constructor
        /// </summary>
        internal Actions() { }


        /// <summary>
        /// Attach a Tool to the flange of the Robot, replacing whichever tool was on it before. Note that the Tool Center Point (TCP) will be translated/rotated according to the tool change.
        /// </summary>
        /// <param name="tool">A Tool object to attach to the Robot flange</param>
        /// <returns name="Action">Attach Action</returns>
        public static MAction Attach(MTool tool) => new ActionAttach(tool);


        /// <summary>
        /// Increase the axes' rotation angle in degrees at the joints of mechanical devices, specially robotic arms.
        /// </summary>
        /// <param name="a1inc">Rotational increment in degrees for Axis 1</param>
        /// <param name="a2inc">Rotational increment in degrees for Axis 2</param>
        /// <param name="a3inc">Rotational increment in degrees for Axis 3</param>
        /// <param name="a4inc">Rotational increment in degrees for Axis 4</param>
        /// <param name="a5inc">Rotational increment in degrees for Axis 5</param>
        /// <param name="a6inc">Rotational increment in degrees for Axis 6</param>
        /// <returns name="Action">Axes Action</returns>
        public static MAction Axes(double a1inc = 0, double a2inc = 0, double a3inc = 0,
            double a4inc = 0, double a5inc = 0, double a6inc = 0) => 
            new ActionAxes(new Joints(a1inc, a2inc, a3inc, a4inc, a5inc, a6inc), true);


        /// <summary>
        /// Set the axes' rotation angle in degrees at the joints of mechanical devices, specially robotic arms.
        /// </summary>
        /// <param name="a1">Angular value in degrees for Axis 1</param>
        /// <param name="a2">Angular value in degrees for Axis 2</param>
        /// <param name="a3">Angular value in degrees for Axis 3</param>
        /// <param name="a4">Angular value in degrees for Axis 4</param>
        /// <param name="a5">Angular value in degrees for Axis 5</param>
        /// <param name="a6">Angular value in degrees for Axis 6</param>
        /// <returns name="Action">Axes Action</returns>
        public static MAction AxesTo(double a1, double a2, double a3,
            double a4, double a5, double a6)
            => new ActionAxes(new Joints(a1, a2, a3, a4, a5, a6), false);


        /// <summary>
        /// Displays an internal comment in a program compilation. This is useful for internal annotations or reminders, but has no effect on the Robot's behavior.
        /// </summary>
        /// <param name="comment">Comment to be displayed on code compilation</param>
        /// <returns name="Action"></returns>
        public static MAction Comment(string comment = "Comment goes here") => new ActionComment(comment);


        /// <summary>
        /// Detach any Tool currently attached to the Robot. Note that the Tool Center Point (TCP) will now be transformed to the Robot's flange.
        /// </summary>
        /// <returns name="Action">Detach Action</returns>
        public static MAction Detach() => new ActionDetach();


        /// <summary>
        /// Turns extrusion in 3D printers on/off.
        /// </summary>
        /// <param name="extrude">True/false for on/off</param>
        /// <returns name="Action">Extrude Action</returns>
        public static MAction Extrude(bool extrude = false) => new ActionExtrusion(extrude);


        /// <summary>
        /// Increases the extrusion rate of filament for 3D printers.
        /// </summary>
        /// <param name="rateInc">Increment of mm of filament per mm of movement</param>
        /// <returns name="Action">ExtrusionRate Action</returns>
        public static MAction ExtrusionRate(double rateInc = 0) => new ActionExtrusionRate(rateInc, true);


        /// <summary>
        /// Sets the extrusion rate of filament for 3D printers.
        /// </summary>
        /// <param name="rate">Value of mm of filament per mm of movement</param>
        /// <returns name="Action">ExtrusionRate Action</returns>
        public static MAction ExtrusionRateTo(double rate = 0) => new ActionExtrusionRate(rate, true);
        

        /// <summary>
        /// Displays a text message on the device. This will depend on the device's configuration, e.g. ABB robots it will display it on the FlexPendant's log window.
        /// </summary>
        /// <param name="message">Text message to display</param>
        /// <returns name="Action">Message Action</returns>
        public static MAction Message(string message = "Hello Machina!") => new ActionMessage(message);


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
                    return new ActionMotion(mt);
                }
            }
            catch { }

            // this is better messagewise, and specially if I want to return something other than null
            DynamoServices.LogWarningMessageEvents.OnLogWarningMessage($"{mode} is not a valid option for MotionMode changes, please specify one of the following: {Utils.EnumerateList(Enum.GetNames(typeof(MotionType)), "or")}");
            return null;
        }


        /// <summary>
        /// Moves the device along a speficied vector relative to its current position.
        /// </summary>
        /// <param name="direction">Translation vector</param>
        /// <returns name="Action">Move Action</returns>
        public static MAction Move(Autodesk.DesignScript.Geometry.Vector direction) => new ActionTranslation(Utils.Vec2BPoint(direction), true);


        /// <summary>
        /// Moves the device to an absolute location.
        /// </summary>
        /// <param name="location">Target Point</param>
        /// <returns name="Action">Move Action</returns>
        public static MAction MoveTo(Autodesk.DesignScript.Geometry.Point location) => new ActionTranslation(new MPoint(location.X, location.Y, location.Z), false);


        /// <summary>
        /// Reverts current settings to the state store by the last call to PushSettings().
        /// </summary>
        /// <returns name="Action">PopSettings Action</returns>
        public static MAction PopSettings() => new ActionPushPop(false);


        /// <summary>
        /// Increase the precision at which future actions will execute. Precision is measured as the radius of the smooth interpolation between motion targets. This is refered to as "Zone", "Approximate Positioning" or "Blending Radius" in different platforms. 
        /// </summary>
        /// <param name="radiusInc">Radius increment in mm</param>
        /// <returns name="Action">Precision Action</returns>
        public static MAction Precision(double radiusInc = 0) => new ActionPrecision(radiusInc, true);


        /// <summary>
        /// Set the precision at which future actions will execute. Precision is measured as the radius of the smooth interpolation between motion targets. This is refered to as "Zone", "Approximate Positioning" or "Blending Radius" in different platforms. 
        /// </summary>
        /// <param name="radius">Radius value in mm</param>
        /// <returns name="Action">Precision Action</returns>
        public static MAction PrecisionTo(double radius = 5) => new ActionPrecision(radius, false);


        /// <summary>
        /// Stores current state settings to a buffer, so that temporary changes can be made, and settings can be reverted to the stored state later with PopSettings().
        /// </summary>
        /// <returns name="Action">PushSettings Action</returns>
        public static MAction PushSettings() => new ActionPushPop(true);


        /// <summary>
        /// Rotates the device a specified angle in degrees along the specified vector.
        /// </summary>
        /// <param name="axis">Rotation axis, with positive rotation direction is defined by the right-hand rule.</param>
        /// <param name="angDegs">Rotation angle in degrees</param>
        /// <returns name="Action">Rotate Action</returns>
        public static MAction Rotate(Autodesk.DesignScript.Geometry.Vector axis, double angDegs) => new ActionRotation(new Rotation(Utils.Vec2BPoint(axis), angDegs), true);


        /// <summary>
        /// Rotates the device to a specified orientation.
        /// </summary>
        /// <param name="refPlane">Target spatial orientation</param>
        /// <returns name="Action">Rotate Action</returns>
        public static MAction RotateTo(Plane refPlane) => 
            new ActionRotation(
                new MOrientation(refPlane.XAxis.X, refPlane.XAxis.Y, refPlane.XAxis.Z, refPlane.YAxis.X, refPlane.YAxis.Y, refPlane.YAxis.Z),
                false);


        /// <summary>
        /// Increases the speed at which future actions will execute.
        /// </summary>
        /// <param name="speedInc">Speed increment in mm/s or deg/sec</param>
        /// <returns name="Action">Speed Action</returns>
        public static MAction Speed(double speedInc = 0) => new ActionSpeed(speedInc, true);


        /// <summary>
        /// Sets the speed at which future actions will execute.
        /// </summary>
        /// <param name="speed">Speed value in mm/s or deg/sec</param>
        /// <returns name="Action">Speed Action</returns>
        public static MAction SpeedTo(double speed = 0)
        {
            if (speed < 0)
            {
                DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("The speed value cannot be negative");
                return null;
            }
            return new ActionSpeed(speed, false);
        }

        /// <summary>
        /// Increase the TCP acceleration value new Actions will be ran at.
        /// </summary>
        /// <param name="accInc">TCP acceleration increment in mm/s^2. Decreasing the total to zero or less will reset it back the robot's default.</param>
        /// <returns></returns>
        public static MAction Acceleration(double accInc = 0) => new ActionAcceleration(accInc, true);


        public static MAction AccelerationTo(double acceleration) => new ActionAcceleration(acceleration, false);

        /// <summary>
        /// Increment the working temperature of one of the device's parts. Useful for 3D printing operations.
        /// </summary>
        /// <param name="tempInc">Temperature increment in °C</param>
        /// <param name="part">Device's part that will change temperature, e.g. "extruder", "bed", etc.</param>
        /// <param name="wait">If true, execution will wait for the part to heat up and resume when reached the target.</param>
        /// <returns name="Action"></returns>
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
        /// <returns name="Action"></returns>
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


        /// <summary>
        /// Performs a compound relative transformation as a rotation + translation. Note that when performing relative transformations, the R+T versus T+R order matters.
        /// </summary>
        /// <param name="direction">Translation vector</param>
        /// <param name="axis">Rotation axis</param>
        /// <param name="angDegs">Rotation angle in degrees</param>
        /// <param name="moveFirst">Apply translation first? Note that when performing relative transformations, the R+T versus T+R order matters.</param>
        /// <returns name="Action">Transform Action</returns>
        public static MAction Transform(Autodesk.DesignScript.Geometry.Vector direction, Autodesk.DesignScript.Geometry.Vector axis, double angDegs, bool moveFirst = true) => 
            new ActionTransformation(
                Utils.Vec2BPoint(direction),
                new Rotation(Utils.Vec2BPoint(axis), angDegs),
                true,
                moveFirst);


        /// <summary>
        /// Performs a compound absolute transformation to target Plane. The device's new absolute position and orientation will be those of the Plane.
        /// </summary>
        /// <param name="plane">Target Plane to transform to</param>
        /// <returns name="Action">Transform Action</returns>
        public static MAction TransformTo(Plane plane) =>
            new ActionTransformation(
                new MPoint(plane.Origin.X, plane.Origin.Y, plane.Origin.Z),
                new MOrientation(plane.XAxis.X, plane.XAxis.Y, plane.XAxis.Z, plane.YAxis.X, plane.YAxis.Y, plane.YAxis.Z),
                false,
                true);


        /// <summary>
        /// Pause program execution for a specified amount of time.
        /// </summary>
        /// <param name="millis">Pause time in milliseconds</param>
        /// <returns name="Action">Wait Action</returns>
        public static MAction Wait(double millis = 0) => new ActionWait((long)Math.Round(millis));


        /// <summary>
        /// Send a value to analog output.
        /// </summary>
        /// <param name="analogPin">Analog pin name or number</param>
        /// <param name="value">Value to send to pin</param>
        /// <param name="toolPin">Is this pin on the robot's tool?</param>
        /// <returns name="Action"></returns>
        public static MAction WriteAnalog(string analogPin = "1", double value = 0, bool toolPin = false) => new ActionIOAnalog(analogPin, value, toolPin);


        /// <summary>
        /// Activate/deactivate digital output. 
        /// </summary>
        /// <param name="digitalPin">Digital pin name or number</param>
        /// <param name="isOn">Turn on?</param>
        /// <param name="toolPin">Is this pin on the robot's tool?</param>
        /// <returns name="Action"></returns>
        public static MAction WriteDigital(string digitalPin = "1", bool isOn = false, bool toolPin = false) => new ActionIODigital(digitalPin, isOn, toolPin);





































        //   ██████╗ ██████╗  █████╗ ██╗   ██╗███████╗██╗   ██╗ █████╗ ██████╗ ██████╗ 
        //  ██╔════╝ ██╔══██╗██╔══██╗██║   ██║██╔════╝╚██╗ ██╔╝██╔══██╗██╔══██╗██╔══██╗
        //  ██║  ███╗██████╔╝███████║██║   ██║█████╗   ╚████╔╝ ███████║██████╔╝██║  ██║
        //  ██║   ██║██╔══██╗██╔══██║╚██╗ ██╔╝██╔══╝    ╚██╔╝  ██╔══██║██╔══██╗██║  ██║
        //  ╚██████╔╝██║  ██║██║  ██║ ╚████╔╝ ███████╗   ██║   ██║  ██║██║  ██║██████╔╝
        //   ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝  ╚═══╝  ╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═════╝ 
        //                                                                             
        // Legacy components, deprecated or on their way to change. 
        

        /// <summary>
        /// LET'S PUT A PIN ON THIS ONE... SOME DEEP CHANGES NEED TO HAPPEN AT CORE, SO THIS IS STAYING HERE
        /// FOR LEGACY PURPOSES, AND WILL BE REWRITTEN AS SOON AS CORE BRINGS IN A NEW MODEL
        /// </summary>
        /// <summary>
        /// Sets the coordinate system that will be used for future relative actions. This can be "global" or "world" (default) to refer to the system's global reference coordinates, or "local" to refer to the device's local reference frame. For example, for a robotic arm, the "global" coordinate system will be the robot's base, and the "local" one will be the coordinates of the tool tip.
        /// </summary>
        /// <param name="type">"global" or "local"</param>
        /// <returns>Set Reference Coordinate System Action</returns>
        [IsVisibleInDynamoLibrary(false)]
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
        /// Turn digital output off. Alias for `WriteDigital(ioNum, false)` 
        /// </summary>
        /// <param name="ioNum"></param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(false)]
        public static MAction TurnOff(int ioNum)
        {
            //return new ActionIODigital(ioNum, false);
            DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("Deprecated component: use WriteDigital(ioNum, false) instead");
            return null;
        }

        /// <summary>
        /// Turn digital output on. Alias for `WriteDigital(ioNum, true)`
        /// </summary>
        /// <param name="ioNum">Digital pin number</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(false)]
        public static MAction TurnOn(int ioNum)
        {
            //return new ActionIODigital(ioNum, true);
            DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("Deprecated component: use WriteDigital(ioNum, true) instead");
            return null;
        }

        

    }
}
