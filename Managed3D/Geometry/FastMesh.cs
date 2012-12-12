using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents an immutable 3D mesh that can be streamed directly to graphics hardware.
    /// </summary>
    public sealed class FastMesh : IRenderable
    {
        #region Fields
        private FastVertex[] vertices;
        private FastFace[] faces;
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

        }
        #endregion

        public IEnumerable<IRenderableVertex> Vertices
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<IRenderableFace> Faces
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public IEnumerable<IRenderableEdge> Edges
        {
            get
            {
                throw new NotImplementedException();
            }
        }

    }

    public struct FastVertex : IRenderableVertex
    {

        public double X
        {
            get;
            internal set;
        }

        public double Y
        {
            get;
            internal set;
        }

        public double Z
        {
            get;
            internal set;
        }
    }

    public struct FastFace : IRenderableFace
    {
        public uint A
        {
            get;
            internal set;
        }

        public uint B
        {
            get;
            internal set;
        }

        public uint C
        {
            get;
            internal set;
        }
    }
}
