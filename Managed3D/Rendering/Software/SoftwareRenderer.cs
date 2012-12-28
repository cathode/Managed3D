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
using System.Linq;

namespace Managed3D.Rendering.Software
{
    /// <summary>
    /// Provides a reference 3D renderer implemented entirely in managed code.
    /// </summary>
    public class SoftwareRenderer : ManagedRenderer
    {
        #region Fields
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
                this.projectionMatrix = Matrix4.CreateTranslationMatrix(buffer.Width / 2.0, buffer.Height / 2.0, 0);
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
            state.Scale(-camScale.X, -camScale.Y, -camScale.Z);
            state.Translate(-camPos.X, -camPos.Y, -camPos.Z);
            state.Rotate(camRot);

            // Set up the projection matrix.
            state.SetProjectionMatrix(this.projectionMatrix);

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

        private class Span
        {
            #region Fields
            internal double X1;
            internal double X2;
            internal Vector4f C1;
            internal Vector4f C2;
            #endregion
            internal Span(Vector4f c1, double x1, Vector4f c2, double x2)
            {
                this.C1 = c1;
                this.C2 = c2;
                this.X1 = x1;
                this.X2 = x2;
            }
        }

        private void DrawSpan(Edge3 e1, Edge3 e2)
        {
            var cp = this.BackBuffer.Color;
            var dp = this.BackBuffer.Depth;

            var e1yd = e1.Q.Y - e1.P.Y;
            if (e1yd == 0.0)
                return;

            var e2yd = e2.Q.Y - e2.P.Y;
            if (e2yd == 0.0)
                return;

            var e1xd = e1.Q.X - e1.P.X;
            var e2xd = e2.Q.X - e2.P.X;

            var e1colord = (e1.Q.Color - e1.P.Color);
            var e2colord = (e2.Q.Color - e2.P.Color);

            var f1 = (e2.P.Y - e1.P.Y) / e1yd;
            var f1step = 1.0 / e1yd;
            var f2 = 0.0;
            var f2step = 1.0 / e2yd;

            for (int y = (int)e2.P.Y; y < e2.Q.Y; y++)
            {
                var span = new Span(e1.P.Color + (e1colord * f1), e1.P.X + (e1xd * f1), e2.P.Color + (e2colord * f2), e2.P.X + (e2xd * f2));

                var xd = span.X2 - span.X1;
                if (xd == 0.0)
                    continue;

                var cd = span.C2 - span.C1;

                var sf = 0.0;
                var sfstep = 1.0 / xd;
                for (int x = (int)span.X1; x < span.X2; x++)
                {
                    if (x > 0 && x < this.BackBuffer.Width && y > 0 && y < this.BackBuffer.Height)
                    {
                        cp[x, y] = span.C1 + (cd * sf);
                    }

                    sf += sfstep;
                }

                f1 += f1step;
                f2 += f2step;
            }

        }

        private void DrawTriangle(Vertex3 p1, Vertex3 p2, Vertex3 p3)
        {
            // draw the triangle
            var edges = (from e in new Edge3[] { new Edge3(p1, p2), new Edge3(p2, p3), new Edge3(p3, p1) }
                         let length = Math.Abs(e.Q.Y - e.P.Y)
                         orderby length
                         select e).ToArray();

            //this.DrawSpan(edges[0], edges[1]);
            //this.DrawSpan(edges[0], edges[2]);

            // draw edges of triangle, but check if two neighboring vertices are set to hide their outgoing edges we skip drawing that line.
            if (!p1.Flags.HasFlag(VertexFlags.HideOutgoingEdges) || !p2.Flags.HasFlag(VertexFlags.HideOutgoingEdges))
                this.DrawLine(p1, p2);
            if (!p2.Flags.HasFlag(VertexFlags.HideOutgoingEdges) || !p3.Flags.HasFlag(VertexFlags.HideOutgoingEdges))
                this.DrawLine(p2, p3);
            if (!p3.Flags.HasFlag(VertexFlags.HideOutgoingEdges) || !p1.Flags.HasFlag(VertexFlags.HideOutgoingEdges))
                this.DrawLine(p3, p1);
        }

        private void RenderNode(Node node)
        {
            // If the node is not visible, then all of it's child nodes are invisible too.
            // Abort rendering of the current node immediately if that's the case.
            if (!this.ActiveCamera.VisibleGroups.HasFlag(node.Visibility))
                return;

            var state = SoftwareRendererState.GlobalState;
            state.PushMatrix();

            state.Scale(node.Scale);
            state.Rotate(node.Orientation);
            state.Translate(node.Position);
            

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
                        verts[0].Flags |= VertexFlags.HideOutgoingEdges;
                        verts[2].Flags |= VertexFlags.HideOutgoingEdges;

                        this.DrawTriangle(verts[0], verts[1], verts[2]);
                        this.DrawTriangle(verts[2], verts[3], verts[0]);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }

            if (node is SpriteNode)
            {
                var v = state.Transform(new Vertex3(node.Position.X, node.Position.Y, node.Position.Z), ReferenceSpace.Object);

                if (!double.IsInfinity(v.X) && !double.IsInfinity(v.Y) && !double.IsInfinity(v.Z))
                {
                    var spr = node as SpriteNode;
                    var buffer = this.BackBuffer;
                    var cp = this.BackBuffer.Color;
                    var pix = spr.Bitmap;

                    var stride = spr.BitmapSize.X;

                    var x1 = (int)(v.X - (spr.BitmapSize.X / 2.0));
                    var y1 = (int)(v.Y - (spr.BitmapSize.Y / 2.0));
                    var x2 = x1 + spr.BitmapSize.X;
                    var y2 = y1 + spr.BitmapSize.Y;

                    for (int y = y1, yp = 0; y < y2; ++y, ++yp)
                    {
                        if (y < 0 || y >= buffer.Height)
                            continue;

                        for (int x = x1, xp = 0; x < x2; ++x, ++xp)
                        {
                            if (x < 0 || x >= buffer.Width)
                                continue;

                            var cpc = cp[x, y];
                            var pc = pix[(yp * stride) + xp];

                            var cx = (cpc.X * (1.0f - pc.W)) + (pc.X * pc.W);
                            var cy = (cpc.Y * (1.0f - pc.W)) + (pc.Y * pc.W);
                            var cz = (cpc.Z * (1.0f - pc.W)) + (pc.Z * pc.W);
                            var cw = 1.0f;

                            cp[x, y] = new Vector4f(cx, cy, cz, cw);
                        }
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
        private Stack<Matrix4> worldMatrices = new Stack<Matrix4>();
        private Stack<Matrix4> viewMatrices = new Stack<Matrix4>();
        private Stack<Matrix4> projectionMatrices = new Stack<Matrix4>();
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SoftwareRendererState"/> class.
        /// </summary>
        /// <param name="renderer">The <see cref="SoftwareRenderer"/> that this instance represents state for.</param>
        public SoftwareRendererState(SoftwareRenderer renderer)
        {
            this.renderer = renderer;

            this.WorldMatrix = Matrix4.Identity;
            this.ViewMatrix = Matrix4.Identity;
            this.ProjectionMatrix = Matrix4.Identity;

            this.UpdateDerivedMatrices();
        }
        #endregion
        #region Properties
        public Matrix4 WorldMatrix
        {
            get;
            private set;
        }

        public Matrix4 ViewMatrix
        {
            get;
            private set;
        }

        public Matrix4 WorldViewProjection
        {
            get;
            private set;
        }

        public Matrix4 ViewProjection
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets a <see cref="Matrix4"/> that is a projection transformation matrix.
        /// </summary>
        public Matrix4 ProjectionMatrix
        {
            get;
            private set;
        }
        #endregion
        #region Methods
        public void PushMatrix()
        {
            this.worldMatrices.Push(this.WorldMatrix);
            this.WorldMatrix *= Matrix4.Identity; // New instance
            this.viewMatrices.Push(this.ViewMatrix);
            this.ViewMatrix *= Matrix4.Identity;
            this.projectionMatrices.Push(this.ProjectionMatrix);
            this.ProjectionMatrix *= Matrix4.Identity;

            this.UpdateDerivedMatrices();
        }

        public void PopMatrix()
        {
            if (this.worldMatrices.Count > 0)
            {
                this.WorldMatrix = this.worldMatrices.Pop();
                this.ViewMatrix = this.viewMatrices.Pop();
                this.ProjectionMatrix = this.projectionMatrices.Pop();
            }

            this.UpdateDerivedMatrices();
        }

        public Vertex3 Transform(Vertex3 input)
        {
            return this.WorldViewProjection * input;
        }

        public Vertex3 Transform(Vertex3 input, ReferenceSpace space)
        {
            // Allow selective transformation based on the scope of the vertex input.
            if (space == ReferenceSpace.Object)
                return this.WorldViewProjection * input;
            else if (space == ReferenceSpace.World)
                return this.ViewProjection * input;
            else if (space == ReferenceSpace.View)
                return this.ProjectionMatrix * input;
            else
                return input;
        }

        public void Translate(Vector3 translation)
        {
            this.WorldMatrix *= Matrix4.CreateTranslationMatrix(translation);

            this.UpdateDerivedMatrices();
        }

        public void Translate(double x, double y, double z)
        {
            this.Translate(new Vector3(x, y, z));
        }

        public void Scale(Vector3 scaling)
        {
            this.WorldMatrix *= Matrix4.CreateScalingMatrix(scaling);

            this.UpdateDerivedMatrices();
        }

        public void Scale(double x, double y, double z)
        {
            this.Scale(new Vector3(x, y, z));
        }

        public void Rotate(Quaternion rotation)
        {
            this.WorldMatrix *= Matrix4.CreateRotationMatrix(rotation);

            this.UpdateDerivedMatrices();
        }

        /// <summary>
        /// Sets the projection matrix for the render state.
        /// </summary>
        /// <param name="projectionMatrix"></param>
        public void SetProjectionMatrix(Matrix4 projectionMatrix)
        {
            this.ProjectionMatrix = projectionMatrix;

            this.UpdateDerivedMatrices();
        }

        private void UpdateDerivedMatrices()
        {
            this.ViewProjection = this.ProjectionMatrix * Matrix4.Identity;
            this.ViewProjection *= this.ViewMatrix;
            this.WorldViewProjection = this.ProjectionMatrix * Matrix4.Identity;
            this.WorldViewProjection *= this.ViewMatrix;
            this.WorldViewProjection *= this.WorldMatrix;
        }
        #endregion
    }
}