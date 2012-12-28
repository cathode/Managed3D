using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.SceneGraph
{
    /// <summary>
    /// Enumerates pre-defined  facings of a camera.
    /// </summary>
  
    public enum CameraFacing
    {
        /// <summary>
        /// Indicates the camera has no explicit direction or facing.
        /// </summary>
        Forward = 0x00,

        Backward = 0x01,

        /// <summary>
        /// Indicates the camera is facing north. This is the default facing.
        /// </summary>
        North = Forward,

        /// <summary>
        /// Indicates the camera is facing south.
        /// </summary>
        South = Backward,

        /// <summary>
        /// Indicates the camera is positioned above the scene, looking down.
        /// </summary>
        Up = 0x02,

        /// <summary>
        /// Indicates the camera is positioned below the scene, looking up.
        /// </summary>
        Down = 0x03,

        /// <summary>
        /// Indicates the camera is facing east. 
        /// </summary>
        East = 0x04,

        /// <summary>
        /// Indicates the camera is facing west.
        /// </summary>
        West = 0x05,

        Isometric = 0x06,
    }
}
