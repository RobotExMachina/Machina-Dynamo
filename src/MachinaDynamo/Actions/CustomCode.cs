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
    //   ██████╗██╗   ██╗███████╗████████╗ ██████╗ ███╗   ███╗ ██████╗ ██████╗ ██████╗ ███████╗
    //  ██╔════╝██║   ██║██╔════╝╚══██╔══╝██╔═══██╗████╗ ████║██╔════╝██╔═══██╗██╔══██╗██╔════╝
    //  ██║     ██║   ██║███████╗   ██║   ██║   ██║██╔████╔██║██║     ██║   ██║██║  ██║█████╗  
    //  ██║     ██║   ██║╚════██║   ██║   ██║   ██║██║╚██╔╝██║██║     ██║   ██║██║  ██║██╔══╝  
    //  ╚██████╗╚██████╔╝███████║   ██║   ╚██████╔╝██║ ╚═╝ ██║╚██████╗╚██████╔╝██████╔╝███████╗
    //   ╚═════╝ ╚═════╝ ╚══════╝   ╚═╝    ╚═════╝ ╚═╝     ╚═╝ ╚═════╝ ╚═════╝ ╚═════╝ ╚══════╝
    //                                                                    
    partial class Actions
    {
        /// <summary>
        /// Insert a line of custom code directly into a compiled program. This is useful for obscure instructions that are not covered by Machina's API. Note that this Action cannot be checked for validity by Machina, and you are responsible for correct syntax.
        /// </summary>
        /// <param name="statement">Code in the machine's native language.</param>
        /// <param name="isDeclaration">Is this a declaration, like a variable or a workobject? If so, this statement will be placed at the beginning of the program.</param>
        /// <returns name="Action">CustomCode Action</returns>
        public static MAction CustomCode(string statement, bool isDeclaration = false)
            => new ActionCustomCode(statement, isDeclaration);

    }
}
