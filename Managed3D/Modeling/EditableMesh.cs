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
        private EditableMeshVertexCollection vertices;
        private EditableMeshFaceCollection faces;
        private EditableMeshEdgeCollection edges;
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
            this.vertices = new EditableMeshVertexCollection();
            this.faces = new EditableMeshFaceCollection();
            this.edges = new EditableMeshEdgeCollection();

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
        public EditableMeshVertex GetVertex(int id)
        {
            return this.vertices[id];
        }

        public EditableMeshFace GetFace(int id)
        {
            return this.faces[id];
        }

        public EditableMeshEdge GetEdge(int id)
        {
            return this.edges[id];
        }

        public EditableMeshVertex CreateVertex(double x, double y, double z)
        {
            var id = this.GetVertexId();
            var v = new EditableMeshVertex(id, new Vector3(x, y, z));
            this.vertices.Add(v);
            return v;
        }

        public EditableMeshEdge CreateEdge(int id0, int id1)
        {
            var v0 = this.GetVertex(id0);
            var v1 = this.GetVertex(id1);

            if (v0.Edge != null)
                if ((v0.Edge.Start.Id == id0 && v0.Edge.End.Id == id1) || (v0.Edge.End.Id == id0 && v0.Edge.End.Id == id1))
                    return v0.Edge;

            var e = new EditableMeshEdge(this.GetEdgeId(), v0, v1);
            this.edges.Add(e);
            v0.Edge = e;
            return e;
        }

        public EditableMeshFace CreateFace(int id1, int id2, int id3)
        {
            var v1 = this.GetVertex(id1);
            var v2 = this.GetVertex(id2);
            var v3 = this.GetVertex(id3);

            throw new NotImplementedException();

            var f = new EditableMeshFace(0);
            return f;
        }
        public EditableMeshFace CreateFace(EditableMeshVertex v0, EditableMeshVertex v1, EditableMeshVertex v2, EditableMeshVertex v3)
        {
            var e0 = this.CreateEdge(v0.Id, v1.Id);
            var e1 = this.CreateEdge(v1.Id, v2.Id);
            var e2 = this.CreateEdge(v2.Id, v3.Id);
            var e3 = this.CreateEdge(v3.Id, v0.Id);

            var f = new EditableMeshFace(this.GetFaceId()) { StartingEdge = e0 };
            this.faces.Add(f);
            return f;
        }
        public EditableMeshFace CreateFace(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3)
        {
            var v1 = this.CreateVertex(x1, y1, z1);
            var v2 = this.CreateVertex(x2, y2, z2);
            var v3 = this.CreateVertex(x3, y3, z3);

            return this.CreateFace(v1.Id, v2.Id, v3.Id);
        }

        public EditableMeshVertex InsertVertexInEdge(int e)
        {
            throw new NotImplementedException();
        }

        public EditableMeshVertex InsertVertexInFace(int f)
        {
            throw new NotImplementedException();
        }

        public EditableMeshVertex WeldVertex(int sourceId, int targetId)
        {
            throw new NotImplementedException();
        }

        public EditableMeshEdge WeldEdges(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a vertex, resulting in cascading deletions of faces and edges.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteVertex(int id)
        {

        }

        /// <summary>
        /// Deletes an edge, resulting in cascading deletions of faces and orphaned vertices.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteEdge(int id)
        {

        }

        /// <summary>
        /// Deletes a face, resulting in cascading deletions of half-edges and orphaned vertices.
        /// </summary>
        /// <param name="id"></param>
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

        protected int GetEdgeId()
        {
            if (this.freeEdgeIds.Count == 0)
                for (int i = 0; i < 24; ++i)
                    this.freeEdgeIds.Enqueue(this.topEdgeId++);

            return this.freeEdgeIds.Dequeue();
        }

        public Mesh3 ConvertToRenderableMesh()
        {
            var m = new Mesh3();
            
            var tl = new List<Triangle3>();

            foreach (var face in this.faces)
            {
                
            }

            return m;
        }
        #endregion


    }
}
