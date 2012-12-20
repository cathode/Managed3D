using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Geometry
{
    public class MeshData
    {
        #region Constructors
        public MeshData(Polygon3[] polygons)
        {

        }
        #endregion
        #region Properties
        public List<Vector3> Positions
        {
            get;
            set;
        }

        public List<Vector3> Normals
        {
            get;
            set;
        }

        public List<Vector2> TextureCoordinates
        {
            get;
            set;
        }

        public List<int> Indices
        {
            get;
            set;
        }
        #endregion
    }
}
