using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents an immutable 3D mesh that can be streamed directly to graphics hardware.
    /// </summary>
    public sealed class FastMesh
    {
        private float[] vertices;
        private uint[] faces;
    }
}
