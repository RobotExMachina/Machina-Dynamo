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

        internal static string EnumerateList(string[] strings, string closing)
        {

            switch (strings.Length)
            {
                case 0:
                    return "";

                case 1:
                    return strings[0];

                default:
                    string str = "";
                    for (int i = 0; i < strings.Length; i++)
                    {
                        str += "\"" + strings[i] + "\"";
                        if (i < strings.Length - 2)
                        {
                            str += ", ";
                        }
                        else if (i == strings.Length - 2)
                        {
                            str += " " + closing + " ";
                        }
                    }
                    return str;
            }
        }

    }
}
