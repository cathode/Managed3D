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
        #region Constructors
        public EditableMeshVertex(Vector3 position, EditableMeshEdge edge)
        {
            this.Position = position;
            this.Edge = edge;
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
            set;
        }
        #endregion
        #region Methods
        public IEnumerable<EditableMeshVertex> FacesAroundVertex()
        {
            if (this == Edge.Start)
            {
                var e = this.Edge.Next;
                while (e != this.Edge)
                {

                }
            }
            else
            {

            }
            yield break;
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.Edge != null);
        }
        #endregion

        public int Id
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
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<IVertex> GetNeighboringVertices()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IHalfEdge> GetOutgoingEdges()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IHalfEdge> GetIncomingEdges()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFace> GetNeighboringFaces()
        {
            throw new NotImplementedException();
        }
    }
}
