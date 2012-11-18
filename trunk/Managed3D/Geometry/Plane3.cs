/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents a two-dimensional surface with infinite width and height and zero curvature.
    /// </summary>
    public struct Plane3
    {
        #region Fields
        private Vector3 normal;
        private Vector3 origin;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Plane3"/> struct with the specified normal.
        /// </summary>
        /// <param name="normal">The surface normal of the plane.</param>
        public Plane3(Vector3 normal)
        {
            this.normal = normal;
            this.origin = new Vector3(0.0, 0.0, 0.0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Plane3"/> struct with the specified normal and origin.
        /// </summary>
        /// <param name="normal">The surface normal of the plane.</param>
        /// <param name="origin">The center of the plane.</param>
        public Plane3(Vector3 normal, Vector3 origin)
        {
            this.normal = normal;
            this.origin = origin;
        }
        #endregion
        #region Properties 
        /// <summary>
        /// Gets or sets a <see cref="Vector3"/> describing the normal of the plane.
        /// </summary>
        public Vector3 Normal
        {
            get
            {
                return this.normal;
            }
            set
            {
                this.normal = value;
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="Vector3"/> describing the location of the center of the plane.
        /// </summary>
        public Vector3 Origin
        {
            get
            {
                return this.origin;
            }
            set
            {
                this.origin = value;
            }
        }

        /// <summary>
        /// Gets the standard XY plane.
        /// </summary>
        public static Plane3 XY
        {
            get
            {
                return new Plane3(new Vector3(0.0, 0.0, 1.0));
            }
        }

        /// <summary>
        /// Gets the standard XZ plane.
        /// </summary>
        public static Plane3 XZ
        {
            get
            {
                return new Plane3(new Vector3(0.0, 1.0, 0.0));
            }
        }

        /// <summary>
        /// Gets the standard YZ plane.
        /// </summary>
        public static Plane3 YZ
        {
            get
            {
                return new Plane3(new Vector3(1.0, 0.0, 0.0));
            }
        }
        #endregion
    }
}
