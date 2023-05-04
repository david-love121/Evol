using Arena;
using DongUtility;
using NeuralNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredatorPreyVisualizer
{
    class PredatorPreyEngine : ArenaEngine
    {
        private Perceptron perceptron;

        public List<Hare> Hares { get; private set; } = new();
        public List<Lynx> Lynxes { get; private set; } = new();

        public PredatorPreyEngine(double xSize, double ySize, Perceptron perceptron, int nHares = 1, int nLynxes = 1) :
            base(xSize, ySize, "dirt.jpg")
        {
            if (nHares <= 0 || nLynxes <= 0)
            {
                throw new ArgumentException("Invalid number of hares or lynxes");
            }

            for (int i = 0; i < nHares; ++i)
            {
                var hare = new Hare();
                hare.Perceptron = perceptron;
                Hares.Add(hare);
            }
            for (int i = 0; i < nLynxes; ++i)
            {
                var lynx = new Lynx();
                Lynxes.Add(lynx);
            }

            // Specifically for one hare, one lynx
            Hares[0].Other = Lynxes[0];
            Lynxes[0].Other = Hares[0];

            AddObject(Hares[0], new Vector2D(Width / 2, 2 * Height / 3));
            AddObject(Lynxes[0], new Vector2D(Width / 2, Height / 3));

        }

        public double SizeScale { get; set; } = 6;

        public override void Initialize()
        {
            Registry.Initialize(@"PredatorPreyVisualizer\", @"Images\");

            Registry.AddEntry(new GraphicInfo("hare.jpg", Hare.MyWidth * SizeScale, Hare.MyLength * SizeScale));
            Registry.AddEntry(new GraphicInfo("lynx.jpg", Lynx.MyWidth * SizeScale, Lynx.MyWidth * SizeScale));
        }

        protected override bool Done()
        {
            return Hares.All((x) => x.IsDead);
        }
    }
}
