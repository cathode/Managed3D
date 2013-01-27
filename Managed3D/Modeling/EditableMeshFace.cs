using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Managed3D.Geometry;

namespace Managed3D.Modeling
{
    /// <summary>
    /// Represents an <see cref="EditableMesh"/> face (triangle).
    /// </summary>
    public class EditableMeshFace : IFace
    {
        #region Fields
        private int id;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EditableMeshFace"/> class.
        /// </summary>
        internal EditableMeshFace(int id)
        {
            this.id = id;
        }
        #endregion
        #region Properties
        public EditableMeshEdge StartingEdge
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the unique id of the face (it's index in the mesh that contains it).
        /// </summary>
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

        /// <summary>
        /// Gets or sets an object that contains user data for the face.
        /// </summary>
        public object Tag
        {
            get;
            set;
        }

        IHalfEdge IFace.StartingEdge
        {
            get
            {
                return this.StartingEdge;
            }
        }
        #endregion
        #region Methods
        public IEnumerable<IHalfEdge> GetEdges()
        {
            var edge = this.StartingEdge;

            // Follow the edge loop in winding order.
            while (edge.Next != this.StartingEdge)
            {
                yield return edge;
                edge = edge.Next;
            }
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.StartingEdge != null);
            Contract.Invariant(this.StartingEdge.Face == this);
        }
        #endregion


        public IEnumerable<IFace> GetNeighboringFaces()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IVertex> GetVertices()
        {
            throw new NotImplementedException();
        }
    }
}
