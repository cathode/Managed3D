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
using System.Diagnostics.Contracts;
using Managed3D.Geometry;

namespace Managed3D.Modeling
{
    /// <summary>
    /// Represents a vertex of an editable mesh.
    /// </summary>
    public class EditableMeshVertex : IVertex
    {
        #region Fields
        private int id;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EditableMeshVertex"/> class.
        /// </summary>
        /// <param name="id">The id of the vertex within the context of the mesh it belongs to.</param>
        /// <param name="position">The position of the vertex.</param>
        internal EditableMeshVertex(int id, Vector3 position)
        {
            this.id = id;
            this.Position = position;
        }
        #endregion
        #region Properties
        public Vector3 Position
        {
            get;
            set;
        }

        public EditableMeshEdge Edge
        {
            get;
            internal set;
        }
        public int Id
        {
            get
            {
                return this.id;
            }
            internal set
            {
                this.id = value;
            }
        }

        public Vector3 Normal
        {
            get;
            set;
        }

        public object Tag
        {
            get;
            set;
        }
        #endregion
        #region Methods
        public IEnumerable<EditableMeshFace> GetNeighboringFaces()
        {
            // Start with the first outgoing edge.
            var edge = this.Edge;

            throw new NotImplementedException();
        }

        IEnumerable<IVertex> IVertex.GetNeighboringVertices()
        {
            throw new NotImplementedException();
        }

        IEnumerable<IHalfEdge> IVertex.GetOutgoingEdges()
        {
            throw new NotImplementedException();
        }

        IEnumerable<IHalfEdge> IVertex.GetIncomingEdges()
        {
            throw new NotImplementedException();
        }

        IEnumerable<IFace> IVertex.GetNeighboringFaces()
        {
            throw new NotImplementedException();
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            //Contract.Invariant(this.Edge != null);
        }
        #endregion

        public SelectionTarget SelectAs
        {
            get
            {
                return SelectionTarget.Vertex;
            }
        }
    }
}
