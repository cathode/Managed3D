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
using System.Runtime.InteropServices;

namespace Managed3D.Rendering.OpenGL
{
    /// <summary>
    /// Provides a managed wrapper around the OpenGL Utility library.
    /// </summary>
    public static class GLU
    {
        #region Fields
        public const string DLL = "Glu32.dll";
        #endregion
        #region Methods
        [DllImport(GLU.DLL, EntryPoint = "gluPerspective")]
        public static extern void Perspective(double fovy, double aspect, double zNear, double zFar);
        #endregion
    }
}
