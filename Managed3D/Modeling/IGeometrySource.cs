using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Modeling
{
    /// <summary>
    /// Represents an interface for a type that produces geometry based on one or more parameters.
    /// </summary>
    public interface IGeometrySource
    {
        EditableMesh Generate();
    }
}
