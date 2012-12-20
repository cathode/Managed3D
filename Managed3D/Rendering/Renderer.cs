/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Threading;
using Managed3D.Platform;
using Managed3D.SceneGraph;
using Managed3D.Geometry;

namespace Managed3D.Rendering
{
    /// <summary>
    /// Represents the basic functionality that underpins a 3D graphics renderer.
    /// </summary>
    /// <remarks>
    /// After creating a new instance of a <see cref="Renderer"/>-derived class,
    /// the <see cref="Renderer.Initialize"/> method must be called to perform any required set-up
    /// which the implementation might need to do prior to being able to render frames.
    /// </remarks>
    public abstract class Renderer : IRenderer
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="Renderer.ActiveCamera"/> property.
        /// </summary>
        private Camera activeCamera;

        /// <summary>
        /// Backing field for the <see cref="Renderer.IsInitialized"/> property.
        /// </summary>
        private bool isInitialized;

        /// <summary>
        /// Backing field for the <see cref="Renderer.IsRunning"/> property.
        /// </summary>
        private bool isRunning;

        /// <summary>
        /// Backing field for the <see cref="Renderer.Profile"/> property.
        /// </summary>
        private DisplayProfile profile;

        /// <summary>
        /// Backing field for the <see cref="Renderer.Scene"/> property.
        /// </summary>
        private Scene scene;

        /// <summary>
        /// Backing field for the <see cref="Scene.BackgroundColor"/> property.
        /// </summary>
        private Vector4f backgroundColor;

        private int frameCount;

        private DateTime lastCheck;

        private double lastRate;
        private double currentRate;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Renderer"/> class.
        /// </summary>
        protected Renderer()
        {
            this.profile = DisplayProfile.Default;
            this.activeCamera = new Camera()
            {
                FieldOfView = Angle.FromDegrees(45),
                FocalDistance = 1000,
                Mode = CameraMode.Perspective
            };
        }
        #endregion
        #region Events
        /// <summary>
        /// Raised when the <see cref="Renderer"/> is performing it's initial set-up.
        /// </summary>
        public event EventHandler<RendererInitializationEventArgs> Initializing;

        /// <summary>
        /// Raised after a frame has been rendered.
        /// </summary>
        public event EventHandler<RenderEventArgs> PostRender;

        /// <summary>
        /// Raised prior to a frame being rendered.
        /// </summary>
        public event EventHandler<RenderEventArgs> PreRender;

        /// <summary>
        /// Raised when the active <see cref="DisplayProfile"/> is changed.
        /// </summary>
        public event EventHandler ProfileChanged;

        /// <summary>
        /// Raised when a frame is rendered.
        /// </summary>
        public event EventHandler<RenderEventArgs> Render;

        /// <summary>
        /// Raised when the active <see cref="Scene"/> is changed.
        /// </summary>
        public event EventHandler SceneChanged;

        /// <summary>
        /// Raised when the renderer is started.
        /// </summary>
        public event EventHandler Starting;

        /// <summary>
        /// Raised when the renderer is stopped.
        /// </summary>
        public event EventHandler Stopping;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the background color of the scene.
        /// </summary>
        public Vector4f BackgroundColor
        {
            get
            {
                return this.backgroundColor;
            }
            set
            {
                this.backgroundColor = value;
            }
        }
        /// <summary>
        /// Gets a value indicating whether the current renderer has been initialized.
        /// </summary>
        public bool IsInitialized
        {
            get
            {
                return this.isInitialized;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current renderer is running (has been started).
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
        }

        /// <summary>
        /// Gets or sets the active <see cref="DisplayProfile"/> for the current renderer.
        /// </summary>
        public DisplayProfile Profile
        {
            get
            {
                return this.profile;
            }
            set
            {
                this.profile = value ?? DisplayProfile.Default;
                this.OnProfileChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the active <see cref="Scene"/> for the current renderer.
        /// </summary>
        public Scene Scene
        {
            get
            {
                return this.scene;
            }
            set
            {
                this.scene = value;
                this.OnSceneChanged(EventArgs.Empty);
            }
        }

        public Camera ActiveCamera
        {
            get
            {
                return this.activeCamera;
            }
            set
            {
                this.activeCamera = value;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Sets up the renderer.
        /// </summary>
        /// <param name="options">The configuration data that describes how the renderer should be set up.</param>
        public void Initialize(RendererOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            this.OnInitializing(new RendererInitializationEventArgs(options));

            this.Profile = options.Profile;

            this.isInitialized = true;
        }

        /// <summary>
        /// Renders a frame.
        /// </summary>
        public void RenderFrame()
        {
            var options = this.CreateFrameOptions();
            var e = new RenderEventArgs(options);

            this.OnPreRender(e);
            this.OnRender(e);
            this.OnPostRender(e);
        }

        /// <summary>
        /// Starts the frame rendering process.
        /// </summary>
        public virtual void Start()
        {
            if (this.isRunning)
                return;

            this.OnStarting(EventArgs.Empty);

            this.isRunning = true;

            while (this.isRunning)
            {
                this.RenderFrame();
                //Thread.Sleep(25);
            }
        }

        /// <summary>
        /// Stops the frame rendering process.
        /// </summary>
        public virtual void Stop()
        {
            if (!this.isRunning)
                return;

            this.OnStopping(EventArgs.Empty);

            this.isRunning = false;
        }

        /// <summary>
        /// Creates an empty set of <see cref="FrameOptions"/>.
        /// </summary>
        /// <returns>A new instance of the <see cref="FrameOptions"/> class,
        /// or of a derived class that has specific rendering options for the
        /// current renderer implementation.</returns>
        protected virtual FrameOptions CreateFrameOptions()
        {
            return new FrameOptions();
        }

        /// <summary>
        /// Raises the <see cref="Renderer.Initializing"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnInitializing(RendererInitializationEventArgs e)
        {
            if (this.Initializing != null)
                this.Initializing(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Renderer.PostRender"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnPostRender(RenderEventArgs e)
        {
            if (this.PostRender != null)
                this.PostRender(this, e);

            ++this.frameCount;

            var tdelta = DateTime.Now - this.lastCheck;
            if (tdelta.Milliseconds > 500)
            {
                this.lastRate = this.currentRate;
                this.currentRate = this.frameCount / tdelta.TotalSeconds;
                this.lastCheck = DateTime.Now;
            }
        }

        /// <summary>
        /// Raises the <see cref="Renderer.PreRender"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnPreRender(RenderEventArgs e)
        {
            if (this.PreRender != null)
                this.PreRender(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Renderer.ProfileChanged"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnProfileChanged(EventArgs e)
        {
            if (this.ProfileChanged != null)
                this.ProfileChanged(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Renderer.Render"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnRender(RenderEventArgs e)
        {
            if (this.Render != null)
                this.Render(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Renderer.SceneChanged"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnSceneChanged(EventArgs e)
        {
            if (this.SceneChanged != null)
                this.SceneChanged(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Renderer.Starting"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnStarting(EventArgs e)
        {
            if (this.Starting != null)
                this.Starting(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Renderer.Stopping"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnStopping(EventArgs e)
        {
            if (this.Stopping != null)
                this.Stopping(this, e);
        }
        #endregion
    }
}
