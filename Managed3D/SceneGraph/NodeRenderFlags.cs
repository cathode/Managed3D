/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;

namespace Managed3D.SceneGraph
{
    /// <summary>
    /// Represents flags that control how (or if) a scene node is rendered.
    /// </summary>
    [Flags]
    public enum NodeRenderFlags
    {
        /// <summary>
        /// The node will not be rendered.
        /// </summary>
        None = 0x0,
        /// <summary>
        /// The node is visible and can be rendered.
        /// </summary>
        Visible = 0x1,
        /// <summary>
        /// Edges of the object are highlighted.
        /// </summary>
        EdgeHighlight = 0x2,
        /// <summary>
        /// Vertices of the object are highlighted.
        /// </summary>
        VertexHighlight = 0x4,
    }
}
