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
        int Id { get; }
        IHalfEdge Next { get; }
        IHalfEdge Previous { get; }
        IHalfEdge Opposite { get; }
        IVertex Start { get; }
        IVertex End { get; }
        IFace Face { get; }
        object Tag { get; set; }
        #endregion
    }
}
