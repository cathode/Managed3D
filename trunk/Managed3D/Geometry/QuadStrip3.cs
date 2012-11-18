/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents a strip of connected quads in 3d space.
    /// </summary>
    public class QuadStrip3 : Polygon3
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="QuadStrip3"/> class.
        /// </summary>
        /// <param name="quads">The number of quads in the strip.</param>
        public QuadStrip3(int quads)
            : base(2 + (2 * quads))
        {
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the <see cref="PrimitiveKind"/> of the current primitive.
        /// </summary>
        public override PrimitiveKind Kind
        {
            get
            {
                return PrimitiveKind.QuadStrip;
            }
        }
        #endregion
    }
}
