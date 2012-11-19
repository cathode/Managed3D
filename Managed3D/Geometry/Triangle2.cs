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
    /// Represents a triangle in two-dimensional space.
    /// </summary>
    public class Triangle2 : Polygon2
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle2"/> class.
        /// </summary>
        public Triangle2()
            : base(3)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle2"/> class.
        /// </summary>
        /// <param name="radius"></param>
        public Triangle2(double radius)
            : base(3, radius)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the <see cref="Triangle2"/> class.
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="mode"></param>
        public Triangle2(double radius, RadiusMode mode)
            : base(3, radius, mode)
        {
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the first vertex.
        /// </summary>
        public Vertex2 A
        {
            get
            {
                return this[0];
            }
            set
            {
                this[0] = value;
            }
        }

        /// <summary>
        /// Gets or sets the second vertex.
        /// </summary>
        public Vertex2 B
        {
            get
            {
                return this[1];
            }
            set
            {
                this[1] = value;
            }
        }

        /// <summary>
        /// Gets or sets the third vertex.
        /// </summary>
        public Vertex2 C
        {
            get
            {
                return this[2];
            }
            set
            {
                this[2] = value;
            }
        }
        #endregion
    }
}
