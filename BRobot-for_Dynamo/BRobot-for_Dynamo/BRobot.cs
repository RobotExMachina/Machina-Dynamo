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

        internal BRobot() { }

        public static Robot NewRobot()
        {
            return new Robot();
        }

        /*
            @TODO: 
            - Add components for all actions (some of them will come with predefined values, like SetMotionToLinear)
            - Create a ProgramReader
            - Create a GenerateCode and ExportToFile
        */




        /// <summary>
        /// Create a relative movement action.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns name = "action"></returns>
        public static BAction Move(Vector direction)
        {
            return new ActionTranslation(new BPoint(direction.X, direction.Y, direction.Z), true);
        }

        public static BAction MoveTo(Autodesk.DesignScript.Geometry.Point location)
        {
            return new ActionTranslation(new BPoint(location.X, location.Y, location.Z), false);
        }

        public static BAction SpeedTo(double speed)
        {
            return new ActionSpeed((int)Math.Round(speed), false);
        }



        public static List<string> ReadProgram(List<BAction> actions)
        {
            List<string> program = new List<string>();

            foreach(BAction a in actions)
            {
                program.Add(a.ToString());
            }

            return program;
        }

        public static List<string> GenerateCode(Robot bot, List<BAction> actions) 
        {
            bot.Mode("offline");
            
            foreach(BAction a in actions)
            {
                bot.Do(a);
            }

            return bot.Export();
        }

        public static string ExportToFile(List<string> code, string filepath)
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
