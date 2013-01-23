using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Modeling
{
    public interface IFace
    {
        #region Properties
        long Id { get; }
        IHalfEdge StartingEdge { get; }
        object Tag { get; set; }
        #endregion
        #region Methods
        IEnumerable<IHalfEdge> GetEdges();
        #endregion
    }
}
