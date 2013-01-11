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
using Managed3D.SceneGraph;
using System.Diagnostics.Contracts;

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
        //private double autoRotate = 0.0;
        private double sensitivity = 0.4;
        internal Managed3D.SceneGraph.Scene scene;
        private bool isAutoRotateEnabled;
        private ManagedRenderer renderer;
        #endregion
        #region Constructors
        public ManagedRendererHostControl(ManagedRenderer renderer)
        {
            Contract.Requires(renderer != null);

            this.Padding = new Padding(0, 0, 0, 0);
            this.Margin = new Padding(0, 0, 0, 0);
            this.renderer = renderer;
        }
        #endregion
        #region Properties
        public ManagedRenderer Renderer
        {
            get
            {
                Contract.Ensures(Contract.Result<ManagedRenderer>() != null);

                return this.renderer;
            }
            set
            {
                Contract.Requires(value != null);

                this.renderer = value;
            }
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
            //Contract.Requires(e != null);

            base.OnMouseDown(e);

            if (Control.MouseButtons == MouseButtons.Left)
            {
                this.rx = e.X;
                this.ry = e.Y;
                this.isRotating = true;
            }
            else if (Control.MouseButtons == MouseButtons.Right)
            {
                this.tx = e.X;
                this.ty = e.Y;
                this.isPanning = true;

            }
            else if (Control.MouseButtons == (MouseButtons.Left | MouseButtons.Right))
            {
                this.sx = e.X;
                this.sy = e.Y;
                this.isPanning = false;
                this.isRotating = false;
                this.isZooming = true;
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            //Contract.Requires(e != null);

            base.OnMouseUp(e);

            if (this.isRotating)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    this.isRotating = !this.isRotating;
            }
            else if (this.isPanning)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    this.isPanning = !this.isPanning;
            }
            else if (this.isZooming)
            {
                this.isZooming = false;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            //Contract.Requires(e != null);

            base.OnMouseMove(e);

            if (this.isRotating)
            {
                if (Control.ModifierKeys == Keys.Control)
                {
                    this.renderer.ActiveCamera.Orientation = Quaternion.LookAt(new Vector3(e.X - (e.X / 2.0), e.Y - (e.Y / 2.0), 10));
                }
                else
                {
                    var xdelta = e.X - this.rx;
                    var ydelta = e.Y - this.ry;
                    this.rx += xdelta;
                    this.ry += ydelta;
                    this.renderer.ActiveCamera.Orientation *= new Quaternion(Vector3.Up, Angle.FromDegrees(-xdelta * sensitivity));
                }
            }
            else if (this.isPanning)
            {
                var xdelta = e.X - this.tx;
                var ydelta = e.Y - this.ty;
                this.tx += xdelta;
                this.ty += ydelta;
                var s = this.renderer.ActiveCamera.Scale.X;

                this.renderer.ActiveCamera.Position = (Vector3)this.renderer.ActiveCamera.Position + new Vector3(xdelta * (this.sensitivity * 3 * (1.0 / s)), ydelta * (this.sensitivity * 3 * (1.0 / s)), 0);
            }
            else if (this.isZooming)
            {
                var xdelta = e.X - this.sx;
                var ydelta = e.Y - this.sy;
                this.sx += xdelta;
                this.sy += ydelta;
                var scale = (xdelta / 2.0 + ydelta / 2.0) / 2.0 * sensitivity;
                this.renderer.ActiveCamera.Scale = (Vector3)this.renderer.ActiveCamera.Scale + new Vector3(scale, scale, scale);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //Contract.Requires(e != null);

            base.OnKeyDown(e);

            var cam = this.renderer.ActiveCamera;

            if (cam == null)
                return;

            switch (e.KeyCode)
            {
                case Keys.NumPad2:
                    cam.Facing = CameraFacing.South;
                    cam.UpdateFacing();
                    break;

                case Keys.NumPad4:
                    cam.Facing = CameraFacing.West;
                    cam.UpdateFacing();
                    break;

                case Keys.NumPad6:
                    cam.Facing = CameraFacing.East;
                    cam.UpdateFacing();
                    break;

                case Keys.NumPad8:
                    cam.Facing = CameraFacing.North;
                    cam.UpdateFacing();
                    break;

                case Keys.NumPad7:
                    cam.Facing = CameraFacing.Up;
                    cam.UpdateFacing();
                    break;

                case Keys.NumPad9:
                    cam.Facing = CameraFacing.Down;
                    cam.UpdateFacing();
                    break;

                case Keys.NumPad5:
                    cam.Facing = CameraFacing.Isometric;
                    cam.UpdateFacing();
                    break;

                case Keys.D1:
                    if (cam.VisibleGroups.HasFlag(VisibilityGroup.G0))
                        cam.VisibleGroups &= ~VisibilityGroup.G0;
                    else
                        cam.VisibleGroups |= VisibilityGroup.G0;
                    break;

                case Keys.D2:
                    if (cam.VisibleGroups.HasFlag(VisibilityGroup.G1))
                        cam.VisibleGroups &= ~VisibilityGroup.G1;
                    else
                        cam.VisibleGroups |= VisibilityGroup.G1;
                    break;

                case Keys.D3:
                    if (cam.VisibleGroups.HasFlag(VisibilityGroup.G2))
                        cam.VisibleGroups &= ~VisibilityGroup.G2;
                    else
                        cam.VisibleGroups |= VisibilityGroup.G2;
                    break;

                case Keys.D4:
                    if (cam.VisibleGroups.HasFlag(VisibilityGroup.G3))
                        cam.VisibleGroups &= ~VisibilityGroup.G3;
                    else
                        cam.VisibleGroups |= VisibilityGroup.G3;
                    break;

                case Keys.Z:
                    this.renderer.ZoomExtents(this.scene.Root.GetGraphExtents());
                    break;

                case Keys.Space:
                    this.isAutoRotateEnabled = !this.isAutoRotateEnabled;
                    break;
            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.backBitmap != null && this.needsUpdate)
                lock (this.bufferLock)
                    e.Graphics.DrawImageUnscaled(this.frontBitmap, 0, 0);

            this.needsUpdate = false;
            if (this.isAutoRotateEnabled)
            {
                //var axis = this.renderer.ActiveCamera.Orientation.GetAxis();
                this.renderer.ActiveCamera.Orientation *= new Quaternion(Vector3.Up, Angle.FromDegrees(0.5));
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }

        private unsafe void RendererPostRenderCallback(object sender, RenderEventArgs e)
        {

        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.Renderer != null);
        }
        #endregion


    }
}
