using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.SceneGraph
{
    /// <summary>
    /// Enumerates the modifiers that a camera can be locked to.
    /// </summary>
    [Flags]
    public enum CameraModifierLock
    {
        /// <summary>
        /// Indicates the camera is not locked to any modifiers.
        /// </summary>
        None            = 0x0000,

        /// <summary>
        /// Indicates the camera rotation is locked on it's X axis.
        /// </summary>
        RotationX       = 0x0001,
        
        /// <summary>
        /// Indicates the camera rotation is locked on it's Y axis.
        /// </summary>
        RotationY       = 0x0002,
        
        /// <summary>
        /// Indicates the camera rotation is locked on it's Z axis.
        /// </summary>
        RotationZ       = 0x0004,
        
        /// <summary>
        /// Indicates the camera scale is locked on the X axis.
        /// </summary>
        ScaleX          = 0x0008,
        
        /// <summary>
        /// Indicates the camera scale is locked on the Y axis.
        /// </summary>
        ScaleY          = 0x0010,
        
        /// <summary>
        /// Indicates the camera scale is locked on the Z axis.
        /// </summary>
        ScaleZ          = 0x0020,
        
        /// <summary>
        /// Indicates the camera position is locked on the X axis.
        /// </summary>
        TranslationX    = 0x0040,
        
        /// <summary>
        /// Indicates the camera position is locked on the Y axis.
        /// </summary>
        TranslationY    = 0x0080,
        
        /// <summary>
        /// Indicates the camera position is locked on the Z axis.
        /// </summary>
        TranslationZ    = 0x0100,
        
        /// <summary>
        /// Indicates all modifiers of the camera are locked.
        /// </summary>
        All             = 0xFFFF,
    }

}
