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
using Managed3D.SceneGraph;
using Managed3D.Geometry;
//using Managed3D.Platform.Microsoft;
//using Managed3D.Platform.Microsoft.OpenGL;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;

namespace Managed3D.Rendering.OpenGL
{
    /// <summary>
    /// Provides a real-time renderer that utilizes the OpenGL API.
    /// </summary>
    public class GLRenderer : Renderer, IDisposable
    {
        #region Fields
        private bool isDisposed;
        private OpenTK.GameWindow gameWindow;

        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GLRenderer"/> class.
        /// </summary>
        public GLRenderer()
        {
            var gw = new GameWindow();
            this.gameWindow = gw;

            gw.Load += (sender, e) =>
            {
                gw.VSync = VSyncMode.On;
            };

            gw.Resize += (sender, e) =>
                {
                    GL.Viewport(0, 0, gw.Width, gw.Height);
                };

            gw.UpdateFrame += (sender, e) =>
                {
                    // add game logic, input handling
                    if (gw.Keyboard[Key.Escape])
                    {
                        gw.Exit();
                    }
                };

            gw.RenderFrame += (sender, e) =>
                {
                    this.RenderFrame();
                };
        }

        ~GLRenderer()
        {
            this.Dispose(false);
        }
        #endregion
        #region Properties
        public bool IsDisposed
        {
            get
            {
                return this.isDisposed;
            }
        }
        #endregion
        #region Methods
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
            this.isDisposed = true;
        }

        protected virtual void Dispose(bool disposing)
        {
            //WGL.MakeCurrent(this.deviceContext, IntPtr.Zero);
            //WGL.DeleteContext(this.renderingContext);
        }

        public override void Start()
        {
            this.Initialize(RendererOptions.Empty);


            this.gameWindow.Closed += gameWindow_Closed;
            this.gameWindow.Run(60.0);
        }

        void gameWindow_Closed(object sender, EventArgs e)
        {
            this.Stop();

            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        protected override void OnInitializing(RendererInitializationEventArgs e)
        {
            base.OnInitializing(e);

            gameWindow.Title = "Managed3D Sample Application";

            float[] mat_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] mat_shininess = { 50.0f };
            float[] light_position = { 100.0f, 100.0f, 100.0f, 0.0f };
            float[] light_ambient = { 0.1f, 0.1f, 0.1f, 1.0f };

            GL.ShadeModel(ShadingModel.Smooth);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, mat_specular);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, mat_shininess);
            GL.Light(LightName.Light0, LightParameter.Position, light_position);
            GL.Light(LightName.Light0, LightParameter.Ambient, light_ambient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, mat_specular);


            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.CullFace);



            //GL.Translate(0, 0, -0.25);
            //GL.Scale(0.5, 0.5, 0.5);
        }

        private void Idle()
        {

        }

        protected override void OnSceneChanged(EventArgs e)
        {
            base.OnSceneChanged(e);
            var clamped = this.BackgroundColor.Clamp();
            //GL.ClearColor((float)clamped.X,
            //              (float)clamped.Y,
            //              (float)clamped.Z,
            //              (float)clamped.W);
        }

        protected override void OnPreRender(RenderEventArgs e)
        {
            base.OnPreRender(e);

            //GL.Clear(GL.GL_COLOR_BUFFER_BIT);
            //GL.LoadIdentity();
        }

        double r = 10.0;

        protected override void OnRender(RenderEventArgs e)
        {
            base.OnRender(e);

            // render graphics
            GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);



            /*
            GL.Begin(BeginMode.Quads);
            // Front Face
            GL.Normal3(0.0f, 0.0f, 0.5f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
            // Back Face
            GL.Normal3(0.0f, 0.0f, -0.5f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
            // Top Face
            GL.Normal3(0.0f, 0.5f, 0.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
            // Bottom Face
            GL.Normal3(0.0f, -0.5f, 0.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            // Right Face
            GL.Normal3(0.5f, 0.0f, 0.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 1.0f);
            // Left Face
            GL.Normal3(-0.5f, 0.0f, 0.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.End();*/


            //GL.sp
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.Scale(0.01, 0.01, 0.01);

            var ct = this.ActiveCamera.Position;

            GL.Translate(-ct.X, -ct.Y, -ct.Z);

            var rt = this.ActiveCamera.Orientation;

            this.ActiveCamera.Orientation = rt.RotateBy(1.5);
            GL.Rotate(rt.W, rt.X, rt.Y, rt.Z);

            this.ProcessNode(this.Scene.Root);

            gameWindow.SwapBuffers();

        }

        protected override void OnProfileChanged(EventArgs e)
        {
            base.OnProfileChanged(e);

            //GL.Viewport(0, 0, this.Profile.Width, this.Profile.Height);
            //GL.MatrixMode(MatrixMode.Projection);

            //GLU.Perspective(45.0, (double)this.Profile.Width / (double)this.Profile.Height, 0.1, 100.0);
            //GL.MatrixMode(MatrixMode.ModelView);
            //GL.LoadIdentity();
        }

        protected override void OnPostRender(RenderEventArgs e)
        {
            base.OnPostRender(e);


        }

        protected virtual void ProcessNode(Node node)
        {
            var axis = node.Orientation.GetAxis();
            GL.PushMatrix();
            GL.Rotate(node.Orientation.GetAngle().Degrees, axis.X, axis.Y, axis.Z);
            GL.Translate(node.Position.X, node.Position.Y, node.Position.Z);



            foreach (var poly in node.Renderable.Faces)
            {
                //GL.Begin(PrimitiveType.Polygon);
                //var n = poly.Normal;

                //GL.Normal3(n.X, n.Y, n.Z);

                //foreach (var vert in poly.Vertices.Reverse())
                 //   GL.Vertex3(vert.X, vert.Y, vert.Z);

                //GL.End();
            }


            foreach (var child in node.Children)
            {
                this.ProcessNode(child);
            }
            GL.PopMatrix();

        }

        private void IdleFunc_Callback()
        {
            //GLUT.PostRedisplay();
            return;
        }

        private void DisplayFunc_Callback()
        {
            this.RenderFrame();
            return;
        }

        #endregion
    }
}
