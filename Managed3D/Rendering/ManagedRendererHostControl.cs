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
        //private bool isRotating;
        //private bool isPanning;
        //private bool isZooming;
        //private double autoRotate = 0.0;
        private double sensitivity = 0.322;
        internal Managed3D.SceneGraph.Scene scene;
        private bool isAutoRotateEnabled;
        private ManagedRenderer renderer;
        #endregion
        #region Constructors
        internal ManagedRendererHostControl(ManagedRenderer renderer)
        {
            this.Padding = new Padding(0, 0, 0, 0);
            this.Margin = new Padding(0, 0, 0, 0);
            this.renderer = renderer;
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

            if (Control.MouseButtons == MouseButtons.Left)
            {
                this.rx = e.X;
                this.ry = e.Y;
            }
            else if (Control.MouseButtons == MouseButtons.Right)
            {
                this.tx = e.X;
                this.ty = e.Y;
            }
            else if (Control.MouseButtons == (MouseButtons.Left | MouseButtons.Right))
            {
                this.sx = e.X;
                this.sy = e.Y;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (Control.MouseButtons == MouseButtons.Left)
            {
                var xdelta = e.X - this.rx;
                var ydelta = e.Y - this.ry;
                this.rx += xdelta;
                this.ry += ydelta;
                this.renderer.ActiveCamera.Orientation *= new Quaternion(Vector3.Right, Angle.FromDegrees(xdelta * sensitivity)) * new Quaternion(Vector3.Up, Angle.FromDegrees(ydelta * sensitivity));
                //var or = this.renderer.ActiveCamera.Orientation;

                //Console.WriteLine("cam-r: {0}", or.FindAngle());
            }
            else if (Control.MouseButtons == MouseButtons.Right)
            {
                var xdelta = e.X - this.tx;
                var ydelta = e.Y - this.ty;
                this.tx += xdelta;
                this.ty += ydelta;
                this.renderer.ActiveCamera.Position = (Vector3)this.renderer.ActiveCamera.Position + new Vector3(-xdelta * (this.sensitivity * 2), -ydelta * (this.sensitivity * 2), 0);
            }
            else if (Control.MouseButtons == (MouseButtons.Left | MouseButtons.Right))
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
            base.OnKeyDown(e);
            var cam = this.renderer.ActiveCamera;
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
                    cam.Facing = CameraFacing.Above;
                    cam.UpdateFacing();
                    break;

                case Keys.NumPad9:
                    cam.Facing = CameraFacing.Below;
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

                case Keys.Z:
                    //this.renderer.ZoomExtents(this.scene.Root.GetGraphExtents());
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
                var axis = this.renderer.ActiveCamera.Orientation.GetAxis();
                this.renderer.ActiveCamera.Orientation *= new Quaternion(axis, Angle.FromDegrees(0.5));
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }

        private unsafe void RendererPostRenderCallback(object sender, RenderEventArgs e)
        {

        }
        #endregion


    }
}
