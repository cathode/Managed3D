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
    /// Represents a dynamic 4-dimensional vector.
    /// </summary>
    public sealed class DynamicVector4 : IVector4
    {
        #region Fields
        /// <summary>
        /// Holds the zero function (returns an empty <see cref="Vector4"/>).
        /// </summary>
        public static readonly Func<IVector4> ZeroFunction = delegate
        {
            return Vector4.Zero;
        };

        /// <summary>
        /// Backing field for the <see cref="DynamicVector4.Function"/> property.
        /// </summary>
        private Func<IVector4> function;

        /// <summary>
        /// Backing field for the <see cref="DynamicVector4.W"/> property.
        /// </summary>
        private double w;

        /// <summary>
        /// Backing field for the <see cref="DynamicVector4.X"/> property.
        /// </summary>
        private double x;

        /// <summary>
        /// Backing field for the <see cref="DynamicVector4.Y"/> property.
        /// </summary>
        private double y;

        /// <summary>
        /// Backing field for the <see cref="DynamicVector4.Z"/> property.
        /// </summary>
        private double z;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicVector4"/> class.
        /// </summary>
        public DynamicVector4()
        {
            this.function = DynamicVector4.ZeroFunction;
            this.Refresh();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicVector4"/> class.
        /// </summary>
        /// <param name="function">A function that yields an <see cref="IVector4"/> when invoked.</param>
        public DynamicVector4(Func<IVector4> function)
        {
            this.function = function ?? DynamicVector4.ZeroFunction;
            this.Refresh();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the function that yields an <see cref="IVector4"/> when invoked.
        /// </summary>
        public Func<IVector4> Function
        {
            get
            {
                return this.function;
            }
            set
            {
                this.function = value;
            }
        }

        /// <summary>
        /// Gets the cached value of the W component.
        /// </summary>
        public double W
        {
            get
            {
                return this.w;
            }
        }

        /// <summary>
        /// Gets the cached value of the X component.
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }
        }

        /// <summary>
        /// Gets the cached value of the Y component.
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }
        }

        /// <summary>
        /// Gets the cached value of the Z component.
        /// </summary>
        public double Z
        {
            get
            {
                return this.z;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Refreshes the cached X, Y, Z and W values with new values from the <see cref="IVector"/> returned by <see cref="DynamicVector4.Function"/>.
        /// </summary>
        public void Refresh()
        {
            var vec = this.function();

            this.w = vec.W;
            this.x = vec.X;
            this.y = vec.Y;
            this.z = vec.Z;
        }
        #endregion


        public Vector4 ToVector4()
        {
            throw new NotImplementedException();
        }


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
