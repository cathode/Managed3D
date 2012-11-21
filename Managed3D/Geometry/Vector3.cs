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
    /// Represents a three-dimensional vector using three double-precision floating point numbers.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3 : IVector3, IEquatable<IVector3>, IEquatable<Vector3>
    {
        #region Fields - Private
        /// <summary>
        /// Backing field for the static <see cref="Vector3.Zero"/> property.
        /// </summary>
        private static readonly Vector3 zero = new Vector3(0.0, 0.0, 0.0);

        /// <summary>
        /// Backing field for the <see cref="Vector3.X"/> property.
        /// </summary>
        private readonly double x;

        /// <summary>
        /// Backing field for the <see cref="Vector3.Y"/> property.
        /// </summary>
        private readonly double y;

        /// <summary>
        /// Backing field for the <see cref="Vector3.Z"/> property.
        /// </summary>
        private readonly double z;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> struct.
        /// </summary>
        /// <param name="vector">A <see cref="Managed3D.Geometry.IVector2"/> instance supplying x and y values.</param>
        /// <remarks>The z value defaults to 0.</remarks>
        public Vector3(Managed3D.Geometry.IVector2 vector)
        {
            this.x = vector.X;
            this.y = vector.Y;
            this.z = 0.0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> struct.
        /// </summary>
        /// <param name="vector">A <see cref="IVector3"/> instance supplying x, y and z values.</param>
        public Vector3(IVector3 vector)
        {
            this.x = vector.X;
            this.y = vector.Y;
            this.z = vector.Z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> struct.
        /// </summary>
        /// <param name="x">The X-component of the vector.</param>
        /// <param name="y">The Y-component of the vector.</param>
        /// <param name="z">The Z-component of the vector.</param>
        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the zero vector.
        /// </summary>
        public static Vector3 Zero
        {
            get
            {
                return Vector3.zero;
            }
        }

        /// <summary>
        /// Gets or sets the X-component of the vector.
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }
        }

        /// <summary>
        /// Gets or sets the Y-component of the vector.
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }
        }

        /// <summary>
        /// Gets or sets the Z-component of the vector.
        /// </summary>
        public double Z
        {
            get
            {
                return this.z;
            }
        }
        #endregion
        #region Operators
        /// <summary>
        /// Subtracts vector b from vector a and returns a new vector as the result.
        /// </summary>
        /// <param name="left">The value that appears on the left-hand side of the operator.</param>
        /// <param name="right">The value that appears on the right-hand side of the operator.</param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        /// <summary>
        /// Calculates inequality of two vectors.
        /// </summary>
        /// <param name="left">The value that appears on the left-hand side of the operator.</param>
        /// <param name="right">The value that appears on the right-hand side of the operator.</param>
        /// <returns></returns>
        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return left.X != right.X || left.Y != right.Y || left.Z != right.Z;
        }

        /// <summary>
        /// Multiplies a vector by a scalar value and returns a new vector as the result.
        /// </summary>
        /// <param name="left">The value that appears on the left-hand side of the operator.</param>
        /// <param name="right">The value that appears on the right-hand side of the operator.</param>
        /// <returns>A new <see cref="Vector3"/> that is the result of the scalar multiplication.</returns>
        public static Vector3 operator *(Vector3 left, double right)
        {
            return new Vector3(left.X * right, left.Y * right, left.Z * right);
        }

        /// <summary>
        /// Multiplies a vector by a scalar value and returns a new vector as the result.
        /// </summary>
        /// <param name="left">The value that appears on the left-hand side of the operator.</param>
        /// <param name="right">The value that appears on the right-hand side of the operator.</param>
        /// <returns>A new <see cref="Vector3"/> that is the result of the scalar multiplication.</returns>
        public static Vector3 operator *(double left, Vector3 right)
        {
            return new Vector3(left * right.X, left * right.Y, left * right.Z);
        }

        /// <summary>
        /// Divides a vector by a scalar value and returns a new vector as the result.
        /// </summary>
        /// <param name="left">The value that appears on the left-hand side of the operator.</param>
        /// <param name="right">The value that appears on the right-hand side of the operator.</param>
        /// <returns></returns>
        public static Vector3 operator /(Vector3 left, double right)
        {
            return new Vector3(left.X / right, left.Y / right, left.Z / right);
        }

        /// <summary>
        /// Adds vector a and vector b and returns a new vector as the result.
        /// </summary>
        /// <param name="left">The left-hand vector to be added.</param>
        /// <param name="right">The right-hand vector to be added.</param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        /// <summary>
        /// Calculates equality of two vectors.
        /// </summary>
        /// <param name="left">The value that appears on the left-hand side of the operator.</param>
        /// <param name="right">The value that appears on the right-hand side of the operator.</param>
        /// <returns></returns>
        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }
        #endregion
        #region Methods
        public Vector3 Absolute()
        {
            return new Vector3(Math.Abs(this.X), Math.Abs(this.Y), Math.Abs(this.Z));
        }

        /// <summary>
        /// Returns the absolute value of a specified <see cref="Vector3"/>.
        /// </summary>
        /// <param name="vector">The <see cref="Vector3"/> to get the absolute value of.</param>
        /// <returns>A new <see cref="Vector3"/> that is the absolute value of <paramref name="vector"/>.</returns>
        public static Vector3 Absolute(Vector3 vector)
        {
            return new Vector3(Math.Abs(vector.X), Math.Abs(vector.Y), Math.Abs(vector.Z));
        }

        public Vector3 Add(Vector3 other)
        {
            return new Vector3(this.X + other.X, this.Y + other.Y, this.Z + other.Z);
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 Add(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X,
                               a.Y + b.Y,
                               a.Z + b.Z);
        }

        /// <summary>
        /// Adds three vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Vector3 Add(Vector3 a, Vector3 b, Vector3 c)
        {
            return new Vector3(a.X + b.X + c.X,
                               a.Y + b.Y + c.Y,
                               a.Z + b.Z + c.Z);
        }

        /// <summary>
        /// Adds four vectors.
        /// </summary>
        /// <param name="a">The first vector to add.</param>
        /// <param name="b">The second vector to add.</param>
        /// <param name="c">The third vector to add.</param>
        /// <param name="d">The fourth vector to add.</param>
        /// <returns>A new <see cref="Vector3"/> that is the result of the addition.</returns>
        public static Vector3 Add(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {
            return new Vector3(a.X + b.X + c.X + d.X,
                               a.Y + b.Y + c.Y + d.Y,
                               a.Z + b.Z + c.Z + d.Z);
        }

        /// <summary>
        /// Adds an array of vectors.
        /// </summary>
        /// <param name="vectors"></param>
        /// <returns></returns>
        public static Vector3 Add(Vector3[] vectors)
        {
            if (vectors == null)
                throw new ArgumentNullException("vectors");
            else if (vectors.Length < 1)
                return Vector3.Zero;
            else if (vectors.Length == 1)
                return vectors[0];
            var r = vectors[0];
            for (int i = 1; i < vectors.Length; i++)
                r += vectors[i];

            return r;
        }

        /// <summary>
        /// Returns the smallest <see cref="Vector3"/> with whole number values which is greater than or equal to the current <see cref="Vector3"/>.
        /// </summary>
        public Vector3 Ceiling()
        {
            return new Vector3(Math.Ceiling(this.X),
                               Math.Ceiling(this.Y),
                               Math.Ceiling(this.Z));
        }

        /// <summary>
        /// Returns the smallest whole <see cref="Vector3"/> greater than or equal to the specified <see cref="Vector3"/>.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3 Ceiling(Vector3 vector)
        {
            return new Vector3(Math.Ceiling(vector.X),
                               Math.Ceiling(vector.Y),
                               Math.Ceiling(vector.Z));
        }

        /// <summary>
        /// Calculates the cross product of the current <see cref="Vector3"/> and the specified <see cref="Vector3"/>.
        /// </summary>
        /// <param name="other">The other <see cref="Vector3"/> instance.</param>
        public Vector3 CrossProduct(Vector3 other)
        {
            return new Vector3((this.Y * other.Z) - (this.Z * other.Y),
                                    (this.Z * other.X) - (this.X * other.Z),
                                    (this.X * other.Y) - (this.Y * other.X));
        }

        /// <summary>
        /// Calculates the cross product of two <see cref="Vector3">Vector3's</see>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 CrossProduct(Vector3 a, Vector3 b)
        {
            return new Vector3((a.Y * b.Z) - (a.Z * b.Y), (a.Z * b.X) - (a.X * b.Z), (a.X * b.Y) - (a.Y * b.X));
        }

        /// <summary>
        /// Calculates the dot product of two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double DotProduct(Vector3 a, Vector3 b)
        {
            return (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        }

        /// <summary>
        /// Divides the current vector by a scalar value.
        /// </summary>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public Vector3 Divide(double scalar)
        {
            return this / scalar;
        }

        /// <summary>
        /// Compares the current <see cref="Vector3"/> and the specified object for equality.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector3)
                return this == (Vector3)obj;
            return false;
        }

        /// <summary>
        /// Compares the current <see cref="Vector3"/> and the specified <see cref="IVector3"/> for equality.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IVector3 other)
        {
            return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
        }

        /// <summary>
        /// Compares the current <see cref="Vector3"/> and the specified <see cref="Vector3"/> for equality.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Vector3 other)
        {
            return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
        }

        /// <summary>
        /// Compares two <see cref="Vector3"/> instances for equality.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Equals(Vector3 a, Vector3 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        /// <summary>
        /// Calculates a unique hash code for the current instance.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return __HashCode.Calculate(this.X, this.Y, this.Z);
        }

        /// <summary>
        /// Interpolates the midpoint of two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 Interpolate(Vector3 a, Vector3 b)
        {
            return new Vector3((a.X * 0.5) + (b.X * 0.5),
                               (a.Y * 0.5) + (b.Y * 0.5),
                               (a.Z * 0.5) + (b.Z * 0.5));
        }

        /// <summary>
        /// Interpolates the weighted midpoint between two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static Vector3 Interpolate(Vector3 a, Vector3 b, double weight)
        {
            if (weight < 0.0 || weight > 1.0)
                throw new ArgumentOutOfRangeException("weight", "Weight must be between 0.0 and 1.0");
            return new Vector3((a.X * (1.0 - weight)) + (b.X * weight),
                (a.Y * (1.0 - weight)) + (b.Y * weight),
                (a.Z * (1.0 - weight)) + (b.Z * weight));
        }

        /// <summary>
        /// Returns the midpoint of three vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Vector3 Interpolate(Vector3 a, Vector3 b, Vector3 c)
        {
            return Vector3.Interpolate(Vector3.Interpolate(a, b), c, 2.0 / 3.0);
        }

        public static Vector3 InterpolateN(params Vector3[] vectors)
        {
            double weight = 1.0 / vectors.Length;
            Vector3 r = Vector3.Zero;
            for (int i = 0; i < vectors.Length; i++)
                r += vectors[i] * weight;

            return r;
        }

        /// <summary>
        /// Returns the midpoint of four vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Vector3 Interpolate(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {
            return Vector3.Interpolate(Vector3.Interpolate(a, c), Vector3.Interpolate(b, d));
        }

        /// <summary>
        /// Inverts the current <see cref="Vector3"/>.
        /// </summary>
        /// <returns>A new <see cref="Vector3"/> that is the inversion of the current <see cref="Vector3"/>.</returns>
        public Vector3 Invert()
        {
            return new Vector3(-this.X, -this.Y, -this.Z);
        }

        /// <summary>
        /// Inverts the specified <see cref="Vector3"/>.
        /// </summary>
        /// <param name="vector">The <see cref="Vector3"/> instance to invert.</param>
        /// <returns>A new <see cref="Vector3"/> that is the inversion of the specified <see cref="Vector3"/>.</returns>
        public static Vector3 Invert(Vector3 vector)
        {
            return new Vector3(-vector.X, -vector.Y, -vector.Z);
        }

        /// <summary>
        /// Determines if the current <see cref="Vector3"/> is parallel to the specified <see cref="Vector3"/>.
        /// </summary>
        /// <param name="other">Another <see cref="Vector3"/> instance.</param>
        /// <returns>true if the current vector is parallel to the specified vector..</returns>
        public bool IsParallelTo(Vector3 other)
        {
            if (Vector3.CrossProduct(this, other) == Vector3.Zero)
                return true;
            return false;
        }

        /// <summary>
        /// Multiplies the current vector by a scalar value.
        /// </summary>
        /// <param name="scalar">The double-precision floating point value to multiply the X, Y, and Z components of the current vector.</param>
        /// <returns></returns>
        public Vector3 Multiply(double scalar)
        {
            return this * scalar;
        }

        /// <summary>
        /// Subtracts a vector from the current vector.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector3 Subtract(Vector3 other)
        {
            return this - other;
        }
        public Vector3 Normalize()
        {
            return Vector3.Normalize(this);
        }

        public static Vector3 Normalize(Vector3 v)
        {
            var sum = (v.X * v.X) + (v.Y * v.Y) + (v.Z * v.Z);

            if (sum == 0)
            {
                return new Vector3(0, 0, 0);
            }
            else
            {
                var d = Math.Sqrt(sum);
                return new Vector3(v.X / d, v.Y / d, v.Z / d);
            }
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", this.X, this.Y, this.Z);
        }
        #endregion

        Vector3 IVector3.ToVector3()
        {
            throw new NotImplementedException();
        }

        Vector2 IVector2.ToVector2()
        {
            throw new NotImplementedException();
        }
    }
}