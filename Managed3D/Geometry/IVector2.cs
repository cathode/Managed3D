/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;

namespace Managed3D.Geometry
{
    /// <summary>
    /// A two-dimensional double-precision floating point vector.
    /// </summary>
    public interface IVector2
    {
        #region Properties
        /// <summary>
        /// Gets the x-component of the vector.
        /// </summary>
        double X
        {
            get;
        }

        /// <summary>
        /// Gets the y-component of the vector.
        /// </summary>
        double Y
        {
            get;
        }
        #endregion
        #region Methods
        Vector2 ToVector2();
        #endregion
    }
}
