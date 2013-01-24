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

        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EditableMeshVertex"/> class.
        /// </summary>
        /// <param name="position">The position of the vertex.</param>
        internal EditableMeshVertex(Vector3 position)
        {
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
                throw new NotImplementedException();
            }
        }

        public Vector3 Normal
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
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
            Contract.Invariant(this.Edge != null);
        }
        #endregion
    }
}
