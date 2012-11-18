/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Managed3D.Geometry4;
using Managed3D.SoftwareProvider;
using Managed3D;

namespace ManagedStudio3D
{
    /// <summary>
    /// A viewport implementation that uses the built-in software rasterizer.
    /// </summary>
    public sealed class SoftwareViewportControl : Control
    {
        #region Fields
        private readonly SoftwareRasterizer rasterizer;
        private Bitmap buffer;
        #endregion
        #region Constructors
        public SoftwareViewportControl()
            : this(new SoftwareRasterizer())
        {
        }
        public SoftwareViewportControl(SoftwareRasterizer rasterizer)
        {
            if (rasterizer == null)
                throw new ArgumentNullException("rasterizer");
               
            this.rasterizer = rasterizer;
            this.rasterizer.FrameSizeChanged += new EventHandler(rasterizer_FrameSizeChanged);
            this.rasterizer.FramePostRender += new EventHandler<RenderArgs>(rasterizer_FramePostRender);

            this.buffer = new Bitmap(this.rasterizer.FrameWidth, this.rasterizer.FrameHeight);
        }
        void rasterizer_FramePostRender(object sender, RenderArgs e)
        {
            var data = this.buffer.LockBits(new Rectangle(0, 0, this.buffer.Width, this.buffer.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            long n = 0;
            //Vector4 v;

            Frame frame = this.rasterizer.Buffer.Front;

            unsafe
            {
                byte* px = (byte*)data.Scan0.ToPointer();

                if (BitConverter.IsLittleEndian)
                    for (int y = 0; y < this.buffer.Height; y++)
                        for (int x = 0; x < this.buffer.Width; x++)
                        {
                            var v = frame[x, y];
                            px[n++] = (byte)(v.Z * 255);
                            px[n++] = (byte)(v.Y * 255);
                            px[n++] = (byte)(v.X * 255);
                            px[n++] = 255;
                        }
                else
                    for (int y = 0; y < this.buffer.Height; y++)
                        for (int x = 0; x < this.buffer.Width; x++)
                        {
                            var v = frame[x, y];
                            px[n++] = 255;
                            px[n++] = (byte)(v.X * 255);
                            px[n++] = (byte)(v.Y * 255);
                            px[n++] = (byte)(v.Z * 255);
                        }
            }
            this.rasterizer.Buffer.CycleBuffers();
            this.buffer.UnlockBits(data);

            this.Invalidate();
        }

        void rasterizer_FrameSizeChanged(object sender, EventArgs e)
        {
            this.buffer = new Bitmap(this.rasterizer.FrameWidth, this.rasterizer.FrameHeight);
            this.Size = this.rasterizer.FrameSize;
        }
        #endregion
        #region Methods
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.Clear(Color.Black);

            if (this.buffer == null)
                return;

            e.Graphics.DrawImage(this.buffer, this.ClientRectangle);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            this.Invalidate();
        }
        #endregion
    }
}
