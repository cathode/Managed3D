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

namespace Managed3D.Geometry
{
    public struct Vector2f
    {
        private readonly float x;
        private readonly float y;

        public Vector2f(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float X
        {
            get
            {
                return this.x;
            }
        }

        public float Y
        {
            get
            {
                return this.y;
            }
        }
    }
}
