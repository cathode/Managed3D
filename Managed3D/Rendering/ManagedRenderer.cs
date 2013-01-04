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
using System.Threading;
using Managed3D.Geometry;

namespace Managed3D.Rendering
{
    /// <summary>
    /// Provides shared functionality for renderer implementations that are fully implemented in managed code.
    /// </summary>
    public abstract class ManagedRenderer : Renderer
    {
        #region Fields
        /// <summary>
        /// Provides a thread synchronization object used for locking access to the back buffer.
        /// </summary>
        private readonly object backBufferLock;

        /// <summary>
        /// Provides a thread synchronization object used for locking access to the front buffer.
        /// </summary>
        private readonly object frontBufferLock;

        /// <summary>
        /// Backing field for the <see cref="ManagedRenderer.BackBuffer"/> property.
        /// </summary>
        private ManagedBuffer backBuffer;

        /// <summary>
        /// Backing field for the <see cref="ManagedRenderer.FrontBuffer"/> property.
        /// </summary>
        private ManagedBuffer frontBuffer;

        private List<IManagedRendererTarget> targets;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedRenderer"/> class.
        /// </summary>
        protected ManagedRenderer()
        {
            this.frontBufferLock = new object();
            this.backBufferLock = new object();
            this.targets = new List<IManagedRendererTarget>();
            this.Scene = new SceneGraph.Scene();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the frame buffer that is currently readable.
        /// </summary>
        public ManagedBuffer FrontBuffer
        {
            get
            {
                return this.frontBuffer;
            }
        }

        /// <summary>
        /// Gets the frame buffer that is currently writable.
        /// </summary>
        protected ManagedBuffer BackBuffer
        {
            get
            {
                return this.backBuffer;
            }
        }

        #endregion
        #region Methods
        /// <summary>
        /// Associates a managed rendering target with the current renderer.
        /// </summary>
        /// <param name="target"></param>
        public void AttachTarget(IManagedRendererTarget target)
        {
            if (!this.targets.Contains(target))
            {
                this.targets.Add(target);
                target.UpdateDisplayProfile(this.Profile);
            }
        }

        public void DetachTarget(IManagedRendererTarget target)
        {
            this.targets.Remove(target);
        }

        public void AcquireFrontBufferLock()
        {
            Monitor.Enter(this.frontBufferLock);
        }

        public void ReleaseFrontBufferLock()
        {
            Monitor.Exit(this.frontBufferLock);
            //Monitor.Pulse(this.frontBufferLock);
        }

        /// <summary>
        /// Performs a page-flip of the front and back buffer. This method is thread safe.
        /// </summary>
        public void SwapBuffers()
        {
            this.AcquireFrontBufferLock();
            this.AcquireBackBufferLock();

            Interlocked.Exchange(ref this.frontBuffer, this.backBuffer);

            this.ReleaseBackBufferLock();
            this.ReleaseFrontBufferLock();
        }

        /// <summary>
        /// Zooms the active camera to fit the entire scene in the viewport.
        /// </summary>
        /// <param name="vector3"></param>
        public void ZoomExtents(Geometry.Vector3 vector3)
        {
            throw new NotImplementedException();
        }

        protected void AcquireBackBufferLock()
        {
            Monitor.Enter(this.backBufferLock);
        }

        protected void ReleaseBackBufferLock()
        {
            Monitor.Exit(this.backBufferLock);
            //Monitor
        }
        protected override void OnPreRender(RenderEventArgs e)
        {
            base.OnPreRender(e);
        }

        protected override void OnRender(RenderEventArgs e)
        {
            base.OnRender(e);
        }

        protected override void OnPostRender(RenderEventArgs e)
        {
            base.OnPostRender(e);

            this.SwapBuffers();

            foreach (var target in this.targets)
                if (target != null)
                    target.ConsumeFrameBuffer(this.FrontBuffer);
        }

        protected override void OnProfileChanged(EventArgs e)
        {
            base.OnProfileChanged(e);

            foreach (var target in this.targets)
                if (target != null)
                    target.UpdateDisplayProfile(this.Profile);

            // Make sure we have exclusive control over both the front and the back buffers.
            this.AcquireFrontBufferLock();
            this.AcquireBackBufferLock();

            this.frontBuffer = new ManagedBuffer(this.Profile.Width, this.Profile.Height);
            this.backBuffer = new ManagedBuffer(this.Profile.Width, this.Profile.Height);

            this.ReleaseBackBufferLock();
            this.ReleaseFrontBufferLock();
        }
        #endregion
    }
}
