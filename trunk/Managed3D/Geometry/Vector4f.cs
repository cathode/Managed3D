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
    /// Represents a 4-dimensional single-precision floating point vector. This type is immutable.
    /// </summary>
    public struct Vector4f
    {
        #region Fields
        /// <summary>
        /// Gets the value of the X component.
        /// </summary>
        public readonly float X;

        /// <summary>
        /// Gets the value of the Y component.
        /// </summary>
        public readonly float Y;

        /// <summary>
        /// Gets the value of the Z component.
        /// </summary>
        public readonly float Z;

        /// <summary>
        /// Gets the value of the W component.
        /// </summary>
        public readonly float W;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4f"/> struct.
        /// </summary>
        /// <param name="x">The value of the X component.</param>
        /// <param name="y">The value of the Y component.</param>
        /// <param name="z">The value of the Z component.</param>
        /// <param name="w">The value of the W component.</param>
        public Vector4f(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }
        #endregion
        #region Methods
        public static Vector4f Color(float red, float green, float blue, float alpha)
        {
            return new Vector4f(blue, green, red, alpha);
        }

        /// <summary>
        /// Adds the current vector with the specified vector and returns the result as a new instance.
        /// </summary>
        /// <param name="other">A <see cref="Vector4f"/> to add with the current vector.</param>
        /// <returns>A new <see cref="Vector4f"/> instance that is the result of the addition.</returns>
        public Vector4f Add(Vector4f other)
        {
            return new Vector4f(this.X + other.X,
                                this.Y + other.Y,
                                this.Z + other.Z,
                                this.W + other.W);
        }

        /// <summary>
        /// Clamps the values of the current <see cref="Vector4f"/> to the [0.0f, 1.0f] range,
        /// and returns the result as a new instance.
        /// </summary>
        /// <returns>A new <see cref="Vector4f"/> that is the result of the clamping operation.</returns>
        /// <remarks>
        /// Calling this method is equivalent to calling the <see cref="Vector4f.Clamp(float, float)"/> overload,
        /// with min = 0.0f and max = 1.0f.
        /// </remarks>
        public Vector4f Clamp()
        {
            var x = (this.X < 0.0f) ? 0.0f : (this.X > 1.0f) ? 1.0f : this.X;
            var y = (this.Y < 0.0f) ? 0.0f : (this.Y > 1.0f) ? 1.0f : this.Y;
            var z = (this.Z < 0.0f) ? 0.0f : (this.Z > 1.0f) ? 1.0f : this.Z;
            var w = (this.W < 0.0f) ? 0.0f : (this.W > 1.0f) ? 1.0f : this.W;

            return new Vector4f(x, y, z, w);
        }

        /// <summary>
        /// Clamps the values of the current <see cref="Vector4f"/> to the [<paramref name="min"/>, <paramref name="max"/>] range,
        /// and returns the result as a new instance.
        /// </summary>
        /// <param name="min">The smallest allowed value.</param>
        /// <param name="max">The largest allowed value.</param>
        /// <returns>A new <see cref="Vector4f"/> that is the result of the clamping operation.</returns>
        public Vector4f Clamp(float min, float max)
        {
            var x = (this.X < min) ? min : (this.X > max) ? max : this.X;
            var y = (this.Y < min) ? min : (this.Y > max) ? max : this.Y;
            var z = (this.Z < min) ? min : (this.Z > max) ? max : this.Z;
            var w = (this.W < min) ? min : (this.W > max) ? max : this.W;

            return new Vector4f(x, y, z, w);
        }
        #endregion
        #region Operators
        public static Vector4f operator +(Vector4f left, Vector4f right)
        {
            return new Vector4f(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
        }

        public static Vector4f operator -(Vector4f left, Vector4f right)
        {
            return new Vector4f(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }

        public static Vector4f operator *(Vector4f left, double right)
        {
            return new Vector4f((float)(left.X * right), (float)(left.Y * right), (float)(left.Z * right), (float)(left.W * right));
        }

        public static Vector4f operator /(Vector4f left, double right)
        {
            return new Vector4f((float)(left.X / right), (float)(left.Y / right), (float)(left.Z / right), (float)(left.W / right));
        }
        #endregion
    }
}
