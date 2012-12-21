using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.SceneGraph
{
    /// <summary>
    /// Enumerates pre-defined  facings of a camera.
    /// </summary>
    [Flags]
    public enum CameraFacing
    {
        /// <summary>
        /// Indicates the camera has no explicit direction or facing.
        /// </summary>
        Any = 0x00,

        /// <summary>
        /// Indicates the camera is positioned above the scene, looking down.
        /// </summary>
        Above = 0x01,

        /// <summary>
        /// Indicates the camera is positioned below the scene, looking up.
        /// </summary>
        Below = 0x02,

        /// <summary>
        /// Indicates the camera is facing north. This is the default facing.
        /// </summary>
        North = 0x04,

        /// <summary>
        /// Indicates the camera is facing east. 
        /// </summary>
        East = 0x08,

        /// <summary>
        /// Indicates the camera is facing south.
        /// </summary>
        South = 0x10,

        /// <summary>
        /// Indicates the camera is facing west.
        /// </summary>
        West = 0x20,

        /// <summary>
        /// Indicates the camera is mirrored horizontally.
        /// </summary>
        Mirrored = 0x40,

        Isometric,

        Trimetric,

        Dimetric,
    }
}
