/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Managed3D.Geometry;
using Managed3D.SceneGraph;

namespace Managed3D.Rendering.Software
{
    /// <summary>
    /// Provides a reference 3D renderer implemented entirely in managed code.
    /// </summary>
    [ContractVerification(false)]
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

                // Set up projection matrix for new profile.
                if (this.ActiveCamera.Mode == CameraMode.Perspective)
                    this.projectionMatrix *= Matrix4.CreatePerspectiveProjectionMatrix(this.ActiveCamera.FieldOfView, (double)buffer.Width / buffer.Height, 0.1, 1000);
                else
                    Matrix4.CreateOrthographicProjectionMatrix(buffer.Width, buffer.Height, 0.1, 1000.0);
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

        private void DrawEdge(Edge3 edge)
        {
            if (edge.Flags.HasFlag(EdgeFlags.Invisible))
                return;
            else
            {
                var state = SoftwareRendererState.GlobalState;
                var p = state.Transform(edge.P);
                var q = state.Transform(edge.Q);

                this.DrawLine(p.Position, p.Color, q.Position, q.Color);
            }
        }

        private void DrawLine(Vector3 v1, Vector4f c1, Vector3 v2, Vector4f c2)
        {
            var xdiff = v2.X - v1.X;
            var ydiff = v2.Y - v1.Y;

            if (xdiff == 0.0 && ydiff == 0.0)
                return;

            var cp = this.BackBuffer.Color;

            if (Math.Abs(xdiff) > Math.Abs(ydiff))
            {
                double xmin, xmax;

                if (v1.X < v2.X)
                {
                    xmin = v1.X;
                    xmax = v2.X;
                }
                else
                {
                    xmin = v2.X;
                    xmax = v1.X;
                }

                xmin = xmin < 0 ? 0 : xmin;
                xmax = xmax >= this.BackBuffer.Width ? this.BackBuffer.Width - 1 : xmax;

                double slope = ydiff / xdiff;

                for (double x = xmin; x <= xmax; ++x)
                {
                    double y = v1.Y + ((x - v1.X) * slope);
                    y = y < 0 ? 0 : y;
                    y = y >= this.BackBuffer.Height ? this.BackBuffer.Height - 1 : y;
                    var color = c1 + ((c2 - c1) * ((x - v1.X) / xdiff));
                    cp[(int)x, (int)y] = color;
                }
            }
            else
            {
                double ymin, ymax;

                if (v1.Y < v2.Y)
                {
                    ymin = v1.Y;
                    ymax = v2.Y;
                }
                else
                {
                    ymin = v2.Y;
                    ymax = v1.Y;
                }

                double slope = xdiff / ydiff;

                ymin = ymin < 0 ? 0 : ymin;
                ymax = ymax >= this.BackBuffer.Height ? this.BackBuffer.Height - 1 : ymax;

                for (double y = ymin; y <= ymax; ++y)
                {
                    double x = v1.X + ((y - v1.Y) * slope);
                    x = x < 0 ? 0 : x;
                    x = x >= this.BackBuffer.Width ? this.BackBuffer.Width - 1 : x;
                    var color = c1 + ((c2 - c1) * ((y - v1.Y) / ydiff));
                    cp[(int)x, (int)y] = color;
                }
            }
        }

        private void DrawSpan(Edge3 e1, Edge3 e2)
        {
            var cp = this.BackBuffer.Color;
            var dp = this.BackBuffer.Depth;

            var e1yd = e1.Q.Y - e1.P.Y;
            if (e1yd < 0.00001 && e1yd > -0.00001)
                return;

            var e2yd = e2.Q.Y - e2.P.Y;
            if (e2yd < 0.00001 && e2yd > -0.00001)
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

        private void DrawTriangle(Triangle3 triangle)
        {
            var p1 = triangle[0];
            var p2 = triangle[1];
            var p3 = triangle[2];

            // -----------------
            // Draw the triangle
            // -----------------

            // Find the vertex with the middle y value.
            var ps = new Vertex3[] { p1, p2, p3 };
            ps = ps.OrderBy(p => p.Y).ToArray();

            var ystart = (int)ps[0].Y;
            var ymid = (int)ps[1].Y;
            var yend = (int)ps[2].Y;

            var e1 = triangle.Edges[0];
            var e2 = new Edge3(ps[0], ps[2]);
            var e3 = new Edge3(ps[1], ps[2]);

            if (ymid > ystart)
            {

            }

            if (yend > ymid)
            {

            }
        }

        private void DrawQuad(Quad3 quad)
        {
            var emid = new Edge3(quad.A, quad.C);
        }

        private void DrawPolygon(Polygon3 poly)
        {
            if (poly is Triangle3)
                this.DrawTriangle((Triangle3)poly);
            else if (poly is Quad3)
                this.DrawQuad((Quad3)poly);
            else
            {
                // NOP
            }

            if (true)
            {
                foreach (var edge in poly.Edges)
                    this.DrawEdge(edge);
            }
        }

        private void RenderNode(Node node)
        {
            // If the node is not visible, then all of it's child nodes are invisible too.
            // Abort rendering of the current node immediately if that's the case.
            if (!this.ActiveCamera.VisibleGroups.HasFlag(node.Visibility))
                return;

            var state = SoftwareRendererState.GlobalState;
            state.PushMatrix();

            if (node.TransformOrder == TransformOrder.RotateTranslateScale)
            {
                state.Rotate(node.Orientation);
                state.Translate(node.Position);
                state.Scale(node.Scale);
            }
            else if (node.TransformOrder == TransformOrder.TranslateRotateScale)
            {
                state.Translate(node.Position);
                state.Rotate(node.Orientation);
                state.Scale(node.Scale);
            }


            // Traverse the graph starting at the current node, render them first.
            // AKA, depth-first search
            if (node.Count > 0)
                foreach (var child in node)
                    this.RenderNode(child);

            foreach (var renderable in node.Renderables)
            {
                foreach (var poly in renderable)
                {
                    if (poly == null)
                        continue;
                    else
                        this.DrawPolygon(poly);
                }
            }

            if (node is SpriteNode)
            {
                var v = state.Transform(new Vertex3(node.Position.X, node.Position.Y, node.Position.Z), ReferenceSpace.Object);

                if (!double.IsInfinity(v.X) && !double.IsInfinity(v.Y) && !double.IsInfinity(v.Z)
                    && !double.IsNaN(v.X) && !double.IsNaN(v.Y) && !double.IsNaN(v.Z))
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

        public override void ZoomExtents(Extents3 extents)
        {

            this.ActiveCamera.Position = extents.FindMidpoint();
            var m = this.projectionMatrix;
            var v1 = m * extents.A;
            var v2 = m * extents.B;
            double h = this.Profile.Height;
            double w = this.Profile.Width;
            var f = ((v1.Y - v2.Y) / 100.0) * (100 / (h / w));

            this.ActiveCamera.Scale = new Vector3(f * 0.9, f * 0.9, f * 0.9);

        }
        #endregion
        #region Types
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
        #endregion
    }
}