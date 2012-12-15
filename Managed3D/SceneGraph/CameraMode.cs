/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Managed3D.SceneGraph
{
    /// <summary>
    /// Enumerates supported camera view modes.
    /// </summary>
    public enum CameraMode
    {
        /// <summary>
        /// Indicates that the camera provides a perspective view of the scene.
        /// </summary>
        Perspective = 0x0,

        /// <summary>
        /// Indicates that the camera provides an orthographic view of the scene.
        /// </summary>
        Orthographic = 0x1,
    }
}
