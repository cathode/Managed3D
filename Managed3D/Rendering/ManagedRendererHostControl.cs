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
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using System.Threading;
using Managed3D.Geometry;
using Managed3D.Platform;

namespace Managed3D.Rendering
{
    /// <summary>
    /// Provides a windows forms control that hosts the output of a managed renderer.
    /// </summary>
    public unsafe sealed class ManagedRendererHostControl : Control, IManagedRendererTarget
    {
        #region Fields
        private bool needsUpdate;
        private Bitmap backBitmap;
        private Bitmap frontBitmap;
        private readonly object bufferLock = new object();
        private double rx;
        private double ry;
        private double tx;
        private double ty;
        private double sx;
        private double sy;
        private bool isRotating;
        private bool isPanning;
        private bool isZooming;
        private double autoRotate = 0.0;
        private double sensitivity = 0.322;
        internal Managed3D.SceneGraph.Scene scene;
        private bool isAutoRotateEnabled;
        #endregion
        #region Constructors
        internal ManagedRendererHostControl()
        {
            this.Padding = new Padding(0, 0, 0, 0);
            this.Margin = new Padding(0, 0, 0, 0);
        }
        #endregion
        #region Methods
        public void UpdateDisplayProfile(DisplayProfile profile)
        {
            lock (this.bufferLock)
            {
                // Do we even need to update the bitmap buffers?
                if (profile.Width == this.Width && profile.Height == this.Height)
                    return;

                // Make sure we don't leak memory by properly disposing any existing buffers.
                if (this.backBitmap != null)
                    this.backBitmap.Dispose();
                if (this.frontBitmap != null)
                    this.frontBitmap.Dispose();

                this.Size = new Size(profile.Width, profile.Height);
                this.backBitmap = new Bitmap(this.Width, this.Height);
                this.frontBitmap = new Bitmap(this.Width, this.Height);
                this.needsUpdate = true;
            }
        }

        public void ConsumeFrameBuffer(ManagedBuffer buffer)
        {
            lock (this.bufferLock)
            {
                buffer.Color.WriteToBitmap(this.backBitmap);
                Interlocked.Exchange(ref this.backBitmap, this.frontBitmap);
                this.needsUpdate = true;
            }
            this.Invalidate();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                if (this.isPanning)
                {
                    this.isZooming = true;
                    this.sx = e.X;
                    this.sy = e.Y;
                }
                else
                {
                    this.isRotating = true;
                    this.rx = e.X;
                    this.ry = e.Y;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (this.isRotating)
                {
                    this.tx = e.X;
                    this.ty = e.Y;
                }
            }
            else if (e.Button == (MouseButtons.Left | MouseButtons.Right))
            {
                this.sx = e.X;
                this.sy = e.Y;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.isZooming = false;
               
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.isZooming = false;
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
            {
                var xdelta = e.X - this.rx;
                var ydelta = e.Y - this.ry;
                this.rx += xdelta;
                this.ry += ydelta;
                this.scene.ActiveCamera.Orientation = (Vector3)this.scene.ActiveCamera.Orientation + new Vector3(ydelta * this.sensitivity, xdelta * this.sensitivity, 0);
            }
            else if (e.Button == MouseButtons.Right)
            {
                var xdelta = e.X - this.tx;
                var ydelta = e.Y - this.ty;
                this.tx += xdelta;
                this.ty += ydelta;
                this.scene.ActiveCamera.Position = (Vector3)this.scene.ActiveCamera.Position + new Vector3(0, xdelta * this.sensitivity, ydelta * this.sensitivity);
            }
            else if (e.Button == (MouseButtons.Left | MouseButtons.Right))
            {
                var xdelta = e.X - this.sx;
                var ydelta = e.Y - this.sy;
                this.sx += xdelta;
                this.sy += ydelta;
                var scale = (xdelta / 2.0 + ydelta / 2.0) / 2.0 * sensitivity;
                this.scene.ActiveCamera.Scale = (Vector3)this.scene.ActiveCamera.Scale + new Vector3(scale, scale, scale);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Space)
                this.isAutoRotateEnabled = !this.isAutoRotateEnabled;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.backBitmap != null && this.needsUpdate)
                lock (this.bufferLock)
                    e.Graphics.DrawImageUnscaled(this.frontBitmap, 0, 0);

            this.needsUpdate = false;
            if (this.isAutoRotateEnabled)
                this.scene.ActiveCamera.Orientation = (Vector3)this.scene.ActiveCamera.Orientation + new Vector3(0, 0.33, 0);
            //e.Graphics.DrawImage(this.frontBitmap, this.ClientRectangle);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }

        private unsafe void RendererPostRenderCallback(object sender, RenderEventArgs e)
        {
            if (this.isAutoRotateEnabled)
                this.scene.ActiveCamera.Orientation = (Vector3)this.scene.ActiveCamera.Orientation + new Vector3(0, 1, 0);
        }
        #endregion


    }
}
