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
    /// Represents a double-precision floating point angle. This type is immutable.
    /// </summary>
    public struct Angle
    {
        #region Constructors - Private
        private Angle(double value, AngleUnit unit)
        {
            this.degrees = (unit == AngleUnit.Degrees) ? value : Angle.DegreesFromRadians(value);
        }
        #endregion
        #region Fields - Private
        private readonly double degrees;
        #endregion
        #region Fields
        /// <summary>
        /// Holds an angle with zero degrees.
        /// </summary>
        public static readonly Angle Zero = new Angle();
        #endregion
        #region Methods - Public
        /// <summary>
        /// Adds the current <see cref="Angle"/> and a second <see cref="Angle"/> and returns the result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Angle Add(Angle other)
        {
            return this + other;
        }

        /// <summary>
        /// Subtracts an <see cref="Angle"/> from the current <see cref="Angle"/> and returns the result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Angle Subtract(Angle other)
        {
            return this - other;
        }

        /// <summary>
        /// Converts the given value in radians to it's approximate equivalent in degrees.
        /// </summary>
        /// <param name="radians">The value in radians to convert.</param>
        /// <returns>The approximate equivalent conversion of radians to degrees.</returns>
        public static double DegreesFromRadians(double radians)
        {
            return radians * (180.0 / Math.PI);
        }

        /// <summary>
        /// Converts the given value in degrees to it's approximate equivalent in radians.
        /// </summary>
        /// <param name="degrees">The value in degrees to convert.</param>
        /// <returns>The approximate equivalent conversion of degrees to radians.</returns>
        public static double RadiansFromDegrees(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        /// <summary>
        /// Determines if the specified object is equal to the current <see cref="Angle"/>.
        /// </summary>
        /// <param name="obj">The object to test for equality.</param>
        /// <returns>true if obj is an <see cref="Angle"/> and is equal, otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Angle)
                return Angle.Equals(this, (Angle)obj);

            return false;
        }

        /// <summary>
        /// Determines if the specified <see cref="Angle"/> is equal to the current <see cref="Angle"/>.
        /// </summary>
        /// <param name="other">The <see cref="Angle"/> to test for equality.</param>
        /// <returns>true if the two angles are equal, otherwise false.</returns>
        public bool Equals(Angle other)
        {
            return this == other;
        }

        /// <summary>
        /// Determines if two <see cref="Angle"/> instances are equal.
        /// </summary>
        /// <param name="a">The first angle.</param>
        /// <param name="b">The second angle.</param>
        /// <returns>true if both angles are equal, otherwise false.</returns>
        public static bool Equals(Angle a, Angle b)
        {
            return a.degrees == b.degrees;
        }

        /// <summary>
        /// Creates a new <see cref="Angle"/> from a given value in degrees.
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static Angle FromDegrees(double degrees)
        {
            return new Angle(degrees, AngleUnit.Degrees);
        }

        /// <summary>
        /// Creates a new <see cref="Angle"/> from a given value in radians.
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        public static Angle FromRadians(double radians)
        {
            return new Angle(radians, AngleUnit.Radians);
        }

        /// <summary>
        /// Returns a new <see cref="Angle"/> that represents the normalized form of the current <see cref="Angle"/>.
        /// </summary>
        /// <returns></returns>
        public Angle Normalize()
        {
            return Angle.FromDegrees(this.Degrees % 360.0);
        }

        /// <summary>
        /// Overridden. Calculates a hash code for the current instance.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return __HashCode.Calculate(this.degrees);
        }

        /// <summary>
        /// Inverts the current angle.
        /// </summary>
        /// <returns></returns>
        public Angle Invert()
        {
            return (this + Angle.FromDegrees(180.0)).Normalize();
        }

        /// <summary>
        /// Converts the current <see cref="Angle"/> to it's string representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Degrees.ToString() + "°";
        }

        #endregion
        #region Operators
        /// <summary>
        /// Determines if two angles are equal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Angle a, Angle b)
        {
            return (a.degrees == b.degrees);
        }

        /// <summary>
        /// Determines if two angles are inequal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Angle a, Angle b)
        {
            return (a.degrees != b.degrees);
        }

        /// <summary>
        /// Adds two angles together and returns the result as a new <see cref="Angle"/>.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Angle operator +(Angle a, Angle b)
        {
            return Angle.FromDegrees(a.Degrees + b.Degrees);
        }

        /// <summary>
        /// Subtracts one angle from another and returns the result as a new <see cref="Angle"/>.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Angle operator -(Angle a, Angle b)
        {
            return Angle.FromDegrees(a.Degrees - b.Degrees);
        }
        #endregion
        #region Properties - Public
        /// <summary>
        /// Gets the cosine of the current <see cref="Angle"/>.
        /// </summary>
        public double Cosine
        {
            get
            {
                return Math.Cos(this.Radians);
            }
        }

        /// <summary>
        /// Gets the value of the current <see cref="Angle"/> in degrees.
        /// </summary>
        public double Degrees
        {
            get
            {
                return this.degrees;
            }
        }

        /// <summary>
        /// Gets the value of the current <see cref="Angle"/> in radians.
        /// </summary>
        public double Radians
        {
            get
            {
                return Angle.RadiansFromDegrees(this.degrees);
            }
        }

        /// <summary>
        /// Gets the sine of the current <see cref="Angle"/>.
        /// </summary>
        public double Sine
        {
            get
            {
                return Math.Sin(this.Radians);
            }
        }
        #endregion
        #region Types - Private
        private enum AngleUnit
        {
            Degrees,
            Radians,
        }
        #endregion
    }
}
