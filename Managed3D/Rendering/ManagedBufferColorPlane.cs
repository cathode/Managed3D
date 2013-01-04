/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using Managed3D.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics.Contracts;

namespace Managed3D.Rendering
{
    /// <summary>
    /// Represents the color plane of a managed frame buffer.
    /// </summary>
    public sealed class ManagedBufferColorPlane
    {
        #region Fields
        private readonly Vector2i size;
        private readonly Vector4f[] pixels;
        private readonly int pixelCount;
        private readonly int stride;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedBufferColorPlane"/> class.
        /// </summary>
        /// <param name="width">The width (in pixels) of the new frame.</param>
        /// <param name="height">The height (in pixels) of the new frame.</param>
        internal ManagedBufferColorPlane(Vector2i size)
        {
            Contract.Ensures(this.Pixels.Length == size.X * size.Y);

            this.size = size;
            this.pixelCount = size.X * size.Y;
            this.stride = size.X;
            this.pixels = new Vector4f[this.size.X * this.size.Y];
        }
        #endregion
        #region Indexers
        public unsafe Vector4f this[int x, int y]
        {
            get
            {
                fixed (Vector4f* ptr = &this.pixels[0])
                    return *((Vector4f*)ptr + (((y * this.stride) + x) % this.pixelCount));
            }
            set
            {
                unchecked
                {
                    this.pixels[((y * this.stride) + x) % this.pixelCount] = value;
                }
            }
        }
        #endregion
        #region Properties
        public Vector4f[] Pixels
        {
            get
            {
                return this.pixels;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Clears the color plane to the specified color.
        /// </summary>
        /// <param name="clearColor"></param>
        public unsafe void Clear(Vector4f clearColor)
        {
            int n = -1;
            fixed (Vector4f* ptr = &this.pixels[0])
            {
                for (int i = this.pixels.Length - 1; i > -1; --i)
                {
                    ptr[++n] = clearColor;
                }
            }
        }

        public unsafe void WriteToBitmap(System.Drawing.Bitmap bitmap)
        {
            Contract.Requires(bitmap != null);

            var bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unchecked
            {
                int* ptr = (int*)bmpData.Scan0.ToPointer();
                fixed (Vector4f* cptr = &(this.Pixels[0]))
                {
                    float* fp = (float*)cptr;
                    int max = this.Pixels.Length - 1;
                    for (int i = -1, n = -1; i < max; )
                    {
                        ptr[++i] = (byte)(fp[++n] * 255) | (byte)(fp[++n] * 255) << 8 | (byte)(fp[++n] * 255) << 16 | (byte)(fp[++n] * 255) << 24;
                    }
                }
            }

            bitmap.UnlockBits(bmpData);
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.Pixels != null);
            //Contract.Invariant(this.
        }
        #endregion
    }
}
