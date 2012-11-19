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
    /// Represents a three-dimensional rotation by an angle around an axis.
    /// </summary>
    public struct Rotation3 : IRotation3
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="Rotation3.Angle"/> property.
        /// </summary>
        private Angle angle;

        /// <summary>
        /// Backing field for the <see cref="Rotation3.Axis"/> property.
        /// </summary>
        private Vector3 axis;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Rotation3"/> struct.
        /// </summary>
        /// <param name="angle">The angle of rotation.</param>
        /// <param name="axis">The axis to rotate about.</param>
        public Rotation3(Angle angle, Vector3 axis)
        {
            this.angle = angle;
            this.axis = axis;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the rotation angle.
        /// </summary>
        public Angle Angle
        {
            get
            {
                return this.angle;
            }
            set
            {
                this.angle = value;
            }
        }

        /// <summary>
        /// Gets the rotation axis.
        /// </summary>
        public Vector3 Axis
        {
            get
            {
                return this.axis;
            }
            set
            {
                this.axis = value;
            }
        }
        #endregion
        #region Methods
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}