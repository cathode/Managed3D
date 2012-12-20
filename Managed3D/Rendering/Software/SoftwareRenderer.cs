/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Managed3D.Geometry;
using Managed3D.SceneGraph;

namespace Managed3D.Rendering.Software
{
    /// <summary>
    /// Provides a reference 3D renderer implemented entirely in managed code.
    /// </summary>
    public class SoftwareRenderer : ManagedRenderer
    {
        #region Fields
        private Thread thread;
        private Matrix4 projectionMatrix;
        #endregion
        #region Methods
        protected override void OnInitializing(RendererInitializationEventArgs e)
        {
            base.OnInitializing(e);

            // Software rendering initialization code
        }

        protected override void OnPostRender(RenderEventArgs e)
        {
            base.OnPostRender(e);
        }

        protected override void OnPreRender(RenderEventArgs e)
        {
            base.OnPreRender(e);

            // Perform some necessary tasks prior to frame rendering
            var clearColor = this.BackgroundColor;
            this.AcquireBackBufferLock();
            this.BackBuffer.Color.Clear(clearColor);
            //this.BackBuffer.
            this.ReleaseBackBufferLock();
        }

        protected override void OnProfileChanged(EventArgs e)
        {
            base.OnProfileChanged(e);

            if (this.ActiveCamera == null)
                this.projectionMatrix = Matrix4.Identity;
            else
            {
                var buffer = this.BackBuffer;
                this.projectionMatrix =  Matrix4.CreateTranslationMatrix(buffer.Width / 2.0, buffer.Height / 2.0, 0);
                this.projectionMatrix *= (this.ActiveCamera.Mode == CameraMode.Perspective)
                    ? Matrix4.CreatePerspectiveProjectionMatrix(this.ActiveCamera.FieldOfView, (double)buffer.Width / buffer.Height, 0.1, 1000)
                    : Matrix4.CreateOrthographicProjectionMatrix(buffer.Width, buffer.Height, 0.1, 1000.0);
            }
        }

        protected override void OnRender(RenderEventArgs e)
        {
            base.OnRender(e);

            var camPos = this.ActiveCamera.Position;
            var camRot = this.ActiveCamera.Orientation;
            var camScale = this.ActiveCamera.Scale;

            var buffer = this.BackBuffer;
            var state = new SoftwareRendererState(this);

            // Set up the world matrix.
            var worldMatrix = Matrix4.Identity;

            // Set up the view matrix.
            state.Translate(-camPos.X, -camPos.Y, -camPos.Z);
            state.Scale(camScale.X, camScale.Y, camScale.Z);
            state.Rotate(camRot);
            state.ProjectionMatrix = this.projectionMatrix;

            SoftwareRendererState.GlobalState = state;
            this.AcquireBackBufferLock();

            this.RenderNode(this.Scene.Root);

            // Rendering complete.
            this.ReleaseBackBufferLock();
        }

        protected override void OnStarting(EventArgs e)
        {
            base.OnStarting(e);
        }

        protected override void OnStopping(EventArgs e)
        {
            base.OnStopping(e);
        }

        private void DrawLine(Vertex3 a, Vertex3 b)
        {
            var xdiff = b.X - a.X;
            var ydiff = b.Y - a.Y;

            if (xdiff == 0.0 && ydiff == 0.0)
                return;

            var cp = this.BackBuffer.Color;

            if (Math.Abs(xdiff) > Math.Abs(ydiff))
            {
                double xmin, xmax;

                if (a.X < b.X)
                {
                    xmin = a.X;
                    xmax = b.X;
                }
                else
                {
                    xmin = b.X;
                    xmax = a.X;
                }

                xmin = xmin < 0 ? 0 : xmin;
                xmax = xmax >= this.BackBuffer.Width ? this.BackBuffer.Width - 1 : xmax;

                double slope = ydiff / xdiff;

                for (double x = xmin; x <= xmax; ++x)
                {
                    double y = a.Y + ((x - a.X) * slope);
                    y = y < 0 ? 0 : y;
                    y = y >= this.BackBuffer.Height ? this.BackBuffer.Height - 1 : y;
                    var color = a.Color + ((b.Color - a.Color) * ((x - a.X) / xdiff));
                    cp[(int)x, (int)y] = color;
                }
            }
            else
            {
                double ymin, ymax;

                if (a.Y < b.Y)
                {
                    ymin = a.Y;
                    ymax = b.Y;
                }
                else
                {
                    ymin = b.Y;
                    ymax = a.Y;
                }

                double slope = xdiff / ydiff;

                ymin = ymin < 0 ? 0 : ymin;
                ymax = ymax >= this.BackBuffer.Height ? this.BackBuffer.Height - 1 : ymax;

                for (double y = ymin; y <= ymax; ++y)
                {
                    double x = a.X + ((y - a.Y) * slope);
                    x = x < 0 ? 0 : x;
                    x = x >= this.BackBuffer.Width ? this.BackBuffer.Width - 1 : x;
                    var color = a.Color + ((b.Color - a.Color) * ((y - a.Y) / ydiff));
                    cp[(int)x, (int)y] = color;
                }
            }
        }

        private void DrawTriangle(Vertex3 p1, Vertex3 p2, Vertex3 p3)
        {
            // draw the triangle

            // draw edges of triangle
            this.DrawLine(p1, p2);
            this.DrawLine(p2, p3);
            this.DrawLine(p3, p1);
        }

        private void RenderNode(Node node)
        {
            var state = SoftwareRendererState.GlobalState;

            state.PushMatrix();

            state.Translate(node.Position);
            state.Rotate(node.Orientation);
            state.Scale(node.Scale);

            // Traverse the graph starting at the current node, render them first.
            // AKA, depth-first search
            if (node.Count > 0)
                foreach (var child in node)
                    this.RenderNode(child);

            foreach (var renderable in node.Renderables)
            {
                foreach (var poly in renderable)
                {
                    var verts = new Vertex3[poly.Vertices.Length];
                    for (int i = 0; i < verts.Length; ++i)
                    {
                        // transform and project vertices.
                        verts[i] = state.Transform(poly.Vertices[i]);

                    }
                    if (verts.Length == 3)
                    {
                        this.DrawTriangle(verts[0], verts[1], verts[2]);
                    }
                    else if (verts.Length == 4)
                    {
                        this.DrawTriangle(verts[0], verts[1], verts[2]);
                        this.DrawTriangle(verts[2], verts[3], verts[0]);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }

            state.PopMatrix();
        }
        #endregion
    }

    /// <summary>
    /// Represents state information used for rendering a frame.
    /// </summary>
    public sealed class SoftwareRendererState
    {
        #region Fields
        internal static SoftwareRendererState GlobalState;

        private readonly SoftwareRenderer renderer;
        private Matrix4 worldView;
        private Stack<Matrix4> matrices = new Stack<Matrix4>();
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SoftwareRendererState"/> class.
        /// </summary>
        /// <param name="renderer">The <see cref="SoftwareRenderer"/> that this instance represents state for.</param>
        public SoftwareRendererState(SoftwareRenderer renderer)
        {
            this.renderer = renderer;
            this.worldView = Matrix4.Identity;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets a <see cref="Matrix4"/> that is a combined world and view transformation matrix.
        /// </summary>
        public Matrix4 WorldViewMatrix
        {
            get
            {
                return this.worldView;
            }
        }

        /// <summary>
        /// Gets a <see cref="Matrix4"/> that is a projection transformation matrix.
        /// </summary>
        public Matrix4 ProjectionMatrix
        {
            get;
            set;
        }
        #endregion
        #region Methods
        public void PushMatrix()
        {
            // make copies of current matrix.
            var newWvp = this.worldView * Matrix4.Identity;

            this.matrices.Push(this.worldView);

            this.worldView = newWvp;
        }

        public void PopMatrix()
        {
            if (this.matrices.Count > 0)
                this.worldView = this.matrices.Pop();
        }

        public Vertex3 Transform(Vertex3 input)
        {
            return this.ProjectionMatrix * (this.worldView * input);
        }

        public void Translate(Vector3 translation)
        {
            this.worldView = this.worldView * Matrix4.CreateTranslationMatrix(translation);
        }

        public void Translate(double x, double y, double z)
        {
            this.Translate(new Vector3(x, y, z));
        }

        public void Scale(Vector3 scaling)
        {
            this.worldView *= Matrix4.CreateScalingMatrix(scaling);
        }

        public void Scale(double x, double y, double z)
        {
            this.Scale(new Vector3(x, y, z));
        }

        public void Rotate(Quaternion rotation)
        {
            this.worldView *= Matrix4.CreateRotationMatrix(rotation);
        }
        #endregion
    }
}