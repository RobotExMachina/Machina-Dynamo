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
using System.Collections.Generic;
using System;

using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;
using WebSocketSharp.Server;

namespace MachinaDynamo
{

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
        // Internal constructor
        internal Programs() { }


        ///// <summary>
        ///// Compiles a list of Actions into the device's native language. This is the code you would typically need to load into the device's controller to run the program.
        ///// </summary>
        ///// <param name="bot">The Robot instance that will export this program</param>
        ///// <param name="actions">A program in the form of a list of Actions</param>
        ///// <param name="inlineTargets">If true, targets will be declared inline with the instruction. Otherwise, the will be declared and used as independent variables</param>
        ///// <param name="machinaComments">If true, Machina-style comments with code information will be added to the end of the code instructions</param>
        ///// <returns name="code">Device-specific code</returns>
        //public static string CompileProgram(Robot bot, List<MAction> actions, bool inlineTargets = true, bool machinaComments = true)
        //{
        //    bot.ControlMode(ControlType.Offline);

        //    foreach (MAction a in actions)
        //    {
        //        bot.Do(a);
        //    }

        //    List<string> codeLines = bot.Export(inlineTargets, machinaComments);
        //    StringWriter writer = new StringWriter();
        //    for (var i = 0; i < codeLines.Count; i++)
        //    {
        //        writer.WriteLine(codeLines[i]);
        //    }
        //    string code = writer.ToString();
        //    writer.Dispose();  // just in case ;)
        //    return code;
        //}



        /// <summary>
        /// Returns a human-readable representation of a list of Actions.
        /// </summary>
        /// <param name="actions">The list of Actions that conforms a program</param>
        /// <returns name="program">Human-readable representation of the program</returns>
        public static List<string> DisplayProgram(List<MAction> actions)
        {
            List<string> program = new List<string>();

            foreach (MAction a in actions)
            {
                program.Add(a.ToString());
            }

            return program;
        }




        // This will be shared by all instances... but oh well... will implement a better API later
        private static bool _sentOnce = false;
        private static WebSocket _ws;
        private static string _url;


        ///// <summary>
        ///// Send a list of Actions to the Machina Bridge App to be streamed to a robot.
        ///// /// </summary>
        ///// <param name="actions">A program in the form of a list of Actions.</param>
        ///// <param name="url">The URL of the Machina Bridge App.</param>
        ///// <param name="connect">Connect to Machina Bridge App?</param>
        ///// <param name="send">Send Actions?</param>
        ///// <returns></returns>
        //[MultiReturn(new[] { "Connected?", "Sent?", "Instructions" })]
        //public static Dictionary<string, object> SendToBridge(List<MAction> actions, string url = "ws://127.0.0.1:6999/Bridge", bool connect = false,  bool send = false)
        //{
        //    List<string> instructions = new List<string>();

        //    bool connectedResult;
        //    if (connect)
        //    {
        //        if (_ws == null || !_ws.IsAlive)
        //        {
        //            _ws = new WebSocket(url);
        //            _ws.Connect();
        //        }
        //        connectedResult = _ws.IsAlive;

        //        if (!connectedResult)
        //        {
        //            DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("Could not connect to Machina Bridge App");
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        if (_ws != null)
        //        {
        //            _ws.Close();
        //        }
        //        connectedResult = _ws.IsAlive;
        //    }

        //    string sentResult = "Nothing sent";
        //    if (send && connectedResult)
        //    {
        //        string ins = "";

        //        foreach (MAction a in actions)
        //        {
        //            // If attaching a tool, send the tool description first.
        //            // This is quick and dirty, a result of this component not taking the robot object as an input.
        //            // How coud this be improved...? Should tool creation be an action?
        //            if (a.type == Machina.ActionType.Attach)
        //            {
        //                ActionAttach aa = (ActionAttach)a;
        //                ins = aa.tool.ToInstruction();
        //                instructions.Add(ins);
        //                _ws.Send(ins);
        //            }

        //            ins = a.ToInstruction();
        //            instructions.Add(ins);
        //            _ws.Send(ins);
        //        }
        //        sentResult = "Sent!";
        //    }

        //    return new Dictionary<string, object>
        //    {
        //        { "Connected?", connectedResult },
        //        { "Sent?", sentResult },
        //        { "Instructions", instructions }
        //    };
        //}








        //   ██████╗ ██████╗  █████╗ ██╗   ██╗███████╗██╗   ██╗ █████╗ ██████╗ ██████╗ 
        //  ██╔════╝ ██╔══██╗██╔══██╗██║   ██║██╔════╝╚██╗ ██╔╝██╔══██╗██╔══██╗██╔══██╗
        //  ██║  ███╗██████╔╝███████║██║   ██║█████╗   ╚████╔╝ ███████║██████╔╝██║  ██║
        //  ██║   ██║██╔══██╗██╔══██║╚██╗ ██╔╝██╔══╝    ╚██╔╝  ██╔══██║██╔══██╗██║  ██║
        //  ╚██████╔╝██║  ██║██║  ██║ ╚████╔╝ ███████╗   ██║   ██║  ██║██║  ██║██████╔╝
        //   ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝  ╚═══╝  ╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═════╝ 
        //                                                                             

        /// <summary>
        /// Writes a list of strings to a file. Make sure Dynamo has the adequate Admin rights to write files to your system. 
        /// </summary>
        /// <param name="code">A List of Strings</param>
        /// <param name="filepath">The path where the file will be saved</param>
        /// <returns name="resultMsg">Success?</returns>
        [IsVisibleInDynamoLibrary(false)]
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
}
