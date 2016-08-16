using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Interfaces;
using Autodesk.DesignScript.Geometry;

using BRobot;


namespace BRobot_for_Dynamo
{
    public class BRobot_for_Dynamo
    {

        internal BRobot_for_Dynamo() { }

        public static Robot NewRobot()
        {
            Robot bot = new Robot();
            return bot;
        }

        
        /*
            @TODO: 
            - Add components for all actions (some of them will come with predefined values, like SetMotionToLinear)
            - Create a ProgramReader
            - Create a GenerateCode and ExportToFile
            

        */

    }
}
