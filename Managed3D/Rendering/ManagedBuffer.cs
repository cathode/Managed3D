/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Managed3D.Geometry;

namespace Managed3D.Rendering
{
    /// <summary>
    /// Represents a framebuffer that allows for 2 to N frames to be buffered.
    /// </summary>
    public sealed class ManagedBuffer
    {
        #region Fields
        private readonly Vector2i size;
        private readonly ManagedBufferColorPlane color;
        private readonly ManagedBufferDepthPlane depth;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedBuffer"/> class.
        /// </summary>
        /// <param name="width">The width of the buffer, in pixels.</param>
        /// <param name="height"></param>
        public ManagedBuffer(int width, int height)
        {
            Contract.Requires(width > 0);
            Contract.Requires(height > 0);

            this.size = new Vector2i(width, height);
            this.color = new ManagedBufferColorPlane(this.size);
            this.depth = new ManagedBufferDepthPlane(this.size);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedBuffer"/> class.
        /// </summary>
        /// <param name="size"></param>
        public ManagedBuffer(Vector2i size)
        {
            Contract.Requires(size.X > 0);
            Contract.Requires(size.Y > 0);

            this.size = size;
            this.color = new ManagedBufferColorPlane(this.size);
            this.depth = new ManagedBufferDepthPlane(this.size);
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the height (in pixels) of the managed buffer.
        /// </summary>
        public int Height
        {
            get
            {
                return this.size.Y;
            }
        }

        /// <summary>
        /// Gets the width (in pixels) of the managed buffer.
        /// </summary>
        public int Width
        {
            get
            {
                return this.size.X;
            }
        }

        /// <summary>
        /// Gets the size (in pixels) of the managed buffer.
        /// </summary>
        public Vector2i Size
        {
            get
            {
                return this.size;
            }
        }

        /// <summary>
        /// Gets the color plane of the frame buffer.
        /// </summary>
        public ManagedBufferColorPlane Color
        {
            get
            {
                return this.color;
            }
        }

        /// <summary>
        /// Gets the depth plane of the frame buffer.
        /// </summary>
        public ManagedBufferDepthPlane Depth
        {
            get
            {
                return this.depth;
            }
        }
        #endregion
    }
}
