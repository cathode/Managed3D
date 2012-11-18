/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Runtime.InteropServices;

namespace Managed3D.Geometry
{
    /// <summary>
    /// A four-dimensional double-precision floating point vector.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct Vector4 : IVector4
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="Vector4.X"/> property.
        /// </summary>
        [FieldOffset(0x00)]
        private readonly double x;

        /// <summary>
        /// Backing field for the <see cref="Vector4.Y"/> property.
        /// </summary>
        [FieldOffset(0x08)]
        private readonly double y;

        /// <summary>
        /// Backing field for the <see cref="Vector4.Z"/> property.
        /// </summary>
        [FieldOffset(0x10)]
        private readonly double z;

        /// <summary>
        /// Backing field for the <see cref="Vector4.W"/> property.
        /// </summary>
        [FieldOffset(0x18)]
        private readonly double w;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4"/> struct.
        /// </summary>
        /// <param name="x">The x-coordinate of the new vector.</param>
        /// <param name="y">The y-coordinate of the new vector.</param>
        /// <param name="z">The z-coordinate of the new vector.</param>
        /// <param name="w">The w-coordinate of the new vector.</param>
        public Vector4(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the x-component of the vector.
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }
        }

        /// <summary>
        /// Gets or sets the y-component of the vector.
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }
        }

        /// <summary>
        /// Gets or sets the z-component of the vector.
        /// </summary>
        public double Z
        {
            get
            {
                return this.z;
            }
        }

        /// <summary>
        /// Gets or sets the w-component of the vector.
        /// </summary>
        public double W
        {
            get
            {
                return this.w;
            }
        }

        /// <summary>
        /// Gets the four-dimensional zero vector.
        /// </summary>
        public static Vector4 Zero
        {
            get
            {
                return new Vector4();
            }
        }
        #endregion
        #region Methods
        public Vector4 Add(Vector4 other)
        {
            return new Vector4(this.x + other.x,
                               this.y + other.y,
                               this.z + other.z,
                               this.w + other.w);
        }

        public static Vector4 Add(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x + b.x,
                               a.y + b.y,
                               a.z + b.z,
                               a.w + b.w);
        }

        public Vector4 Clamp()
        {
            double x = (this.x < 0.0) ? 0.0 : (this.x > 1.0) ? 1.0 : this.x;
            double y = (this.y < 0.0) ? 0.0 : (this.y > 1.0) ? 1.0 : this.y;
            double z = (this.z < 0.0) ? 0.0 : (this.z > 1.0) ? 1.0 : this.z;
            double w = (this.w < 0.0) ? 0.0 : (this.w > 1.0) ? 1.0 : this.w;

            return new Vector4(x, y, z, w);
        }

        public Vector4 Clamp(double min, double max)
        {
            double x = (this.x < min) ? min : (this.x > max) ? max : this.x;
            double y = (this.y < min) ? min : (this.y > max) ? max : this.y;
            double z = (this.z < min) ? min : (this.z > max) ? max : this.z;
            double w = (this.w < min) ? min : (this.w > max) ? max : this.w;

            return new Vector4(x, y, z, w);
        }

        public static Vector4 Clamp(Vector4 v)
        {
            double x = (v.x < 0.0) ? 0.0 : (v.x > 1.0) ? 1.0 : v.x;
            double y = (v.y < 0.0) ? 0.0 : (v.y > 1.0) ? 1.0 : v.y;
            double z = (v.z < 0.0) ? 0.0 : (v.z > 1.0) ? 1.0 : v.z;
            double w = (v.w < 0.0) ? 0.0 : (v.w > 1.0) ? 1.0 : v.w;

            return new Vector4(x, y, z, w);
        }

        public static Vector4 Clamp(Vector4 v, double min, double max)
        {
            double x = (v.x < min) ? min : (v.x > max) ? max : v.x;
            double y = (v.y < min) ? min : (v.y > max) ? max : v.y;
            double z = (v.z < min) ? min : (v.z > max) ? max : v.z;
            double w = (v.w < min) ? min : (v.w > max) ? max : v.w;

            return new Vector4(x, y, z, w);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>true if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector4)
                return this == (Vector4)obj;
            return false;
        }

        /// <summary>
        /// Determines if two <see cref="Vector4"/> instances are equal.
        /// </summary>
        /// <param name="first">The first <see cref="Vector4"/> to compare.</param>
        /// <param name="second">The second <see cref="Vector4"/> to compare.</param>
        /// <returns>true if both instances represent the same value; otherwise, false.</returns>
        public static bool Equals(Vector4 first, Vector4 second)
        {
            return first == second;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return __HashCode.Calculate(this.x, this.y, this.z, this.w);
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}", this.x, this.y, this.z, this.w);
        }
        public Vector4 Normalize()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Operators
        /// <summary>
        /// Determines if two <see cref="Vector4"/> instances are equal.
        /// </summary>
        /// <param name="left">The first <see cref="Vector4"/> to compare.</param>
        /// <param name="right">The second <see cref="Vector4"/> to compare.</param>
        /// <returns>true if both instances represent the same value; otherwise, false.</returns>
        public static bool operator ==(Vector4 left, Vector4 right)
        {
            return left.w == right.w && left.x == right.x && left.y == right.y && left.z == right.z;
        }

        /// <summary>
        /// Determines if two <see cref="Vector4"/> instances are inequal.
        /// </summary>
        /// <param name="left">The first <see cref="Vector4"/> to compare.</param>
        /// <param name="right">The second <see cref="Vector4"/> to compare.</param>
        /// <returns>true if both instances represent different values; otherwise, false.</returns>
        public static bool operator !=(Vector4 left, Vector4 right)
        {
            return left.w != right.w || left.x != right.x || left.y != right.y || left.z != right.z;
        }

        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            return new Vector4(left.x + right.x,
                               left.y + right.y,
                               left.z + right.z,
                               left.w + right.w);
        }
        #endregion
    }
}
