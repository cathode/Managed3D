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

namespace Managed3D.Geometry
{
    /// <summary>
    /// Provides a three-component 32-bit integer vector implementation. This type is immutable.
    /// </summary>
    public struct Vector3i
    {
        #region Fields
        private readonly int x;
        private readonly int y;
        private readonly int z;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3i"/> struct.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3i(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        #endregion
        #region Properties
        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }

        public int Z
        {
            get
            {
                return this.z;
            }
        }
        #endregion
        #region Methods
        public override bool Equals(object obj)
        {
            if (obj is Vector3i)
                return this.Equals((Vector3i)obj);

            return false;
        }
        public static bool Equals(Vector3i v1, Vector3i v2)
        {
            return (v1.x == v2.x)
                && (v1.y == v2.y)
                && (v1.z == v2.z);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
        #region Operators
        public static bool operator ==(Vector3i left, Vector3i right)
        {
            return Vector3i.Equals(left, right);
        }

        public static bool operator !=(Vector3i left, Vector3i right)
        {
            return !Vector3i.Equals(left, right);
        }
        #endregion
    }
}
