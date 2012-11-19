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
using Managed3D.Platform;

namespace Managed3D.Rendering
{
    /// <summary>
    /// Represents options which control the behavior of a renderer instance.
    /// </summary>
    public class RendererOptions
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RendererOptions"/> class.
        /// </summary>
        public RendererOptions()
        {
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets an empty <see cref="RendererOptions"/> instance with all values configured to their defaults.
        /// </summary>
        public static RendererOptions Empty
        {
            get
            {
                return new RendererOptions()
                {
                    Profile = DisplayProfile.Default,
                };
            }
        }
        /// <summary>
        /// Gets or sets the <see cref="DisplayProfile"/> associated with the renderer options.
        /// </summary>
        public DisplayProfile Profile
        {
            get;
            set;
        }
        #endregion
    }
}
