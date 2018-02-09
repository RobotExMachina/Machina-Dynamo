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

namespace MachinaDynamo
{
    [NodeName("Speed with Slider (NodeModel) 25")]
    [NodeDescription("Example Speed Nodemodel with custom UI")]
    [NodeCategory("MachinaDynamo.Tests")]
    [InPortNames("Speed")]
    [InPortTypes("double")]  // is this not necessary anymore?
    [InPortDescriptions("The speed to set this to")]
    [OutPortNames("Action")]
    [OutPortTypes("Machina.Action")]
    [OutPortDescriptions("The Machina Action")]
    [IsDesignScriptCompatible]
    [IsVisibleInDynamoLibrary(false)]
    public class SpeedNodeModelSlider : CoreNodeModels.DSDropDownBase
    {
        private int _sliderVal;

        public int SliderValue
        {
            get { return _sliderVal; }
            set
            {
                _sliderVal = value;
                RaisePropertyChanged("SliderValue");
                OnNodeModified(false);
            }
        }

        protected override void SerializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.SerializeCore(nodeElement, context);
        }

        public SpeedNodeModelSlider()
        { 
            RegisterAllPorts();
        }

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            var sNode = AstFactory.BuildIntNode(SliderValue);

            var fCall = AstFactory.BuildFunctionCall(
                new Func<int, Machina.ActionSpeed>(Machina.Action.Speed),
                new List<AssociativeNode> { sNode });

            return new[] { AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), fCall) };
        }


        protected override SelectionState PopulateItemsCore(string currentSelection)
        {
            throw new NotImplementedException();
        }
    }





    public class SpeedNodeModelSliderNodeView : INodeViewCustomization<SpeedNodeModelSlider>
    {
        private System.Windows.Controls.Image icon; 

        public void CustomizeView(SpeedNodeModelSlider model, NodeView nodeView)
        {
            //throw new NotImplementedException();
            var slider = new Slider();
            nodeView.inputGrid.Children.Add(slider);
            slider.DataContext = model;


            //// https://stackoverflow.com/a/28083353/1934487
            //icon = new System.Windows.Controls.Image();
            //BitmapImage bm = new BitmapImage();
            //bm.BeginInit();
            //bm.UriSource = new Uri("MachinaDynamo.Actions.Speed.Small.png", UriKind.RelativeOrAbsolute);
            //bm.EndInit();
            //icon.Stretch = Stretch.Fill;
            //icon.Source = bm;

            //nodeView.inputGrid.Children.Add(icon);
            
        }

        public void Dispose() { }

        //static private ImageSource getImageFromResource(string assemblyName, string resourceName)
        //{
        //    Uri oUri = new Uri("pack://application:,,,/" + assemblyName + ";component/" + resourceName, UriKind.RelativeOrAbsolute);
        //    return BitmapFrame.Create(oUri);
        //}
    }

}
