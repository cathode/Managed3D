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
        /// <summary>
        /// Backing field for the <see cref="Vector3i.X"/> property.
        /// </summary>
        private int x;

        /// <summary>
        /// Backing field for the <see cref="Vector3i.Y"/> property.
        /// </summary>
        private int y;

        /// <summary>
        /// Backing field for the <see cref="Vector3i.Z"/> property.
        /// </summary>
        private int z;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3i"/> struct.
        /// </summary>
        /// <param name="x">The value of the X component for the new vector instance.</param>
        /// <param name="y">The value of the Y component for the new vector instance.</param>
        /// <param name="z">The value of the Z component for the new vector instance.</param>
        public Vector3i(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the value of the X component for the current <see cref="Vector3i"/> instance.
        /// </summary>
        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        /// <summary>
        /// Gets or sets the value of the Y component for the current <see cref="Vector3i"/> instance.
        /// </summary>
        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// Gets or sets the value of the Z component for the current <see cref="Vector3i"/> instance.
        /// </summary>
        public int Z
        {
            get
            {
                return this.z;
            }
            set
            {
                this.z = value;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool Equals(Vector3i v1, Vector3i v2)
        {
            return (v1.x == v2.x)
                && (v1.y == v2.y)
                && (v1.z == v2.z);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector3i)
                return this == (Vector3i)obj;

            return false;
        }

        public override int GetHashCode()
        {
            return __HashCode.Calculate(this.x, this.y, this.z, typeof(Vector3i));
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
