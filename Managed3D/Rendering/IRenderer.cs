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
using Managed3D.Geometry;
using Managed3D.SceneGraph;

namespace Managed3D.Rendering
{
    /// <summary>
    /// Defines the essential functionality that a 3d renderer implementation should provide.
    /// </summary>
    public interface IRenderer
    {
        #region Properties
        /// <summary>
        /// Gets or sets a <see cref="Vector4f"/> describing a background color for rendered frames.
        /// </summary>
        Vector4f BackgroundColor
        {
            get;
            set;
        }
        /// <summary>
        /// Gets a value indicating whether the renderer is performing real-time frame rendering.
        /// </summary>
        bool IsRunning
        {
            get;
        }

        /// <summary>
        /// Gets or sets the <see cref="Scene"/>
        /// </summary>
        Scene Scene
        {
            get;
            set;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Prepares the renderer using the specified options.
        /// </summary>
        /// <param name="options"></param>
        void Initialize(RendererOptions options);

        /// <summary>
        /// Causes the renderer to render a frame.
        /// </summary>
        void RenderFrame();

        /// <summary>
        /// Causes the renderer to begin rendering frames for real-time operation.
        /// </summary>
        void Start();

        /// <summary>
        /// Causes the renderer to stop rendering real-time frames.
        /// </summary>
        void Stop();
        #endregion
    }
}
