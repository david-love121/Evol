using Arena;
using Arena.GraphicTurns;
using DongUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredatorPreyVisualizer
{
    class Move : Turn
    {
        private readonly Vector2D newPosition;

        public Move(PredatorPreyOrganism org, Vector2D newPosition) :
            base(org)
        {
            this.newPosition = newPosition;
        }

        public override bool DoTurn()
        {
            if (Owner.Arena.TestPoint(newPosition))
            {
                //Owner.Position = newPosition;
                Owner.Arena.MoveObject(Owner, newPosition);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
