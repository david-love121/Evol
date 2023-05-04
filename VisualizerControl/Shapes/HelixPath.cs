using DongUtility;
using Geometry.Geometry2D;
using System;
using System.Collections.Generic;
using System.Text;

namespace VisualizerControl.Shapes
{
    class HelixPath : Path
    {
        public HelixPath(double radius, Point center, double zVelocity, double initialValue, double finalValue)
        {
            Radius = radius;
            Center = center;
            ZVelocity = zVelocity;
            InitialParameter = initialValue;
            FinalParameter = finalValue;
        }

        public override double InitialParameter { get; }

        public override double FinalParameter { get; }

        public double Radius { get; }
        public Point Center { get; }
        public double ZVelocity { get; }

        protected override Vector Function(double parameter)
        {
            double x = Radius * Math.Cos(parameter) + Center.X;
            double y = Radius * Math.Sin(parameter) + Center.Y;
            double z = ZVelocity * parameter;
            return new Vector(x, y, z);
        }
    }
}
