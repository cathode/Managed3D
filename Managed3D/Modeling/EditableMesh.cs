/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Managed3D.Geometry;

namespace Managed3D.Modeling
{
    /// <summary>
    /// Provides an implementation of a 3d mesh that supports arbitrary modifications to it's topology.
    /// </summary>
    public class EditableMesh
    {
        #region Fields
        private Dictionary<long, EditableMeshVertex> vertices;
        private Dictionary<long, EditableMeshFace> faces;
        private Dictionary<long, EditableMeshEdge> edges;
        private Queue<int> freeVertexIds;
        private Queue<int> freeFaceIds;
        private Queue<int> freeEdgeIds;
        private int topVertexId;
        private int topFaceId;
        private int topEdgeId;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EditableMesh"/> class.
        /// </summary>
        public EditableMesh()
        {
            this.vertices = new Dictionary<long, EditableMeshVertex>();
            this.faces = new Dictionary<long, EditableMeshFace>();
            this.edges = new Dictionary<long, EditableMeshEdge>();

            this.freeEdgeIds = new Queue<int>();
            this.freeFaceIds = new Queue<int>();
            this.freeVertexIds = new Queue<int>();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets a value indicating the number of vertices contained in the mesh.
        /// </summary>
        public long VertexCount
        {
            get
            {
                return this.vertices.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating the number of edges contained in the mesh.
        /// </summary>
        public long EdgeCount
        {
            get
            {
                return this.edges.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating the number of faces contained in the mesh.
        /// </summary>
        public long FaceCount
        {
            get
            {
                return this.faces.Count;
            }
        }
        #endregion
        #region Methods
        public EditableMeshVertex GetVertex(long id)
        {
            return this.vertices[id];
        }

        public EditableMeshFace GetFace(long id)
        {
            return this.faces[id];
        }

        public EditableMeshEdge GetEdge(long id)
        {
            return this.edges[id];
        }

        public EditableMeshVertex CreateVertex(double x, double y, double z)
        {
            throw new NotImplementedException();
        }

        public EditableMeshEdge CreateEdge(long v1, long v2)
        {

            throw new NotImplementedException();
        }

        public EditableMeshFace CreateFace(long id1, long id2, long id3)
        {
            var v1 = this.GetVertex(id1);
            var v2 = this.GetVertex(id2);
            var v3 = this.GetVertex(id3);

            throw new NotImplementedException();

            var f = new EditableMeshFace(0);
            return f;
        }

        public EditableMeshFace CreateFace(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3)
        {
            var v1 = this.CreateVertex(x1, y1, z1);
            var v2 = this.CreateVertex(x2, y2, z2);
            var v3 = this.CreateVertex(x3, y3, z3);

            return this.CreateFace(v1.Id, v2.Id, v3.Id);
        }

        public EditableMeshVertex InsertVertexInEdge(long e)
        {
            throw new NotImplementedException();
        }

        public EditableMeshVertex InsertVertexInFace(long f)
        {
            throw new NotImplementedException();
        }

        public EditableMeshVertex WeldVertex(long sourceId, long targetId)
        {
            throw new NotImplementedException();
        }

        public EditableMeshEdge WeldEdges(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public void DeleteVertex(int id)
        {

        }

        public void DeleteEdge(int id)
        {

        }

        public void DeleteFace(int id)
        {

        }

        protected int GetVertexId()
        {
            if (this.freeVertexIds.Count == 0)
                for (int i = 0; i < 32; ++i)
                    this.freeVertexIds.Enqueue(this.topVertexId++);

            return this.freeVertexIds.Dequeue();
        }

        protected int GetFaceId()
        {
            if (this.freeFaceIds.Count == 0)
                for (int i = 0; i < 16; ++i)
                    this.freeFaceIds.Enqueue(this.topFaceId++);

            return this.freeFaceIds.Dequeue();
        }

        protected int GetEdgeId() {
            if (this.freeEdgeIds.Count == 0)
                for (int i = 0; i < 24; ++i)
                    this.freeEdgeIds.Enqueue(this.topEdgeId++);

            return this.freeEdgeIds.Dequeue();
        }
        #endregion


    }
}
