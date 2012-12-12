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
        private Form hostForm;
        private Thread thread;
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
        }

        protected override void OnRender(RenderEventArgs e)
        {
            base.OnRender(e);

            var camPos = this.ActiveCamera.Position;
            var camRot = this.ActiveCamera.Orientation;
            var camScale = this.ActiveCamera.Scale;

            var buffer = this.BackBuffer;
            var matrix = Matrix4.Identity;

            // Set up the world matrix.
            var world = Matrix4.Identity;

            // Set up the view matrix.
            var view = Matrix4.CreateTranslationMatrix(-camPos.X, -camPos.Y, -camPos.Z) * Matrix4.CreateRotationMatrix(-camRot.X, -camRot.Y, -camRot.Z) * Matrix4.CreateScalingMatrix(camScale.X, camScale.Y, camScale.Z);


            // Set up the projection matrix.
            Matrix4 projection;
            if ((this.ActiveCamera.Mode == CameraMode.Isometric) || (this.ActiveCamera.Mode == CameraMode.Orthographic))
            {
                //this.Scene.ActiveCamera.Orientation = new Vector3(45, 35.264, 0);
                projection = Matrix4.CreateOrthographicProjectionMatrix(buffer.Width, buffer.Height, 0.1, 1000.0);
            }
            else
            {
                projection = Matrix4.CreatePerspectiveProjectionMatrix(this.ActiveCamera.FieldOfView, (double)buffer.Width / buffer.Height, 0.1, 1000);
            }

            // Pulls the x/y coords into the center of the screen.
            projection *= Matrix4.CreateTranslationMatrix(buffer.Width / 2.0, buffer.Height / 2.0, 0);



            var state = new RenderState();

            state.ProjectionMatrix = projection;
            state.ViewMatrix = view;
            state.WorldMatrix = world;

            this.AcquireBackBufferLock();

            this.RenderNode(state, this.Scene.Root);

            // Rendering complete.
            this.ReleaseBackBufferLock();
        }

        internal class RenderState
        {
            internal Matrix4 WorldMatrix;
            internal Matrix4 ViewMatrix;
            internal Matrix4 ProjectionMatrix;

            public RenderState Clone()
            {
                return new RenderState()
                {
                    WorldMatrix = this.WorldMatrix,
                    ViewMatrix = this.ViewMatrix,
                    ProjectionMatrix = this.ProjectionMatrix,
                };
            }
        }

        private void RenderNode(RenderState state, Node node)
        {
            var newState = new RenderState();
            newState.WorldMatrix = Matrix4.CreateRotationMatrix(node.Orientation) * Matrix4.CreateTranslationMatrix(node.Position) * Matrix4.CreateScalingMatrix(node.Scale) * state.WorldMatrix;
            newState.ViewMatrix = state.ViewMatrix;
            newState.ProjectionMatrix = state.ProjectionMatrix;

            if (node.Count > 0)
            {
                foreach (var child in node)
                    this.RenderNode(newState, child);
            }

            var world = newState.WorldMatrix;
            var view = newState.ViewMatrix;
            var proj = newState.ProjectionMatrix;

            var wvp = proj * view * world;
            var wv = view * world;

            var cameraUp = new Vector3(0, 0, 1);

            if (node is GeometryNode && ((GeometryNode)node).Geometry != null)
            {
                
                var mesh = ((GeometryNode)node).Geometry;
                foreach (var poly in mesh.Polygons)
                {
                    if (poly == null)
                        continue;

                    // Cull backfacing polygons.
                    if (true) // TODO: Allow per-mesh or per-poly control over whether backfacing polys are culled or not.
                    {
                        var p1 = wv * poly.Vertices[0];
                        var p2 = wv * poly.Vertices[1];
                        var p3 = wv * poly.Vertices[2];
                        var faceNormal = Vector3.CrossProduct((Vector3)p3 - (Vector3)p2, (Vector3)p3 - (Vector3)p1);

                        faceNormal = faceNormal.Normalize();

                        var dp = Vector3.DotProduct(cameraUp, faceNormal);
                        if (dp > 0)
                            continue;
                    }

                    
                    for (int i = 0; i < poly.Vertices.Length; ++i)
                    {
                        if (i == (poly.Vertices.Length - 1))
                        {
                            var a = wvp * poly.Vertices[i];
                            var b = wvp * poly.Vertices[0];
                            this.DrawLine(a, b);
                        }
                        else
                        {
                            var a = wvp * poly.Vertices[i];
                            var b = wvp * poly.Vertices[i + 1];
                            this.DrawLine(a, b);
                        }
                    }
                }
            }

            
        }

        protected override void OnStarting(EventArgs e)
        {
            base.OnStarting(e);

            this.hostForm = new ManagedRendererHostForm(this);

            this.thread = new Thread(this.RunForm);

            this.thread.Start();
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

        private void RunForm()
        {
            Application.Run(this.hostForm);
        }
        #endregion
    }
}