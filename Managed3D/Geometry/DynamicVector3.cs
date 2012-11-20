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
    /// A dynamic three-dimensional double-precision floating point vector implementation.
    /// </summary>
    public sealed class DynamicVector3 : IVector3
    {

        #region Fields - Private
        public static readonly Func<Vector3> ZeroFunction = delegate
        {
            return Vector3.Zero;
        };
        private Func<Vector3> function;


        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicVector3"/> class.
        /// </summary>
        public DynamicVector3()
        {
            this.function = DynamicVector3.ZeroFunction;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="function"></param>
        public DynamicVector3(Func<Vector3> function)
        {
            if (function == null)
                throw new ArgumentNullException("function");
            this.function = function;
        }
        #endregion
        #region Properties - Public
        public Func<Vector3> Function
        {
            get
            {
                return this.function;
            }
            set
            {
                this.function = value ?? DynamicVector3.ZeroFunction;
            }
        }
        public double X
        {
            get
            {
                return this.Function().X;
            }
        }
        public double Y
        {
            get
            {
                return this.Function().Y;
            }
        }
        public double Z
        {
            get
            {
                return this.Function().Z;
            }
        }
        #endregion
    }
}
