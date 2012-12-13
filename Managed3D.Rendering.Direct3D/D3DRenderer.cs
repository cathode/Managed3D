/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using SlimDX.Windows;
using Device = SlimDX.Direct3D11.Device;
using Resource = SlimDX.Direct3D11.Resource;
using Buffer = SlimDX.Direct3D11.Buffer;
using System.Windows.Forms;
using Managed3D.SceneGraph;
using SlimDX.D3DCompiler;

namespace Managed3D.Rendering.Direct3D
{
    /// <summary>
    /// Provides a <see cref="Renderer"/> implementation that utilizes the Microsoft Direct3D API to perform rendering via hardware.
    /// </summary>
    public class D3DRenderer : Renderer, IDisposable
    {
        #region Fields
        private RenderForm form;
        private Device device;
        private SwapChain swapChain;
        private DeviceContext context;
        private Viewport viewport;
        private SwapChainDescription description;
        private RenderTargetView renderTarget;
        private int frames;
        private DateTime lastTick;
        private VertexShader vertexShader;
        private PixelShader pixelShader;
        private ShaderSignature inputSignature;
        #endregion
        #region Constructors
        public D3DRenderer()
        {
            this.lastTick = DateTime.Now;
        }
        #endregion
        #region Methods
        public override void Start()
        {
            SlimDX.Windows.MessagePump.Run(this.form, this.RenderFrame);
        }

        public override void Stop()
        {
            this.form.Close();
        }

        protected override void OnInitializing(RendererInitializationEventArgs args)
        {
            base.OnInitializing(args);

            this.form = new SlimDX.Windows.RenderForm("D3DRenderer");
            var description = new SwapChainDescription()
            {
                BufferCount = 1,
                Usage = Usage.RenderTargetOutput,
                OutputHandle = form.Handle,
                IsWindowed = true,
                ModeDescription = new ModeDescription(0, 0, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                SampleDescription = new SampleDescription(1, 0),
                Flags = SwapChainFlags.AllowModeSwitch,
                SwapEffect = SwapEffect.Discard
            };

            Device.CreateWithSwapChain(DriverType.Warp, DeviceCreationFlags.None, description, out this.device, out this.swapChain);

            using (var resource = Resource.FromSwapChain<Texture2D>(swapChain, 0))
                renderTarget = new RenderTargetView(device, resource);

            this.context = device.ImmediateContext;
            this.viewport = new Viewport(0.0f, 0.0f, form.ClientSize.Width, form.ClientSize.Height);
            context.OutputMerger.SetTargets(renderTarget);
            context.Rasterizer.SetViewports(viewport);

            using (var factory = swapChain.GetParent<Factory>())
                factory.SetWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAltEnter);

            using (var bytecode = ShaderBytecode.CompileFromFile("./Shaders/VSDefault.hlsl", "VShader", "vs_4_0", ShaderFlags.None, EffectFlags.None))
            {
                this.inputSignature = ShaderSignature.GetInputSignature(bytecode);
                this.vertexShader = new VertexShader(device, bytecode);
            }

            using (var bytecode = ShaderBytecode.CompileFromFile("./Shaders/PSDefault.hlsl", "PShader", "ps_4_0", ShaderFlags.None, EffectFlags.None))
                this.pixelShader = new PixelShader(device, bytecode);

            form.KeyDown += (o, e) =>
            {
                if (e.Alt && e.KeyCode == Keys.Enter)
                    swapChain.IsFullScreen = !swapChain.IsFullScreen;
                else
                    switch (e.KeyCode)
                    {
                        case Keys.NumPad2:
                            this.ActiveCamera.Facing = CameraFacing.South;
                            this.ActiveCamera.UpdateFacing();
                            break;

                        case Keys.NumPad4:
                            this.ActiveCamera.Facing = CameraFacing.West;
                            this.ActiveCamera.UpdateFacing();
                            break;

                        case Keys.NumPad6:
                            this.ActiveCamera.Facing = CameraFacing.East;
                            this.ActiveCamera.UpdateFacing();
                            break;

                        case Keys.NumPad8:
                            this.ActiveCamera.Facing = CameraFacing.North;
                            this.ActiveCamera.UpdateFacing();
                            break;

                        case Keys.NumPad7:
                            this.ActiveCamera.Facing = CameraFacing.Above;
                            this.ActiveCamera.UpdateFacing();
                            break;

                        case Keys.NumPad9:
                            this.ActiveCamera.Facing = CameraFacing.Below;
                            this.ActiveCamera.UpdateFacing();
                            break;

                        case Keys.NumPad5:
                            this.ActiveCamera = Camera.CreateIsometric();
                            break;

                        case Keys.Z:
                            //this.ZoomExtents(this.Scene.Root.GetGraphExtents());
                            break;

                        case Keys.Space:
                            //this.isAutoRotateEnabled = !this.isAutoRotateEnabled;
                            break;
                    }
            };

            this.UpdateScene();
        }

        public void UpdateScene()
        {
            this.AssembleNode(this.Scene.Root);
        }

        protected override void OnPreRender(RenderEventArgs e)
        {
            base.OnPreRender(e);

            context.ClearRenderTargetView(this.renderTarget, new Color4(this.BackgroundColor.X, this.BackgroundColor.Y, this.BackgroundColor.Z));
        }
        protected override void OnRender(RenderEventArgs e)
        {
            base.OnRender(e);
            for (var i = 0; i < 2; ++i)
            {
                context.Draw(3, i);
            }
        }
        protected override void OnPostRender(RenderEventArgs e)
        {
            base.OnPostRender(e);
            swapChain.Present(0, PresentFlags.None);

            ++this.frames;
            var sinceLastTick = (DateTime.Now - lastTick);
            if (sinceLastTick.Milliseconds > 500)
            {
                this.form.Invoke((Action)delegate
                {
                    this.form.Text = string.Format("{0} frames per second", (frames / (sinceLastTick.Milliseconds / 1000.0)));
                });
                this.lastTick = DateTime.Now;
                this.frames = 0;
            }
        }

        private void AssembleNode(Node node)
        {
            if (node.HasChildren)
                foreach (var child in node.Children)
                    this.AssembleNode(child);

            foreach (var renderable in node.Renderables)
            {
                var vstack = new Queue<SlimDX.Vector3>(renderable.Polygons.Length * 3);
                var fq = new Queue<int>(renderable.Polygons.Length);

                for (int i = 0; i < renderable.Polygons.Length; ++i)
                    for (int j = 0; j < renderable.Polygons[i].Vertices.Length; ++j)
                    {
                        var v = renderable.Polygons[i].Vertices[j].Position;
                        vstack.Enqueue(new SlimDX.Vector3((float)v.X, (float)v.Y, (float)v.Z));
                        fq.Enqueue(renderable.Polygons[i].Vertices.Length);
                    }

                int n = vstack.Count * 12;
                if (n == 0)
                    continue;
                var vstream = new DataStream(n, true, true);
                while (vstack.Count > 0)
                    vstream.Write(vstack.Dequeue());
                vstream.Position = 0;

                var vbuffer = new SlimDX.Direct3D11.Buffer(device, vstream, n, ResourceUsage.Default,
                    BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);

                var elements = new[] { new InputElement("POSITION", 0, Format.R32G32B32_Float, 0) };
                 var layout = new InputLayout(device, inputSignature, elements);
                //context.InputAssembler.InputLayout = layout
            }
        }
        #endregion

        public void Dispose()
        {
            // clean up all resources
            // anything we missed will show up in the debug output

            renderTarget.Dispose();
            swapChain.Dispose();
            device.Dispose();
        }
    }
}
