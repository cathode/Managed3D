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

namespace Managed3D.Platform.Microsoft
{
    /// <summary>
    /// Indicates the type of pixels in a <see cref="PixelFormatDescriptor"/>.
    /// </summary>
    public enum PfdPixelType : byte
    {
        Rgba = 0,
        ColorIndex = 1,
    }
}
