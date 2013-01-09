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

namespace Managed3D.Rendering.Software.Shaders
{
    /// <summary>
    /// Represents the basic functionality shared by fragment (aka pixel) shaders for the software renderer.
    /// </summary>
    public abstract class FragmentShader
    {
        #region Methods
        protected abstract Fragment Shade(Fragment input);
        #endregion
        #region Types
        /// <summary>
        /// Implements a basic fragment shader.
        /// </summary>
        public sealed class DefaultShader : FragmentShader
        {
            protected override Fragment Shade(Fragment input)
            {
                var frag = new Fragment();
                frag.Position = input.Position;
                frag.Color = new Geometry.Vector4f(1.0f, 0f, 0f, 1.0f);

                return frag;
            }
        }
        #endregion
    }
}
