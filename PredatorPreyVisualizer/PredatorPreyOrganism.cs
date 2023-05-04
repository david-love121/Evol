using Arena;
using DongUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PredatorPreyVisualizer
{
    public abstract class PredatorPreyOrganism : MovingObject
    {
        public Vector2D Velocity { get; set; } = Vector2D.NullVector();

        public double Width { get; }
        public double Height { get; }

        public double MaxSpeed { get; }
        public double MaxAcceleration { get; }
        public double StepTime { get; }

        public PredatorPreyOrganism(int graphicsCode, double width, double height, double maxSpeed, double maxAccel, double stepTime, Vector2D position) :
            base(graphicsCode, 1, width, height)
        {
            Width = width;
            Height = height;
            MaxSpeed = maxSpeed;
            MaxAcceleration = maxAccel;
            StepTime = stepTime;
            Position = position;
        }

        private double nextDecisionTime = 0;

        virtual protected void Eat()
        { }

        private void ChooseMove()
        {
            var deltaV = ChooseVelocityChange();
            // Make sure it's not too big
            if (deltaV.Magnitude > MaxAcceleration)
                deltaV = deltaV.UnitVector() * MaxAcceleration;

            Velocity += deltaV;

            if (Velocity.Magnitude > MaxSpeed)
                Velocity = Velocity.UnitVector() * MaxSpeed;
        }

        abstract protected Vector2D ChooseVelocityChange();

        private double formerTime = 0;

        protected override bool DoTurn(Turn turn)
        {
            return turn.DoTurn();
        }

        protected override void UserDefinedBeginningOfTurn()
        {

        }

        protected override Turn UserDefinedChooseAction()
        {
            double deltaT = Arena.Time - formerTime;

            if (nextDecisionTime <= Arena.Time)
            {
                nextDecisionTime += StepTime;
                ChooseMove();
            }

            var newPosition = Position + Velocity * deltaT;

            // Check to make sure we didn't go out of bounds
            if (newPosition.X < Width / 2)
            {
                newPosition.X = Width / 2;
                Velocity = new Vector2D(0, Velocity.Y);
            }
            else if (newPosition.X > Arena.Width - Width / 2)
            {
                newPosition.X = Arena.Width - Width / 2;
                Velocity = new Vector2D(0, Velocity.Y);
            }
            if (newPosition.Y < Height / 2)
            {
                newPosition.Y = Height / 2;
                Velocity = new Vector2D(Velocity.X, 0);
            }
            else if (newPosition.Y > Arena.Height - Height / 2)
            {
                newPosition.Y = Arena.Height - Height / 2;
                Velocity = new Vector2D(Velocity.X, 0);
            }

            return new Move(this, newPosition);
        }

        internal bool IsDead { get; set; } = false;

        protected override void UserDefinedEndOfTurn()
        {
            Eat();
            formerTime = Arena.Time;
        }

        public override bool IsPassable(ArenaObject mover = null)
        {
            return true;
        }
    }
}
