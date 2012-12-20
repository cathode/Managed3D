using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.SceneGraph
{
    /// <summary>
    /// Represents a visibility group, which allows control over whether nodes are shown or hidden.
    /// </summary>
    [Flags]
    public enum VisibilityGroup : ulong
    {
        None = 0x0000,
        G0 = 0x0001,
        G1 = 0x0002,
        G2 = 0x0004,
        G3 = 0x0008,
        G4 = 0x0010,
        G5 = 0x0020,
        G6 = 0x0040,
        G7 = 0x0080,
        G8 = 0x0100,
        G9 = 0x0200,
        G10 = 0x0400,
        G11 = 0x0800,
        G12 = 0x1000,
        G13 = 0x2000,
        G14 = 0x4000,
        G15 = 0x8000,
        
        /// <summary>
        /// Camera visibility group 0.
        /// </summary>
        Camera0 = 0x1000000000000000,
        Camera1 = 0x2000000000000000,
        Camera2 = 0x4000000000000000,
        Camera3 = 0x8000000000000000,
        
        All     = 0xFFFFFFFFFFFFFFFF,
    }
}
