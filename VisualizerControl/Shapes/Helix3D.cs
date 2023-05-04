using DongUtility;
using Geometry.Geometry2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using VisualizerControl.Shapes;

namespace VisualizerControl.Shapes
{
    /// <summary>
    /// A helix shape
    /// </summary>
    public class Helix3D : FunctionShape3D
    {

        public Helix3D(double radius, Point center, double zVelocity, double nTurns = 5) :
            base(new HelixPath(radius, center, zVelocity, 0, 2 * Math.PI * nTurns), GetName(radius, center, zVelocity, nTurns))
        { }

        static private string GetName(double radius, Point center, double zVelocity, double nTurns)
        {
            return "Helix" + radius.ToString() + center.X.ToString() + center.Y.ToString() + zVelocity.ToString() + nTurns.ToString();
        }

        /// <summary>
        /// Creates a helix with the given parameters
        /// </summary>
        /// <param name="azimuthal">The azimuthal angle of the tangent line at the origin</param>
        /// <param name="polar">The polar angle of the tangent line at the origin</param>
        /// <param name="radius">The radius of the helix</param>
        /// <param name="nTurns">The number of complete turns to render</param>
        static public Helix3D MakeHelix(double azimuthal, double polar, double radius, double nTurns = 5)
        {
            int sign = Math.Sign(radius);
            radius = Math.Abs(radius);
            double circlePhi = azimuthal + sign * Math.PI / 2;
            double centerx = radius * Math.Cos(circlePhi);
            double centery = radius * Math.Sin(circlePhi);
            double vz = radius / Math.Tan(polar);

            return new Helix3D(radius, new Point(centerx, centery), vz, nTurns);
        }

        /// <summary>
        /// Finds the value of the parameter when the helix is at the origin
        /// </summary>
        /// <param name="centerx">The x coordinate of the center of the circle</param>
        /// <param name="centery">The y coordinate of the center of the circle</param>
        static double GetBeginning(double centerx, double centery)
        {
            return Math.Atan2(centery, centerx);
        }
    }
}
