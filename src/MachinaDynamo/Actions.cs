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
    public partial class Actions
    {








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


        ///// <summary>
        ///// Sets the current motion mode to be applied to future actions. This can be "linear" (default) for straight line movements in euclidean space, or "joint" for smooth interpolation between axes angles. NOTE: "joint" motion may produce unexpected trajectories resulting in reorientations or collisions; use with caution!
        ///// </summary>
        ///// <param name="mode">"linear" or "joint"</param>
        ///// <returns name="Action">MotionMode Action</returns>
        //public static MAction MotionMode(string mode = "linear")
        //{
        //    MotionType mt;
        //    try
        //    {
        //        mt = (MotionType)Enum.Parse(typeof(MotionType), mode, true);
        //        if (Enum.IsDefined(typeof(MotionType), mt))
        //        {
        //            return new ActionMotion(mt);
        //        }
        //    }
        //    catch { }

        //    // this is better messagewise, and specially if I want to return something other than null
        //    DynamoServices.LogWarningMessageEvents.OnLogWarningMessage($"{mode} is not a valid option for MotionMode changes, please specify one of the following: {Utils.EnumerateList(Enum.GetNames(typeof(MotionType)), "or")}");
        //    return null;
        //}


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

        

        ///// <summary>
        ///// Increase the TCP angular rotation speed value new Actions will be ran at.
        ///// </summary>
        ///// <param name="rotationSpeedInc">TCP angular rotation speed increment in deg/s. Setting this value to zero or less will reset it back to the robot's default.</param>
        ///// <returns></returns>
        //public static MAction RotationSpeed(double rotationSpeedInc) => new ActionRotationSpeed(rotationSpeedInc, true);

        ///// <summary>
        ///// Set the TCP angular rotation speed value new Actions will be ran at.
        ///// </summary>
        ///// <param name="rotationSpeed">TCP angular rotation speed value in deg/s. Setting this value to zero or less will reset it back to the robot's default.</param>
        ///// <returns></returns>
        //public static MAction RotationSpeedTo(double rotationSpeed) => new ActionRotationSpeed(rotationSpeed, false);

        ///// <summary>
        ///// Increase the maximum joint angular rotation speed value. Movement will be constrained so that the fastest joint rotates below this threshold. 
        ///// </summary>
        ///// <param name="jointSpeedInc">Maximum joint angular rotation speed increment in deg/s. Decreasing the total to zero or less will reset it back to the robot's default.</param>
        ///// <returns></returns>
        //public static MAction JointSpeed(double jointSpeedInc) => new ActionJointSpeed(jointSpeedInc, true);

        ///// <summary>
        ///// Set the maximum joint angular rotation speed value. Movement will be constrained so that the fastest joint rotates below this threshold. 
        ///// </summary>
        ///// <param name="jointSpeed">Maximum joint angular rotation speed value in deg/s. Setting this value to zero or less will reset it back to the robot's default.</param>
        ///// <returns></returns>
        //public static MAction JointSpeedTo(double jointSpeed) => new ActionJointSpeed(jointSpeed, false);

        ///// <summary>
        ///// Increase the maximum joint angular rotation acceleration value. Movement will be constrained so that the fastest joint accelerates below this threshold.
        ///// </summary>
        ///// <param name="jointAccelerationInc">Maximum joint angular rotation acceleration increment in deg/s^2. Decreasing the total to zero or less will reset it back to the robot's default.</param>
        ///// <returns></returns>
        //public static MAction JointAcceleration(double jointAccelerationInc) => new ActionJointAcceleration(jointAccelerationInc, true);

        ///// <summary>
        ///// Set the maximum joint angular rotation acceleration value. Movement will be constrained so that the fastest joint accelerates below this threshold. 
        ///// </summary>
        ///// <param name="jointAcceleration">Maximum joint angular rotation acceleration value in deg/s^2. Setting this value to zero or less will reset it back to the robot's default.</param>
        ///// <returns></returns>
        //public static MAction JointAccelerationTo(double jointAcceleration) => new ActionJointAcceleration(jointAcceleration, false);



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
        /// Send a value to analog output.
        /// </summary>
        /// <param name="analogPin">Analog pin name or number</param>
        /// <param name="value">Value to send to pin</param>
        /// <param name="toolPin">Is this pin on the robot's tool?</param>
        /// <returns name="Action"></returns>
        public static MAction WriteAnalog(int analogPin = 1, double value = 0, bool toolPin = false) => new ActionIOAnalog(analogPin.ToString(), value, toolPin);

        /// <summary>
        /// Activate/deactivate digital output. 
        /// </summary>
        /// <param name="digitalPin">Digital pin name or number</param>
        /// <param name="isOn">Turn on?</param>
        /// <param name="toolPin">Is this pin on the robot's tool?</param>
        /// <returns name="Action"></returns>
        public static MAction WriteDigital(string digitalPin = "1", bool isOn = false, bool toolPin = false) => new ActionIODigital(digitalPin, isOn, toolPin);

        /// <summary>
        /// Activate/deactivate digital output. 
        /// </summary>
        /// <param name="digitalPin">Digital pin name or number</param>
        /// <param name="isOn">Turn on?</param>
        /// <param name="toolPin">Is this pin on the robot's tool?</param>
        /// <returns name="Action"></returns>
        public static MAction WriteDigital(int digitalPin = 1, bool isOn = false, bool toolPin = false) => new ActionIODigital(digitalPin.ToString(), isOn, toolPin);





































        

    }
}
