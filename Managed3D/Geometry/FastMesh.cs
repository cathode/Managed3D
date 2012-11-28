using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents an immutable 3D mesh that can be streamed directly to graphics hardware.
    /// </summary>
    public sealed class FastMesh
    {
        #region Fields
        private float[] vertices;
        private uint[] faces;
        private uint[] edges;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FastMesh"/> class.
        /// </summary>
        /// <param name="vertexCount"></param>
        /// <param name="faceCount"></param>
        /// <param name="edgeCount"></param>
        internal FastMesh(uint vertexCount, uint faceCount, uint edgeCount)
        {
            this.vertices = new float[vertexCount];
            this.faces = new uint[faceCount];
            this.edges = new uint[edgeCount];

        }
        #endregion
    }
}
