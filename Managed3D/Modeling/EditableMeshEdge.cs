using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Managed3D.Modeling
{
    /// <summary>
    /// Represents a render-dynamic edge.
    /// </summary>
    public class EditableMeshEdge : IHalfEdge
    {
        #region Fields
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EMEdge"/> class.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public EditableMeshEdge()
        {

        }
        #endregion
        #region Properties
        public EditableMeshEdge Opposite
        {
            get;
            set;
        }

        public EditableMeshEdge Next
        {
            get;
            set;
        }

        public EditableMeshEdge Previous
        {
            get;
            set;
        }

        public EditableMeshVertex Start
        {
            get;
            set;
        }
        public EditableMeshVertex End
        {
            get;
            set;
        }

        public EditableMeshFace Face
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public object Tag { get; set; }
        #endregion
        #region MEthods
        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this == this.Next.Previous);
            Contract.Invariant(this == this.Opposite.Opposite);
            Contract.Invariant(this.Face == this.Next.Face);
        }
        #endregion


        IHalfEdge IHalfEdge.Next
        {
            get
            {
                return this.Next;
            }
        }

        IHalfEdge IHalfEdge.Previous
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IHalfEdge IHalfEdge.Opposite
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IVertex IHalfEdge.Start
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IVertex IHalfEdge.End
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IFace IHalfEdge.Face
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public SelectionTarget SelectionKind
        {
            get
            {
                return SelectionTarget.Edge;
            }
        }
    }
}
