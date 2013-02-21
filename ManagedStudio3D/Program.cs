/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using Managed3D.Geometry;
using Managed3D.Geometry.Primitives;
using Managed3D.Rendering;
using Managed3D.Rendering.Direct3D;
using Managed3D.Rendering.OpenGL;
using Managed3D.Rendering.Software;
using Managed3D.SceneGraph;

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

            var c1 = new Cube(50);
            c1.Polygons[0].Edges[0].Flags = EdgeFlags.Invisible;

            var root = new Node(
                new Node(c1)
                {
                    Position = new Vector3(150, 0, 0),
                    Visibility = VisibilityGroup.G0,
                    TransformOrder = TransformOrder.RotateTranslateScale,
                },
                new Node(new Cube(50))
                {
                    Position = new Vector3(-150, 0, 0),
                    Visibility = VisibilityGroup.G0 | VisibilityGroup.G1,
                    TransformOrder = TransformOrder.TranslateRotateScale,
                },
                new Node(new Cube(50))
                {
                    Position = new Vector3(0, -150, 0),
                    Visibility = VisibilityGroup.G1,
                },
                new Node(new Cube(130))
                {
                    Position = new Vector3(0, 120, 0),
                    Orientation = new Quaternion(Vector3.Up, Angle.FromDegrees(40)),
                    TransformOrder = Managed3D.SceneGraph.TransformOrder.RotateTranslateScale,
                    Visibility = VisibilityGroup.G1 | VisibilityGroup.G2,
                }, new LabelNode("Sample Label")
                {
                    Position = new Vector3(100, 100, 0),
                    Visibility = VisibilityGroup.G2,
                });


            var model = new Managed3D.Modeling.Model();

            model.BaseGeometry = new Managed3D.Modeling.Modifiers.Source.PrimitiveSourceModifier() { Kind = Managed3D.Modeling.PrimitiveKind.Box };
            model.RebuildModel();

            root = new Node(model.Geometry.ConvertToRenderableMesh());
            //root.Add(new Node(new Managed3D.Modeling.Primitives.Cone(40, 90, 5)));
            scene.Root = root;

            //var terrain = new Managed3D.Modeling.Primitives.Terrain(1, 1);
            //terrain.ChunkSize = 12;
            //var node = new Node();
            //node.Renderables.AddRange(terrain.Generate());
            //scene.Root = node;

            Console.WriteLine(root.GetGraphExtents());
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
                BackgroundColor = new Vector4f(0.9f, 0.9f, 0.9f, 1.0f),
            };
            var options = new RendererOptions();
            renderer.Scene = Program.scene;
            renderer.ActiveCamera.Facing = CameraFacing.Isometric;
            renderer.ActiveCamera.Mode = CameraMode.Orthographic;
            renderer.ActiveCamera.UpdateFacing();
            renderer.Initialize(options);
            renderer.Profile = Managed3D.Platform.DisplayProfile.GenericWSVGA;

            var target = new ManagedRendererHostForm(renderer);
            System.Windows.Forms.Application.Run(target);
        }
        #endregion
    }
}
