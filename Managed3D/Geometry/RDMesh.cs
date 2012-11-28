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
        private Dictionary<uint, RDVertex> vertices;
        private Dictionary<uint, RDEdge> edges;
        private Dictionary<uint, RDFace> faces;

        private Dictionary<ushort, RDSparseAttributes> attributes;
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RDMesh"/> class.
        /// </summary>
        public RDMesh()
        {

        }

        #endregion
        #region Methods
        public uint CreateVertex(double x, double y, double z)
        {
            var vertex = new Vertex3(x, y, z);

            throw new NotImplementedException();
            //this.vertices.add
        }

        public void DeleteFace(uint faceIndex)
        {

        }

        public void DeleteFace(Polygon3 face)
        {

        }

        public void DeleteEdge(uint edgeIndex)
        {
            var edge = this.edges[edgeIndex];


        }

        public void DeleteEdge(Edge3 edge)
        {

        }

        public void DeleteVertex(uint vertexIndex)
        {

        }

        public void WeldVertex(uint subjectIndex, uint targetIndex)
        {
            var subject = this.vertices[subjectIndex];
            var target = this.vertices[targetIndex];

            // Find all edges and faces that reference the subject vertex.

            var edges = from e in this.edges.Values
                        where e.A == subject || e.B == subject
                        select e;

            var faces = edges.SelectMany(e => new[] { e.Left, e.Right }).Where(f => f != null).Distinct();

            // Update them so they point at the target vertex

            foreach (var e in edges)

                if (e.A == subject)
                    e.A = target;
                else if (e.B == subject)
                    e.B = target;

            //foreach (var f in faces)
            //    for (int i = 0; i < f.Vertices.Length; ++i)
            //        if (f[i] == subject)
            //            f[i] = target;

            // Delete the subject vertex
            this.DeleteVertex(subjectIndex);
        }
        #endregion
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
        internal List<RDEdge> edges;
        internal List<RDFace> faces;
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
        public int V1, V2, V3;
        public int E1, E2, E3;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RDFace"/> class.
        /// </summary>
        public RDFace()
        {
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the faces which share edges with the current face.
        /// </summary>
        public IEnumerable<RDFace> Neighbors
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion
        #region Methods
        public void InsertVertex(RDVertex vertex)
        {
            // Save edges of the current face.
            //var edges = this.edges.ToArray();

            throw new NotImplementedException();
        }

        #endregion
    }

    /// <summary>
    /// Represents a render-dynamic edge.
    /// </summary>
    public class RDEdge
    {
        #region Fields
        private RDVertex a;
        private RDVertex b;
        private RDFace left;
        private RDFace right;
        private readonly List<RDEdge> neighbors;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RDEdge"/> class.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public RDEdge(RDVertex a, RDVertex b)
        {
            this.A = a;
            this.B = b;
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
                return this.a;
            }
            set
            {
                this.a = value;
            }
        }

        /// <summary>
        /// Gets or sets the second vertex of the edge.
        /// </summary>
        public RDVertex B
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

        /// <summary>
        /// Gets or sets the left-hand face.
        /// </summary>
        public RDFace Left
        {
            get
            {
                return this.left;
            }
            set
            {
                this.left = value;
            }
        }

        public RDFace Right
        {
            get
            {
                return this.right;
            }
            set
            {
                this.right = value;
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
