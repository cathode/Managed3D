/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using Managed3D.Geometry;

namespace Managed3D.SceneGraph
{
    public abstract class LightNode : Node
    {
        public Vector4 Diffuse
        {
            get;
            set;
        }
        public Vector4 Specular
        {
            get;
            set;
        }
    }
}
