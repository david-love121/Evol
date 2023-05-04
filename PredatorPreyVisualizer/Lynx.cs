using DongUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PredatorPreyVisualizer
{
    class Lynx : PredatorPreyOrganism
    {
        internal const double MyWidth = .3;
        internal const double MyLength = 1;
        private const double myMaxSpeed = 12;
        private const double myStepTime = .3;
        private const double myMaxAccel = 8;
        private const double initialx = 25;
        private const double initialy = 40;
        private const int graphicsCode = 2;

        public Hare Other { get; set; }

        public override string Name => "Lynx";

        public Lynx() :
            base(graphicsCode, MyWidth, MyLength, myMaxSpeed, myMaxAccel, myStepTime, new Vector2D(initialx, initialy))
        { }

        protected override Vector2D ChooseVelocityChange()
        {
            Vector2D diff = Other.Position - Position;

            return diff.UnitVector() * myMaxAccel;
        }

        protected override void Eat()
        {
            double xdiff = Math.Abs(Other.Position.X - Position.X);
            double ydiff = Math.Abs(Other.Position.Y - Position.Y);
            if (xdiff <= MyWidth / 2 && ydiff <= MyLength / 2)
            {
                Other.IsDead = true;
            }
        }
    }
}
