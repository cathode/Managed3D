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
    /// A dynamic two-dimensional vector.
    /// </summary>
    public sealed class DynamicVector2 : IVector2
    {
        #region Fields
        /// <summary>
        /// Returns the two-dimensional zero vector when no Function is set.
        /// </summary>
        /// <returns></returns>
        public static readonly Func<Vector2> ZeroFunction = delegate
        {
            return Vector2.Zero;
        };

        /// <summary>
        /// Backing field for the <see cref="DynamicVector2.Function"/> property.
        /// </summary>
        private Func<Vector2> function;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicVector2"/> class.
        /// </summary>
        public DynamicVector2()
        {
            this.function = DynamicVector2.ZeroFunction;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the function which provides the dynamic vector values.
        /// </summary>
        public Func<Vector2> Function
        {
            get
            {
                return this.function;
            }
            set
            {
                this.function = value ?? DynamicVector2.ZeroFunction;
            }
        }

        /// <summary>
        /// Gets the x-component of the vector.
        /// </summary>
        public double X
        {
            get
            {
                return this.Function().X;
            }
        }

        /// <summary>
        /// Gets the y-component of the vector.
        /// </summary>
        public double Y
        {
            get
            {
                return this.Function().Y;
            }
        }

        #endregion
        #region Methods
        public Vector2 ToVector2()
        {
            return this.Function();
        }
        #endregion
    }
}
