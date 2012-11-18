/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using Managed3D.Geometry;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents an three-dimensional mesh combined with additional rendering information such as color or texture.
    /// </summary>
    public class Model
    {
        #region Constructors
        public Model()
        {
        }
        #endregion
        #region Fields
        private Mesh3 geometry;
        #endregion
        #region Properties
        public Mesh3 Geometry
        {
            get
            {
                return this.geometry;
            }
            set
            {
                this.geometry = value;
            }
        }
        #endregion
    }
}
