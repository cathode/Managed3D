/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Rendering
{
    /// <summary>
    /// Enumerates general styles of displaying 3D objects.
    /// </summary>
    public enum RenderStyle
    {
        /// <summary>
        /// Indicates the 3D object(s) are displayed in wireframe mode; only the line segments 
        /// </summary>
        Wireframe = 0,

        /// <summary>
        /// Indicates the objecs to be rendered are displayed as solid
        /// </summary>
        Solid = 1,
        Smooth = 2,
        Textured = 3,
    }
}
