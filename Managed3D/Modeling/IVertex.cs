using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Managed3D.Geometry;

namespace Managed3D.Modeling
{
    public interface IVertex
    {
        #region Properties
        int Id { get; }
        Vector3 Position { get; set; }
        Vector3 Normal { get; }
        object Tag { get; set; }
        #endregion
        #region Methods
        IEnumerable<IVertex> GetNeighboringVertices();
        IEnumerable<IHalfEdge> GetOutgoingEdges();
        IEnumerable<IHalfEdge> GetIncomingEdges();
        IEnumerable<IFace> GetNeighboringFaces();
        #endregion
    }
}
