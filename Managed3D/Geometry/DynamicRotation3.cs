/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents a dynamic rotation in three-dimensional space.
    /// </summary>
    public sealed class DynamicRotation3 : IRotation3
    {
        #region Fields
        public static readonly Func<Angle> ZeroAngleFunction = delegate
        {
            return Angle.Zero;
        };

        public static readonly Func<Vector3> ZeroAxisFunction = delegate
        {
            return Vector3.Zero;
        };
        private Func<Angle> angleFunction;
        private Func<Vector3> axisFunction;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicRotation3"/> class.
        /// </summary>
        public DynamicRotation3()
        {
            this.angleFunction = DynamicRotation3.ZeroAngleFunction;
            this.axisFunction = DynamicRotation3.ZeroAxisFunction;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicRotation3"/> class.
        /// </summary>
        /// <param name="angleFunction"></param>
        /// <param name="axisFunction"></param>
        public DynamicRotation3(Func<Angle> angleFunction, Func<Vector3> axisFunction)
        {
            this.angleFunction = angleFunction;
            this.axisFunction = axisFunction;
        }
        #endregion
        #region Properties
        public Func<Angle> AngleFunction
        {
            get
            {
                return this.angleFunction;
            }
            set
            {
                this.angleFunction = value ?? DynamicRotation3.ZeroAngleFunction;
            }
        }
        public Func<Vector3> AxisFunction
        {
            get
            {
                return this.axisFunction;
            }
            set
            {
                this.axisFunction = value ?? DynamicRotation3.ZeroAxisFunction;
            }
        }
        public Angle Angle
        {
            get
            {
                return this.AngleFunction();
            }
        }
        public Vector3 Axis
        {
            get
            {
                return this.AxisFunction();
            }
        }
        #endregion
    }
}
