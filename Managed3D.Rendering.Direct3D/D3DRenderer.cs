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
using System.Windows.Forms;

namespace Managed3D.Rendering.Direct3D
{
    /// <summary>
    /// Provides a <see cref="Renderer"/> implementation that utilizes the Microsoft Direct3D API to perform rendering via hardware.
    /// </summary>
    public class D3DRenderer : Renderer
    {
        #region Fields
        private RenderForm form;
        private Device device;
        private SwapChain swapChain;
        private DeviceContext context;
        private Viewport viewport;
        private SwapChainDescription description;
        private RenderTargetView renderTarget;
        #endregion
        #region Methods
        public override void Start()
        {
            SlimDX.Windows.MessagePump.Run(this.form, this.RenderFrame);
        }

        public override void Stop()
        {
            throw new NotImplementedException();
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

            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, description, out this.device, out this.swapChain);

            using (var resource = Resource.FromSwapChain<Texture2D>(swapChain, 0))
                renderTarget = new RenderTargetView(device, resource);

            this.context = device.ImmediateContext;
            this.viewport = new Viewport(0.0f, 0.0f, form.ClientSize.Width, form.ClientSize.Height);
            context.OutputMerger.SetTargets(renderTarget);
            context.Rasterizer.SetViewports(viewport);

            using (var factory = swapChain.GetParent<Factory>())
                factory.SetWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAltEnter);

          
            form.KeyDown += (o, e) =>
            {
                if (e.Alt && e.KeyCode == Keys.Enter)
                    swapChain.IsFullScreen = !swapChain.IsFullScreen;
            };

        }

        protected override void OnPreRender(RenderEventArgs e)
        {
            base.OnPreRender(e);

            context.ClearRenderTargetView(this.renderTarget, new Color4(Scene.BackgroundColor.X, Scene.BackgroundColor.Y, Scene.BackgroundColor.Z));
        }
        protected override void OnRender(RenderEventArgs e)
        {
            base.OnRender(e);

          
            

        }
        protected override void OnPostRender(RenderEventArgs e)
        {
            base.OnPostRender(e);
            swapChain.Present(0, PresentFlags.None);
        }
        #endregion
    }
}
