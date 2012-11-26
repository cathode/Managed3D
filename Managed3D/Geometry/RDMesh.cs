/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Provides a 3D Render-Dynamic Mesh implementation.
    /// </summary>
    public class RDMesh
    {
        private Dictionary<ushort, RDVertex> vertices;
        private Dictionary<ushort, RDEdge> edges;
        private Dictionary<ushort, RDFace> faces;

        private Dictionary<ushort, RDSparseAttributes> attributes;
    }

    public class RDSparseAttributes
    {
        #region Properties
        public bool IsHidden
        {
            get;
            set;
        }

        public Vector4f Color
        {
            get;
            set;
        }

        public int SmoothingGroup
        {
            get;
            set;
        }
        #endregion
    }

    public class RDVertex
    {
        #region Fields
        private List<RDEdge> edges;
        private List<RDFace> faces;
        #endregion
        #region Properties
        public IEnumerable<RDEdge> ConnectedEdges
        {
            get
            {
                return this.edges;
            }
        }

        public IEnumerable<RDFace> ConnectedFaces
        {
            get
            {
                return this.faces;
            }
        }
        #endregion
    }

    /// <summary>
    /// Represents a face of a render-dynamic mesh.
    /// </summary>
    public class RDFace
    {
        #region Fields
        private readonly List<RDVertex> vertices;
        private readonly List<RDEdge> edges;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RDFace"/> class.
        /// </summary>
        public RDFace()
        {
            this.vertices = new List<RDVertex>();
            this.edges = new List<RDEdge>();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the vertices that make up the corners of the face.
        /// </summary>
        public IEnumerable<RDVertex> Vertices
        {
            get
            {
                return this.vertices;
            }
        }

        /// <summary>
        /// Gets the edges that make up the boundary of the face.
        /// </summary>
        public IEnumerable<RDEdge> Edges
        {
            get
            {
                return this.edges;
            }
        }

        /// <summary>
        /// Gets the faces which share edges with the current face.
        /// </summary>
        public IEnumerable<RDFace> Neighbors
        {
            get
            {
                foreach (var edge in this.edges)
                    if (edge.Left != this && edge.Left != null)
                        yield return edge.Left;
                    else if (edge.Right != this && edge.Right != null)
                        yield return edge.Right;
            }
        }
        #endregion
    }

    /// <summary>
    /// Represents a render-dynamic edge.
    /// </summary>
    public class RDEdge
    {
        #region Fields
        private RDVertex u;
        private RDVertex v;
        private RDFace a;
        private RDFace b;
        private readonly List<RDEdge> neighbors;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RDEdge"/> class.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        public RDEdge(RDVertex u, RDVertex v)
        {
            this.A = u;
            this.B = v;
            this.neighbors = new List<RDEdge>();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the first vertex of the edge.
        /// </summary>
        public RDVertex A
        {
            get
            {
                return this.u;
            }
            set
            {
                this.u = value;
            }
        }

        /// <summary>
        /// Gets or sets the second vertex of the edge.
        /// </summary>
        public RDVertex B
        {
            get
            {
                return this.v;
            }
            set
            {
                this.v = value;
            }
        }

        /// <summary>
        /// Gets or sets the left-hand face.
        /// </summary>
        public RDFace Left
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

        public RDFace Right
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

        public IEnumerable<RDEdge> Neighbors
        {
            get
            {
                return this.neighbors;
            }
        }
        #endregion

    }
}
