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
using System.Diagnostics.Contracts;

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

        private readonly List<IManagedRendererTarget> targets;
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
            this.backBuffer = new ManagedBuffer(0, 0);
            this.frontBuffer = new ManagedBuffer(0, 0);
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
        /// Zooms (scales) the active camera to fit the entire scene in the viewport, and repositions the camera so that it is
        /// looking at the center of the scene geometry.
        /// </summary>
        /// <param name="vector3"></param>
        public virtual void ZoomExtents(Extents3 extents)
        {
            this.ActiveCamera.Position = extents.FindMidpoint() * -1;

            var h = this.Profile.Height;
            var h1 = h / 2.0;
            var h2 = h / -2.0;
            var ey = extents.A.Y - extents.B.Y;
            var y1 = h1 / extents.A.Y;
            var y2 = h2 / extents.B.Y;
            var yf = (h / ey);
            var efactor = (extents.A.Y - extents.B.Y) / 100.0;
            var sfactor = 100.0 / (this.Profile.Height / 2.0);
            var f = efactor * sfactor;
            this.ActiveCamera.Scale = new Vector3(yf, yf, yf);
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

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.targets != null);
        }
        #endregion
    }
}
