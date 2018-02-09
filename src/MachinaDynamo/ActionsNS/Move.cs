using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dynamo.Controls;
using Dynamo.Wpf;
using Dynamo.Graph.Nodes;
using ProtoCore.AST.AssociativeAST;
using Machina;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CoreNodeModels;
using Dynamo.Graph;
using System.Xml;


using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Interfaces;
using Autodesk.DesignScript.Geometry;


namespace MachinaDynamo.ActionsNS
{
    [NodeName("Move (NodeModel)")]
    [NodeDescription("Example Move Nodemodel")]
    [NodeCategory("Machina.Actions")]
    //[InPortNames("Move", "rel")]
    //[InPortTypes("Point", "bool")]
    //[InPortDescriptions("Target Location", "Is this a relative action")]    
    [OutPortNames("Action")]
    [OutPortTypes("Machina.Action")]
    [OutPortDescriptions("Move Action")]
    [IsDesignScriptCompatible]
    public class Move : NodeModel
    {
        public Move()
        {
            InPortData.Add(new PortData("Move", "Target location"));
            InPortData.Add(new PortData("rel", "Is this a relative Action?", AstFactory.BuildBooleanNode(false)));

            OutPortData.Add(new PortData("Action", "Move Action"));

            RegisterAllPorts();
        }

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            //return base.BuildOutputAst(inputAstNodes);
            if (!HasConnectedInput(0))
            {
                return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), AstFactory.BuildNullNode()) };
            }

            var functionCall = AstFactory.BuildFunctionCall(
                new Func<Machina.Vector, Machina.ActionTranslation>(Machina.Action.Move),  // This won't work...
                new List<AssociativeNode> { inputAstNodes[0] });

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall) };
            
        }

    } 
}
