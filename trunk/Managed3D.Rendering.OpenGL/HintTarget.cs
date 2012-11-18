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

namespace Managed3D.Rendering.OpenGL
{
    public enum HintTarget
    {
        PerspectiveCorrection = 0x0C50,
        PointSmooth = 0x0C51,
        LineSmooth = 0x0C52,
        PolygonSmooth = 0x0C53,
        Fog = 0x0C54,
    }
}
