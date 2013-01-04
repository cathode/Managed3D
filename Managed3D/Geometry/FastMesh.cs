using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents an immutable, low-overhead 3D mesh that can be streamed directly to graphics hardware.
    /// </summary>
    public class FastMesh
    {
        #region Fields
        private FastVertex[] vertices;
        private int[] indices;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FastMesh"/> class.
        /// </summary>
        /// <param name="vertexCount"></param>
        /// <param name="faceCount"></param>
        /// <param name="edgeCount"></param>
        internal FastMesh(int vertexCount, int faceCount)
        {
            this.vertices = new FastVertex[vertexCount];
            this.indices = new int[faceCount * 3];
        }
        #endregion
        #region Properties

        #endregion
        #region Methods
        #endregion
    }

    public struct FastVertex
    {
        #region Fields
        private Vector3 position;
        private Vector3 normal;
        private Vector2 texCoords;
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
            this.position = new Vector3(x, y, z);
            this.normal = Vector3.Zero;
            this.texCoords = Vector2.Zero;
        }

        #endregion
        #region Properties
        public Vector3 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
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
