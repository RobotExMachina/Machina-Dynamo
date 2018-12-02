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
    //   ██████╗ ██████╗ ███╗   ███╗██████╗ ██╗██╗     ███████╗
    //  ██╔════╝██╔═══██╗████╗ ████║██╔══██╗██║██║     ██╔════╝
    //  ██║     ██║   ██║██╔████╔██║██████╔╝██║██║     █████╗  
    //  ██║     ██║   ██║██║╚██╔╝██║██╔═══╝ ██║██║     ██╔══╝  
    //  ╚██████╗╚██████╔╝██║ ╚═╝ ██║██║     ██║███████╗███████╗
    //   ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚═╝     ╚═╝╚══════╝╚══════╝
    //                                                         
    public partial class Programs
    {
        /// <summary>
        /// Compiles a list of Actions into the device's native language. This is the code you would typically need to load into the device's controller to run the program.
        /// </summary>
        /// <param name="bot">The Robot instance that will export this program</param>
        /// <param name="actions">A program in the form of a list of Actions</param>
        /// <param name="inlineTargets">If true, targets will be declared inline with the instruction. Otherwise, the will be declared at the beginning of the programs and used as independent variables</param>
        /// <param name="machinaComments">If true, Machina-style comments with code information will be added to the end of the code instructions</param>
        /// <returns name="code">Device-specific code</returns>
        public static string Compile(Robot bot, List<MAction> actions, bool inlineTargets = true, bool machinaComments = true)
        {
            if (bot == null)
            {
                DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("Invalid Robot instance.");
                return null;
            }

            bot.ControlMode(ControlType.Offline);

            foreach (MAction a in actions)
            {
                bot.Do(a);
            }

            List<string> codeLines = bot.Compile(inlineTargets, machinaComments);
            StringWriter writer = new StringWriter();
            for (var i = 0; i < codeLines.Count; i++)
            {
                writer.WriteLine(codeLines[i]);
            }
            string code = writer.ToString();
            writer.Dispose();  // just in case ;)
            return code;
        }
    }
}
