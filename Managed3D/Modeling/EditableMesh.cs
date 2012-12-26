﻿/******************************************************************************
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
using WEdge = Managed3D.Modeling.EditableMeshEdge;
using WFace = Managed3D.Modeling.EditableMeshFace;
using WVertex = Managed3D.Modeling.EditableMeshVertex;
namespace Managed3D.Modeling
{
    /// <summary>
    /// Provides an implementation of a 3d mesh that supports modifications to it's topology.
    /// </summary>
    public class EditableMesh : IRenderable
    {
        #region Fields
        private Dictionary<uint, WVertex> vertices;
        private Dictionary<uint, WEdge> edges;
        private Dictionary<uint, WFace> faces;
        private Dictionary<ushort, EMSparseAttribute> attributes;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EditableMesh"/> class.
        /// </summary>
        public EditableMesh()
        {

        }
        #endregion
        #region Properties
        IEnumerable<IRenderableVertex> IRenderable.Vertices
        {
            get
            {
                foreach (var wv in this.vertices.Values)
                    yield return wv;
            }
        }

        IEnumerable<IRenderableFace> IRenderable.Faces
        {
            get
            {
                foreach (var wf in this.faces.Values)
                    yield return wf;
            }
        }

        IEnumerable<IRenderableEdge> IRenderable.Edges
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion
        #region Methods
        public uint CreateVertex(double x, double y, double z)
        {
            var vertex = new Vertex3(x, y, z);

            throw new NotImplementedException();
            //this.vertices.add
        }

        /// <summary>
        /// Given two edges that share a common vertex, sets the edge 'wings' of each of the two given edges.
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        internal void SetWings(WEdge e1, WEdge e2)
        {
            Contract.Requires((e1.EdgeData.V0 == e2.EdgeData.V0) ||
                              (e1.EdgeData.V0 == e2.EdgeData.V1) ||
                              (e1.EdgeData.V1 == e2.EdgeData.V0) ||
                              (e1.EdgeData.V1 == e1.EdgeData.V1));

            if ((e1.AV == e2.AV) && (e1.BF == e2.AF))
            {
                e1.BCW = e2;
                e2.ACCW = e1;
            }
            else if ((e1.AV == e2.AV) && (e1.AF == e2.BF))
            {
                e1.ACCW = e2;
                e2.BCW = e1;
            }
            else if ((e1.AV == e2.BV) && (e1.BF == e2.BF))
            {
                e1.BCW = e2;
                e2.BCCW = e1;
            }
            else if ((e1.AV == e2.BV) && (e1.AF == e2.AF))
            {
                e1.ACCW = e2;
                e2.ACW = e1;
            }
            else if ((e1.BV == e2.AV) && (e1.AF == e2.AF))
            {
                e1.ACW = e2;
                e2.ACCW = e1;
            }
            else if ((e1.BV == e2.AV) && (e1.BF == e2.BF))
            {
                e1.BCCW = e2;
                e2.BCW = e1;
            }
            else if ((e1.BV == e2.BV) && (e1.AF == e2.BF))
            {
                e1.ACW = e2;
                e2.BCCW = e1;
            }
            else if ((e1.BV == e2.BV) && (e1.BF == e2.AF))
            {
                e1.BCCW = e2;
                e2.ACW = e1;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        internal static EditableMeshFace NextFaceAroundVertexCW(EditableMeshVertex vertex, EditableMeshFace known)
        {
            EditableMeshEdge e = null;

            while (e == null)
            {

            }



            throw new NotImplementedException();
        }

        internal static EditableMeshFace NextFaceAroundVertexCCW(EditableMeshVertex vertex, EditableMeshFace known)
        {

            throw new NotImplementedException();
        }
        #endregion


       
    }

    /// <summary>
    /// Half-edge editable mesh implementation.
    /// </summary>
    public class EMesh
    {

    }

    public class HEdge
    {
        public HEdge Opposite
        {
            get;
            set;
        }

        public HEdge Next
        {
            get;
            set;
        }

        public HEdge Previous
        {
            get;
            set;
        }

        public Vector3 Start
        {
            get;
            set;
        }
        public Vector3 End
        {
            get;
            set;
        }

        public HFace Face
        {
            get;
            set;
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this == this.Next.Previous);
            Contract.Invariant(this == this.Opposite.Opposite);
            Contract.Invariant(this.Face == this.Next.Face);
        }
    }

    public class HFace
    {
        
    }

    public class EMSparseAttribute
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
}
