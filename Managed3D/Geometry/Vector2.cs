/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Diagnostics.Contracts;

namespace Managed3D.Geometry
{
    /// <summary>
    /// A two-dimensional double-precision floating point vector. This type is immutable.
    /// </summary>
    /// <remarks>
    /// This type is immutable.
    /// </remarks>
    public struct Vector2 
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="Vector2.North"/> property.
        /// </summary>
        private static readonly Vector2 north = new Vector2(0.0, 1.0);     
       
        /// <summary>
        /// Backing field for the <see cref="Vector2.East"/> property.
        /// </summary>
        private static readonly Vector2 east = new Vector2(1.0, 0.0);     
        
        /// <summary>
        /// Backing field for the <see cref="Vector2.South"/> property.
        /// </summary>
        private static readonly Vector2 south = new Vector2(0.0, -1.0);     
        
        /// <summary>
        /// Backing field for the <see cref="Vector2.West"/> property.
        /// </summary>
        private static readonly Vector2 west = new Vector2(-1.0, 0.0);    
        
        /// <summary>
        /// Backing field for the <see cref="Vector2.Zero"/> property.
        /// </summary>
        private static readonly Vector2 zero = new Vector2(0.0, 0.0);     
        
        /// <summary>
        /// Backing field for the <see cref="Vector2.X"/> property.
        /// </summary>
        private readonly double x;     
        
        /// <summary>
        /// Backing field for the <see cref="Vector2.Y"/> property.
        /// </summary>
        private readonly double y;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2"/> struct.
        /// </summary>
        /// <param name="vector">An <see cref="Vector2"/> containing the X and Y values to use for the new <see cref="Vector2"/> instance.</param>
        public Vector2(Vector2 vector)
        {
            this.x = vector.X;
            this.y = vector.Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2"/> struct.
        /// </summary>
        /// <param name="x">The x-component of the new vector.</param>
        /// <param name="y">The y-component of the new vector.</param>
        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets a unit vector pointing due east.
        /// </summary>
        public static Vector2 East
        {
            get
            {
                return Vector2.east;
            }
        }

        /// <summary>
        /// Gets a unit vector pointing due north.
        /// </summary>
        public static Vector2 North
        {
            get
            {
                return Vector2.north;
            }
        }

        /// <summary>
        /// Gets a unit vector pointing due south.
        /// </summary>
        public static Vector2 South
        {
            get
            {
                return Vector2.south;
            }
        }

        /// <summary>
        /// Gets a unit vector pointing due west.
        /// </summary>
        public static Vector2 West
        {
            get
            {
                return Vector2.west;
            }
        }

        /// <summary>
        /// Gets the x-component of the <see cref="Vector2"/>.
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }
        }

        /// <summary>
        /// Gets the y-component of the <see cref="Vector2"/>.
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }
        }

        /// <summary>
        /// Gets the zero vector.
        /// </summary>
        public static Vector2 Zero
        {
            get
            {
                return new Vector2(0.0, 0.0);
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Converts the current <see cref="Vector2"/> to it's absolute value.
        /// </summary>
        public Vector2 Absolute()
        {
            return new Vector2(Math.Abs(this.X), Math.Abs(this.Y));
        }

        /// <summary>
        /// Returns the absolute value of a specified vector.
        /// </summary>
        /// <param name="v">The <see cref="Vector2"/> to get the absolute value of.</param>
        /// <returns>A new <see cref="Vector2"/> that is the absolute value of <paramref name="vector"/>.</returns>
        public static Vector2 Absolute(Vector2 v)
        {
            return new Vector2(Math.Abs(v.X), Math.Abs(v.Y));
        }

        /// <summary>
        /// Adds a specified vector to the current instance.
        /// </summary>
        /// <param name="other">The <see cref="Vector2"/> to add to the current <see cref="Vector2"/>.</param>
        public Vector2 Add(Vector2 other)
        {
            return new Vector2(this.X + other.X, this.Y + other.Y);
        }

        /// <summary>
        /// Adds two <see cref="Vector2"/> instances.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2"/> to add.</param>
        /// <param name="b">The second <see cref="Vector2"/> to add.</param>
        /// <returns>A new <see cref="Vector2"/> containing the sum of inputs.</returns>
        public static Vector2 Add(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        /// Adds three <see cref="Vector2"/> instances.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2"/> to add.</param>
        /// <param name="b">The second <see cref="Vector2"/> to add.</param>
        /// <param name="c">The third <see cref="Vector2"/> to add.</param>
        /// <returns>A new <see cref="Vector2"/> containing the sum of inputs.</returns>
        public static Vector2 Add(Vector2 a, Vector2 b, Vector2 c)
        {
            return new Vector2(a.X + b.X + c.X, a.Y + b.Y + c.Y);
        }

        /// <summary>
        /// Adds four <see cref="Vector2"/> instances.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2"/> to add.</param>
        /// <param name="b">The second <see cref="Vector2"/> to add.</param>
        /// <param name="c">The third <see cref="Vector2"/> to add.</param>
        /// <param name="d">The fourth <see cref="Vector2"/> to add.</param>
        /// <returns>A new <see cref="Vector2"/> containing the sum of inputs.</returns>
        public static Vector2 Add(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
        {
            return new Vector2(a.X + b.X + c.X + d.X, a.Y + b.Y + c.Y + d.Y);
        }

        /// <summary>
        /// Returns the smallest whole <see cref="Vector2"/> greater than or equal to the current <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Ceiling()
        {
            return new Vector2(Math.Ceiling(this.X), Math.Ceiling(this.Y));
        }

        /// <summary>
        /// Returns the smallest whole <see cref="Vector2"/> greater than or equal to the specified <see cref="Vector2"/>.
        /// </summary>
        /// <param name="v">A <see cref="Vector2"/> instance.</param>
        /// <returns>A new <see cref="Vector2"/> instance representing the ceiling of <paramref name="vector"/>.</returns>
        public static Vector2 Ceiling(Vector2 v)
        {
            return new Vector2(Math.Ceiling(v.X), Math.Ceiling(v.Y));
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>true if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector2)
                return Vector2.Equals(this, (Vector2)obj);
            return false;
        }

        /// <summary>
        /// Indicates whether this instance and a specified <see cref="Vector2"/> are equal.
        /// </summary>
        /// <param name="other">Another <see cref="Vector2"/> to compare to.</param>
        /// <returns>true if <paramref name="other"/> and this instance represent the same value; otherwise, false.</returns>
        public bool Equals(Vector2 other)
        {
            return (this.X == other.X) && (this.Y == other.Y);
        }

        /// <summary>
        /// Determines if two <see cref="Vector2"/> instances are equal.
        /// </summary>
        /// <param name="a">The first instance to compare.</param>
        /// <param name="b">The second instance to compare.</param>
        /// <returns>true if both instances represent the same value; otherwise false.</returns>
        public static bool Equals(Vector2 a, Vector2 b)
        {
            return (a.X == b.X) && (a.Y == b.Y);
        }

        /// <summary>
        /// Floors the current <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Floor()
        {
            return new Vector2(Math.Floor(this.X), Math.Floor(this.Y));
        }

        /// <summary>
        /// Returns the largest whole <see cref="Vector2"/> less than or equal to the specified <see cref="Vector2"/>.
        /// </summary>
        /// <param name="vector">A <see cref="Vector2"/> instance.</param>
        /// <returns>A new <see cref="Vector2"/> instance representing the floor of <paramref name="vector"/>.</returns>
        public static Vector2 Floor(Vector2 vector)
        {
            return new Vector2(Math.Floor(vector.X), Math.Floor(vector.Y));
        }

        /// <summary>
        /// Overridden. Calculates a hash code for the current <see cref="Vector2"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="Vector2"/>.</returns>
        public override int GetHashCode()
        {
            return __HashCode.Calculate(this.X, this.Y);
        }

        /// <summary>
        /// Multiplies a <see cref="Vector2"/> by a scalar value.
        /// </summary>
        /// <param name="scalar">The scalar value to multiply by.</param>
        public Vector2 Multiply(double scalar)
        {
            return new Vector2(this.X * scalar, this.Y * scalar);
        }

        /// <summary>
        /// Multiplies a <see cref="Vector2"/> by a scalar value.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2"/> to be multiplied.</param>
        /// <param name="scalar">The scalar value to multiply by.</param>
        /// <returns>A new <see cref="Vector2"/> that is the result of the multiplication.</returns>
        public static Vector2 Multiply(Vector2 vector, double scalar)
        {
            return new Vector2(vector.X * scalar, vector.Y * scalar);
        }

        /// <summary>
        /// Rounds the current vector to the nearest integral value.
        /// </summary>
        public Vector2 Round()
        {
            return new Vector2(Math.Round(this.X), Math.Round(this.Y));
        }

        /// <summary>
        /// Rounds the current vector to the specified number of fractional digits.
        /// </summary>
        /// <param name="digits">The number of fractional digits to round to.</param>
        public Vector2 Round(int digits)
        {
            Contract.Requires(digits >= 0);
            Contract.Requires(digits <= 15);

            return new Vector2(Math.Round(this.X, digits), Math.Round(this.Y, digits));
        }

        /// <summary>
        /// Rounds a vector to the nearest integral value, and returns the result as a new instance.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2"/> to be rounded.</param>
        /// <returns>A new <see cref="Vector2"/> instance that is the rounded form of the specified vector.</returns>
        public static Vector2 Round(Vector2 vector)
        {
            return new Vector2(Math.Round(vector.X), Math.Round(vector.Y));
        }

        /// <summary>
        /// Rounds a vector to specified number of fractional digits, and returns the result as a new instance.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2"/> to be rounded.</param>
        /// <param name="digits">The number of fractional digits to round to.</param>
        /// <returns>A new <see cref="Vector2"/> instance that is the rounded form of the specified vector.</returns>
        public static Vector2 Round(Vector2 vector, int digits)
        {
            Contract.Requires(digits >= 0);
            Contract.Requires(digits <= 15);

            return new Vector2(Math.Round(vector.X, digits), Math.Round(vector.Y, digits));
        }

        /// <summary>
        /// Subtracts a specified <see cref="Vector2"/> from the current instance.
        /// </summary>
        /// <param name="other">The <see cref="Vector2"/> to subtract from the current instance.</param>
        public Vector2 Subtract(Vector2 other)
        {
            return new Vector2(this.X - other.X, this.Y - other.Y);
        }

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="a">The <see cref="Vector2"/> to subtract from.</param>
        /// <param name="b">The <see cref="Vector2"/> to subtract.</param>
        /// <returns>A new <see cref="Vector2"/> that is the result of the subtraction.</returns>
        public static Vector2 Subtract(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }
        #endregion
        #region Operators
        /// <summary>
        /// Multiplies a <see cref="Vector2"/> by a scalar value.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2"/> to be multiplied.</param>
        /// <param name="scalar">The scalar value to multiply by.</param>
        /// <returns>A new <see cref="Vector2"/> that is the result of the multiplication.</returns>
        public static Vector2 operator *(Vector2 vector, double scalar)
        {
            return new Vector2(vector.X * scalar, vector.Y * scalar);
        }

        /// <summary>
        /// Multiplies a <see cref="Vector2"/> by a scalar value.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2"/> to be multiplied.</param>
        /// <param name="scalar">The scalar value to multiply by.</param>
        /// <returns>A new <see cref="Vector2"/> that is the result of the multiplication.</returns>
        public static Vector2 operator *(double scalar, Vector2 vector)
        {
            return vector * scalar;
        }

        /// <summary>
        /// Divides a <see cref="Vector2"/> by a scalar value.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2"/> to be divided.</param>
        /// <param name="scalar">The scalar value to divide by.</param>
        /// <returns>A new <see cref="Vector2"/> that is the result of the division.</returns>
        public static Vector2 operator /(Vector2 vector, double scalar)
        {
            return new Vector2(vector.X / scalar, vector.Y / scalar);
        }

        /// <summary>
        /// Adds two <see cref="Vector2"/> instances.
        /// </summary>
        /// <param name="left">The <see cref="Vector2"/> on the left of the operator.</param>
        /// <param name="right">The <see cref="Vector2"/> on the right of the operator.</param>
        /// <returns>A new <see cref="Vector2"/> that is the result of the addition.</returns>
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        /// <summary>
        /// Subtracts two <see cref="Vector2"/> instances.
        /// </summary>
        /// <param name="left">The <see cref="Vector2"/> on the left of the operator.</param>
        /// <param name="right">The <see cref="Vector2"/> on the right of the operator.</param>
        /// <returns>A new <see cref="Vector2"/> that is the result of the subtraction.</returns>
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        /// <summary>
        /// Compares two <see cref="Vector2"/> instances for equality.
        /// </summary>
        /// <param name="left">The <see cref="Vector2"/> on the left of the operator.</param>
        /// <param name="right">The <see cref="Vector2"/> on the right of the operator.</param>
        /// <returns>true if <paramref name="left"/> represents the same value as <paramref name="right"/>; otherwise false.</returns>
        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return (left.X == right.X) && (left.Y == right.Y);
        }

        /// <summary>
        /// Compares two <see cref="Vector2"/> instances for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Vector2"/> on the left of the operator.</param>
        /// <param name="right">The <see cref="Vector2"/> on the right of the operator.</param>
        /// <returns>true if <paramref name="left"/> represents a different value than <paramref name="right"/>; otherwise false.</returns>
        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return (left.X != right.X) || (left.Y != right.Y);
        }
        #endregion
    }
}
