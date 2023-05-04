using DongUtility;
using NeuralNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PredatorPreyVisualizer
{
    class Hare : PredatorPreyOrganism
    {
        internal const double MyWidth = .2;
        internal const double MyLength = .2;
        private const double myMaxSpeed = 6;
        private const double myStepTime = .15;
        private const double myMaxAccel = 2.5;
        private const double initialx = 25;
        private const double initialy = 10;

        private const int graphicsCode = 1;

        public Lynx Other { get; set; }

        public Hare() :
            base(graphicsCode, MyWidth, MyLength, myMaxSpeed, myMaxAccel, myStepTime, new Vector2D(initialx, initialy))
        {
        }

        public Perceptron Perceptron { get; set;  } = new SingleLayerPerceptron(4, 2);

        public override string Name => "Hare";

        protected override Vector2D ChooseVelocityChange()
        {
            Perceptron.Reset();

            Perceptron.AddInputs(
                Position.X,
                Position.Y,
                Other.Position.X,
                Other.Position.Y
                );

            Perceptron.Run();

            double x = Perceptron.GetOutput(0);
            double y = Perceptron.GetOutput(1);

            if (!UtilityFunctions.IsValid(x) || !UtilityFunctions.IsValid(y))
                return new Vector2D(0, 0);

            return new Vector2D(x, y);
        }
    }
}
