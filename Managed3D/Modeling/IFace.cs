using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Modeling
{
    public interface IFace : ISelectable
    {
        #region Properties
        int Id { get; }
        IHalfEdge StartingEdge { get; }
        object Tag { get; set; }
        #endregion
        #region Methods
        IEnumerable<IHalfEdge> GetEdges();
        IEnumerable<IFace> GetNeighboringFaces();
        IEnumerable<IVertex> GetVertices();
        
        #endregion
    }
}
