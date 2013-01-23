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
        private readonly long id;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EditableMeshFace"/> class.
        /// </summary>
        internal EditableMeshFace(long id)
        {
            this.id = id;
        }
        #endregion
        #region Properties
        public EditableMeshEdge Edge
        {
            get;
            set;
        }
        

        public long Id
        {
            get
            {
                return this.id;
            }
        }

        public IHalfEdge StartingEdge
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

        /// <summary>
        /// Gets or sets an object that contains user data for the face.
        /// </summary>
        public object Tag
        {
            get;
            set;
        }
        #endregion
        #region Methods
        public IEnumerable<IHalfEdge> GetEdges()
        {
            throw new NotImplementedException();
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.Edge != null);
            Contract.Invariant(this.Edge.Face == this);
        }
        #endregion
    }
}
