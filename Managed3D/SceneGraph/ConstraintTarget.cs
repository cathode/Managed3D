using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.SceneGraph
{
    /// <summary>
    /// Enumerates supported targets for a graph node constraint.
    /// </summary>
    [Flags]
    public enum ConstraintTarget
    {
        None = 0x00,
        Position = 0x01,
        Orientation = 0x02,
        Scale = 0x04,
        All = Position | Orientation | Scale,
    }
}
