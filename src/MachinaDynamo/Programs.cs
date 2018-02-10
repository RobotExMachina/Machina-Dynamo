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


        /// <summary>
        /// Compiles a list of Actions into the device's native language. This is the code you would typically need to load into the device's controller to run the program.
        /// </summary>
        /// <param name="bot">The Robot instance that will export this program</param>
        /// <param name="actions">A program in the form of a list of Actions</param>
        /// <param name="inlineTargets">If true, targets will be declared inline with the instruction. Otherwise, the will be declared and used as independent variables</param>
        /// <param name="machinaComments">If true, Machina-style comments with code information will be added to the end of the code instructions</param>
        /// <returns name="code">Device-specific code</returns>
        public static string CompileProgram(Robot bot, List<MAction> actions, bool inlineTargets = true, bool machinaComments = true)
        {
            bot.ControlMode(ControlType.Offline);

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
