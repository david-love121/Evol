using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizerControl.Commands
{
    public class UpdateMaterial : VisualizerCommand
    {
        private readonly BasicMaterial material;

        public UpdateMaterial(BasicMaterial materialPrototype)
        {
            material = materialPrototype;
        }
        public override void Do(Visualizer viz)
        {
            // TODO this can be fixed up to do more than color
            viz.ChangeMaterialColor(material);
        }

        protected override void WriteContent(BinaryWriter bw)
        {
            material.WriteContent(bw);
        }

        internal UpdateMaterial(BinaryReader br)
        {
            material = new BasicMaterial(br);
        }
    }
}
