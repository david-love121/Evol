using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;

namespace VisualizerControl.Shapes
{
    /// <summary>
    /// Generalized "cube-like" shape made of six quadrilaterals
    /// </summary>
    public class GeneralizedCube3D : TriangleBasedShape3D
    {
        /// <summary>
        /// Sides, with right-hand winding, are as follows:
        /// 1234
        /// 1485
        /// 1562
        /// 5876
        /// 2673
        /// 3784
        /// </summary>
        public GeneralizedCube3D(Vector3D p1, Vector3D p2, Vector3D p3, Vector3D p4, Vector3D p5, Vector3D p6, Vector3D p7, Vector3D p8) :
            base(new List<Triangle3D> {
                new Triangle3D(p1, p2, p3), new Triangle3D(p1, p3, p4),
                new Triangle3D(p1, p4, p8), new Triangle3D(p1, p8, p5),
                new Triangle3D(p1, p5, p6), new Triangle3D(p1, p6, p2),
                new Triangle3D(p5, p8, p7), new Triangle3D(p5, p7, p6),
                new Triangle3D(p2, p6, p7), new Triangle3D(p2, p7, p3),
                new Triangle3D(p3, p7, p8), new Triangle3D(p3, p8, p4),
            })
        {}

        public GeneralizedCube3D(List<Vector3D> vectors) :
            this(vectors[0], vectors[1], vectors[2], vectors[3], vectors[4], vectors[5], vectors[6], vectors[7])
        { 
            if (vectors.Count != 8)
            {
                throw new ArgumentException("Wrong number of vectors passed to GeneralizedCube 3D: " + vectors.Count + " instead of 8!");
            }
        }
        
    }
}
