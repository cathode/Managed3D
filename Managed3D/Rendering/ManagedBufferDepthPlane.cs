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
using Managed3D.Geometry;

namespace Managed3D.Rendering
{
    /// <summary>
    /// Represents the depth buffer plane of a <see cref="ManagedBuffer"/>.
    /// </summary>
    public sealed class ManagedBufferDepthPlane
    {
        #region Fields
        private readonly Vector2i size;
        private readonly float[] plane;
        private readonly int stride;
        #endregion
        #region Constructors
        internal ManagedBufferDepthPlane(Vector2i size)
        {
            this.size = size;
            this.stride = size.X;
            this.plane = new float[size.X * size.Y];
        }
        #endregion
        #region Indexers
        public float this[int x, int y]
        {
            get
            {
                return this.plane[(y * this.stride) + x];
            }
            set
            {
                this.plane[(y * this.stride) + x] = value;
            }
        }
        #endregion
        #region Methods

        public void Clear()
        {
            this.Clear(0.0f);
        }
        public void Clear(float value)
        {
           
        }
        #endregion
    }
}
