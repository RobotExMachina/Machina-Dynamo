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
    //  ██╗      ██████╗  ██████╗  ██████╗ ███████╗██████╗ 
    //  ██║     ██╔═══██╗██╔════╝ ██╔════╝ ██╔════╝██╔══██╗
    //  ██║     ██║   ██║██║  ███╗██║  ███╗█████╗  ██████╔╝
    //  ██║     ██║   ██║██║   ██║██║   ██║██╔══╝  ██╔══██╗
    //  ███████╗╚██████╔╝╚██████╔╝╚██████╔╝███████╗██║  ██║
    //  ╚══════╝ ╚═════╝  ╚═════╝  ╚═════╝ ╚══════╝╚═╝  ╚═╝
    //                                                     
    /// <summary>
    /// Robot creation and manipulation tools.
    /// </summary>
    public partial class Robots
    {

        private static List<string> _messages = new List<string>();
        private static int _level = 3;
        private static int _maxCount = 10;
        private static bool isEventHandlerRegistered = false;

        

        [CanUpdatePeriodically(true)]
        public static List<string> Logger(int level = 3, int maxMessages = 10)
        {
            // @TODO: there has to be a better way of doing this... 
            if (!isEventHandlerRegistered)
            {
                Machina.Logger.WriteLine += Logger_WriteLine;
                isEventHandlerRegistered = true;
            }

            if (level != _level)
            {
                if (level < (int)LogLevel.NONE || level > (int)LogLevel.DEBUG)
                {
                    DynamoServices.LogWarningMessageEvents.OnLogWarningMessage("Log level out of bounds, please choose one of: 0 None, 1 Error, 2 Warning, 3 Info (default), 4 Verbose or 5 Debug");
                    return _messages;
                }

                _level = level;
                Machina.Logger.SetLogLevel(_level);
            }

            return _messages;
        }


        private static void Logger_WriteLine(string msg)
        {
            _messages.Add(msg);

            int diff = _messages.Count - _maxCount;
            if (diff > 0)
            {
                for (int i = 0; i < diff; i++)
                {
                    _messages.RemoveAt(0);
                }
            }
        }

    }
}
