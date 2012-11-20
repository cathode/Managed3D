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
using System.Windows.Forms;

namespace Managed3D.Rendering
{
    public sealed class ManagedRendererHostForm : Form
    {
        #region Fields
        private readonly ManagedRenderer renderer;
        private ManagedRendererHostControl hostControl;
        private DateTime lastCheck;
        private int framesRendered;
        private double lastFps;
        #endregion
        #region Constructors
        public ManagedRendererHostForm(ManagedRenderer renderer)
        {
            this.renderer = renderer;
            this.renderer.PostRender += new EventHandler<RenderEventArgs>(renderer_PostRender);
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            this.hostControl = new ManagedRendererHostControl(this.renderer);
            this.hostControl.scene = renderer.Scene;
            renderer.AttachTarget(this.hostControl);
            this.Controls.Add(hostControl);
        }
        #endregion

        void renderer_PostRender(object sender, RenderEventArgs e)
        {
            this.framesRendered++;

            var elapsed = DateTime.Now - this.lastCheck;
            if (elapsed.TotalMilliseconds > 1000)
            {
                this.lastFps = framesRendered / elapsed.TotalSeconds;
                this.lastCheck = DateTime.Now;
                this.framesRendered = 0;

                try
                {
                    this.Invoke(new Action(this.UpdateText));
                }
                catch
                {
                }
            }
        }

        void UpdateText()
        {
            this.Text = string.Format("{0} frames/second", this.lastFps.ToString("F3"));
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            this.renderer.Stop();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.hostControl.Invalidate();
        }
        public string Title
        {
            get;
            set;
        }
    }
}
