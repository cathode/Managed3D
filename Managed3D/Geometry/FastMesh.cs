using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents an immutable 3D mesh that can be streamed directly to graphics hardware.
    /// </summary>
    public class FastMesh : IRenderable
    {
        #region Fields
        private FastVertex[] vertices;
        private FastFace[] faces;
        private FastEdge[] edges;
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
            this.vertices = new FastVertex[vertexCount];
            this.faces = new FastFace[faceCount];
            this.edges = new FastEdge[edgeCount];
        }
        #endregion
        #region Properties
        public IEnumerable<IRenderableVertex> Vertices
        {
            get
            {
                for (int i = 0; i < this.vertices.Length; ++i)
                    yield return this.vertices[i];
            }
        }

        public IEnumerable<IRenderableFace> Faces
        {
            get
            {
                for (int i = 0; i < this.faces.Length; ++i)
                    yield return this.faces[i];
            }
        }

        public IEnumerable<IRenderableEdge> Edges
        {
            get
            {
                for (int i = 0; i < this.edges.Length; ++i)
                    yield return this.edges[i];
            }
        }
        #endregion
        #region Methods
        internal void SetGeometry(FastVertex[] vertices, FastFace[] faces, FastEdge[] edges)
        {
            this.vertices = vertices;
            this.faces = faces;
            this.edges = edges;
        }
        #endregion
    }

    public struct FastVertex : IRenderableVertex
    {
        #region Fields
        private double x;
        private double y;
        private double z;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FastVertex"/> struct.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public FastVertex(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        #endregion
        #region Properties
        public double X
        {
            get
            {
                return this.x;
            }
            internal set
            {
                this.x = value;
            }
        }

        public double Y
        {
            get
            {
                return this.y;
            }
            internal set
            {
                this.y = value;
            }
        }

        public double Z
        {
            get
            {
                return this.z;
            }
            set
            {
                this.z = value;
            }
        }
        #endregion
    }

    public struct FastFace : IRenderableFace
    {
        #region Fields
        private uint a;
        private uint b;
        private uint c;
        #endregion
        #region Constructors
        public FastFace(uint p1, uint p2, uint p3)
        {
            this.a = p1;
            this.b = p2;
            this.c = p3;
        }
        #endregion
        #region Properties
        public uint A
        {
            get
            {
                return this.a;
            }
            set
            {
                this.a = value;
            }
        }

        public uint B
        {
            get
            {
                return this.b;
            }
            set
            {
                this.b = value;
            }
        }

        public uint C
        {
            get
            {
                return this.c;
            }
            set
            {
                this.c = value;
            }
        }
        #endregion
    }

    public struct FastEdge : IRenderableEdge
    {
        #region Fields
        private uint p;
        private uint q;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FastEdge"/> class.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        public FastEdge(uint p, uint q)
        {
            this.p = p;
            this.q = q;
        }
        #endregion
        #region Properties
        public uint P
        {
            get
            {
                return this.p;
            }
            internal set
            {
                this.p = value;
            }
        }

        public uint Q
        {
            get
            {
                return this.q;
            }
            set
            {
                this.q = value;
            }
        }
        #endregion
    }
}
