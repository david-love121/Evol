using System;

namespace DongUtility
{
    /// <summary>
    /// A simple two-dimensional integer coordinate
    /// </summary>
    public struct Coordinate2D : IEquatable<Coordinate2D>
    {
        public Coordinate2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

       

        public override bool Equals(object? obj)
        {
            return obj is Coordinate2D coord && Equals(coord);
        }

        public bool Equals(Coordinate2D other)
        {
            return X == other.X &&
                   Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        static public bool operator ==(Coordinate2D lhs, Coordinate2D rhs)
        {
            return lhs.Equals(rhs);
        }

        static public bool operator !=(Coordinate2D lhs, Coordinate2D rhs)
        {
            return !lhs.Equals(rhs);
        }

        public override string ToString()
        {
            return "[ " + X + ", " + Y + " ]";
        }
    }
}

