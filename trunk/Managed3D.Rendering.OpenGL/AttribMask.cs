﻿/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Rendering.OpenGL
{
    public enum AttribMask
    {
        ColorBuffer = GL.GL_COLOR_BUFFER_BIT,
        DepthBuffer = GL.GL_DEPTH_BUFFER_BIT,
        StencilBuffer = GL.GL_STENCIL_BUFFER_BIT,
    }
}
