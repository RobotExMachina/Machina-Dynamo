using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dynamo.Graph.Nodes;
using ProtoCore.AST.AssociativeAST;
using Machina;

using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Interfaces;
using Autodesk.DesignScript.Geometry;

namespace MachinaDynamo
{
    [NodeName("Speed (NodeModel)")]
    [NodeDescription("Example Speed Nodemodel")]
    [NodeCategory("MachinaDynamo.Tests")]
    [InPortNames("Speed")]
    [InPortTypes("double")]  // is this not necessary anymore?
    [InPortDescriptions("The speed to set this to")]
    [OutPortNames("Action")]
    [OutPortTypes("Machina.Action")]
    [OutPortDescriptions("The Machina Action")]
    [IsDesignScriptCompatible]
    [IsVisibleInDynamoLibrary(false)]
    class SpeedNodeModel : NodeModel
    {
        public SpeedNodeModel()
        {
            RegisterAllPorts();
        }

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            if (!HasConnectedInput(0))
            {
                return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), AstFactory.BuildNullNode()) };
            }

            var fCall = AstFactory.BuildFunctionCall(
                new Func<int, Machina.ActionSpeed>(Machina.Action.Speed),
                new List<AssociativeNode> { inputAstNodes[0] });

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), fCall) };
        }
    }

    
}
