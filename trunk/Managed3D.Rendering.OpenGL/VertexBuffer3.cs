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
    /// Represents an OpenGL Vertex Buffer Object containing 3-dimensional vertices.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public class VertexBuffer3
    {
        [FieldOffset(0x0)]
        public BufferedVertex3[] Data;
    }
}
