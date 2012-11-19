/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/

namespace Managed3D.Rendering.OpenGL
{
    public enum BeginMode : int
    {
        Points = 0x00,
        Lines = 0x01,
        LineLoop = 0x02,
        LineStrip = 0x03,
        Triangles = 0x04,
        TriangleStrip = 0x05,
        TriangleFan = 0x06,
        Quads = 0x07,
        QuadStrip = 0x08,
        Polygon = 0x09,
    }
}
