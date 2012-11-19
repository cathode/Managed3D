/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/

namespace Managed3D.Geometry
{
    public enum PrimitiveKind
    {
        Point = 0x0,
        Line = 0x1,
        LineStrip = 0x3,
        LineLoop = 0x2,
        Triangle = 0x4,
        TriangleStrip = 0x5,
        TriangleFan = 0x6,
        Quad = 0x7,
        QuadStrip = 0x8,
        Polygon = 0x9,
    }
}
