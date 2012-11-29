/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using Managed3D.Geometry;
using Managed3D.Rendering;
using Managed3D.Rendering.OpenGL;
using Managed3D.Rendering.Software;
using Managed3D.SceneGraph;
using Managed3D.Rendering.Direct3D;
using Managed3D.Geometry.Primitives;

namespace ManagedStudio3D
{
    internal static class Program
    {
        #region Fields
        internal static Scene scene;
        #endregion
        #region Methods
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        internal static void Main()
        {
            Console.WriteLine("Starting ManagedStudio3D...");

            Program.scene = new Scene();
           
            // scene.ActiveCamera.Orientation = new Managed3D.Geometry.Rotation3(Angle.FromDegrees(10), new Vector3(0, 0, 1));

            var root = new GeometryNode();
            root.Geometry = new Managed3D.Geometry.Primitives.Cube(60);
            scene.DefaultCamera = Camera.CreateIsometric();

            
            //scene.Root.Orientation = new Vector3(-35.264, -45, 90);
            scene.Root.Scale = new Vector3(1.5, 1.5, 1.5);
            root.Add(new GeometryNode(new Managed3D.Geometry.Primitives.Cube(40))
            {
                Position = new Vector3(150, 0, 0),
                Orientation = new Vector3(20,0,0),
            });
            root.Add(new GeometryNode(new Managed3D.Geometry.Primitives.Icosahedron(55))
            {
                Position = new Vector3(35, 90, 0)
            });

            root = new GeometryNode(new MengerSponge(60, 1));
            Program.scene.Root = root;

            //Program.RunDirect3DMode();
            Program.RunSoftwareMode();
            //Program.RunOpenGLMode();
        }

        private static void RunDirect3DMode()
        {
            var renderer = new D3DRenderer()
            {
                // Same eye-pleasing grey used as the default viewport background by 3DS Max 2011.
                BackgroundColor = new Vector4f(0.11764705882f, 0.11764705882f, 0.11764705882f, 1.0f),
            };

            var options = new RendererOptions();
            options.Profile = Managed3D.Platform.DisplayProfile.Generic720p;

            renderer.Scene = scene;
            renderer.Initialize(options);
            renderer.Start();
        }

        private static void RunOpenGLMode()
        {
            var renderer = new GLRenderer();
            var options = new RendererOptions();
            renderer.Scene = scene;
            renderer.Initialize(options);
            renderer.Start();
        }
        private static void RunSoftwareMode()
        {
            var renderer = new SoftwareRenderer()
            {
                // Same eye-pleasing grey used as the default viewport background by 3DS Max 2011.
                BackgroundColor = new Vector4f(0.11764705882f, 0.11764705882f, 0.11764705882f, 1.0f),
            };
            var options = new RendererOptions();
            renderer.Scene = Program.scene;
            renderer.Initialize(options);
            renderer.Profile = Managed3D.Platform.DisplayProfile.GenericVGA;
            renderer.ActiveCamera = Camera.CreateIsometric();
            renderer.Start();
        }

        #endregion
    }
}
