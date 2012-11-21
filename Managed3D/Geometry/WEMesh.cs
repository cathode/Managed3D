using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents a 3D Winged-Edge Polygon Mesh.
    /// </summary>
    public class WEMesh
    {
        #region Fields
        private Edge3[] edges;
        private readonly Dictionary<uint, Vector3> vertices;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WEMesh"/> class.
        /// </summary>
        public WEMesh()
        {
            this.vertices = new Dictionary<uint, Vector3>();
        }
        #endregion
        #region Properties

        #endregion
        #region Methods
        /// <summary>
        /// Deletes the vertex with the specified index. This causes cascading
        /// changes that can result in the removal of edges associated with the
        /// removed vertex and consequently faces associated with those edges.
        /// </summary>
        /// <param name="vertexIndex"></param>
        public void DeleteVertex(uint vertexIndex)
        {

        }

        /// <summary>
        /// Deletes the edge with the specified index. This causes cascading
        /// changes that can result in the removal of faces associated with
        /// the removed edge.
        /// </summary>
        /// <param name="edgeIndex"></param>
        public void DeleteEdge(uint edgeIndex)
        {

        }

        /// <summary>
        /// Deletes the face with the specified index.
        /// </summary>
        /// <param name="faceIndex"></param>
        public void DeleteFace(uint faceIndex)
        {

        }

        /// <summary>
        /// Adds a vertex to the specified 
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public uint AddVertex(Vector3 vertex)
        {
            return 0;
        }

        /// <summary>
        /// Creates an edge between the vertices with the specified indices.
        /// </summary>
        /// <param name="vertexA"></param>
        /// <param name="vertexB"></param>
        /// <returns></returns>
        public uint CreateEdge(uint vertexA, uint vertexB)
        {
            return 0;
        }

        public uint WeldVertices(uint vertexA, uint vertexB)
        {
            return 0;
        }
        #endregion
    }
}
