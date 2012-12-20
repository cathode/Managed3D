﻿/******************************************************************************
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
using System.Threading;

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

            var root = new GeometryNode();
            root.Renderables.Add(new Cube(75));

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
            renderer.ActiveCamera = Camera.CreateIsometric();
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
            //renderer.ActiveCamera = Camera.CreateIsometric();
            renderer.Initialize(options);
            renderer.Profile = Managed3D.Platform.DisplayProfile.GenericVGA;
           
            var target = new ManagedRendererHostForm(renderer);
            target.Load += delegate
            {
                new Thread((ThreadStart)delegate
                {
                    renderer.AttachTarget(target);
                    renderer.Start();
                }).Start();
            };
            System.Windows.Forms.Application.Run(target);
        }
        #endregion
    }
}
