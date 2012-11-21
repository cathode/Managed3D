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
    /// A three-dimensional double-precision floating point vector.
    /// </summary>
    public interface IVector3 : Managed3D.Geometry.IVector2
    {
        #region Properties
        /// <summary>
        /// Gets the z-component of the vector.
        /// </summary>
        double Z
        {
            get;
        }
        #endregion
        #region Methods
        Vector3 ToVector3();
        #endregion
    }
}
