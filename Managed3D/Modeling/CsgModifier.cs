using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Managed3D.Geometry;

namespace Managed3D.Modeling
{
    public class CsgModifier : ModifierNode
    {
        #region Fields

        #endregion
        #region Constructors

        #endregion
        #region Properties
        public CsgOperation Operation
        {
            get;
            set;
        }
        #endregion
        #region Methods
        public static Mesh3 GenerateSubtraction(Mesh3 mesh, BoundingVolume sub)
        {
            // For each of the six planes defined by the bounding volume's sides, we need to slice the mesh on those planes.

            // Find all edges that pass through the plane.

            var isec = from e in mesh.SelectMany(poly => poly.Edges).Distinct()
                       where e.A.X > sub.X
                       select e;


            var result = new Mesh3();
            return result;
        }
        #endregion
    }
}
