using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Managed3D.Geometry;

namespace Managed3D.Modeling
{
    public interface IHalfEdge
    {
        #region Properties
        IHalfEdge Next { get; set; }
        IHalfEdge Previous { get; set; }
        IHalfEdge Opposite { get; set; }
        IVertex Start { get; set; }
        IVertex End { get; set; }
        IFace Face { get; set; }
        #endregion
    }
}
