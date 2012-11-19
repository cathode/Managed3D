/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Managed3D.Geometry.Primitives
{
    /// <summary>
    /// Represents a cube primitive in 3d space. This class is immutable.
    /// </summary>
    public sealed class Cube : Mesh3
    {
        #region Constructors - Public
        /// <summary>
        /// Initializes a new instance of the <see cref="Cube"/> class using the default size.
        /// </summary>
        public Cube()
            : this(Cube.DefaultSize)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Cube"/> class using the specified size.
        /// </summary>
        /// <param name="size">The size of the new <see cref="Cube"/>.</param>
        public Cube(double size)
        {
            this.size = size;
            double s = size / 2;
            var a = new Vertex3(s, s, s);
            var b = new Vertex3(s, -s, s);
            var c = new Vertex3(-s, -s, s);
            var d = new Vertex3(-s, s, s);
            var e = new Vertex3(s, s, -s);
            var f = new Vertex3(s, -s, -s);
            var g = new Vertex3(-s, -s, -s);
            var h = new Vertex3(-s, s, -s);

            this.Polygons = new Polygon3[]
            {
                new Triangle3(a, b, c),
                new Triangle3(c, d, a),
                new Triangle3(e, a, d),
                new Triangle3(d, h, e),
                new Triangle3(e, f, b),
                new Triangle3(b, a, e),
                new Triangle3(b, f, g),
                new Triangle3(g, c, b),
                new Triangle3(d, c, g),
                new Triangle3(g, h, d),
                new Triangle3(h, g, f),
                new Triangle3(f, e, h)
            };
        }
        #endregion
        #region Fields - Private
        private double size;
        #endregion
        #region Fields - Public
        /// <summary>
        /// Holds the default size of a new <see cref="Cube"/>, if none is specified when the object is created.
        /// </summary>
        public const double DefaultSize = 1.0;
        #endregion
        #region Properties - Public
        /// <summary>
        /// Gets the size of the current <see cref="Cube"/>.
        /// </summary>
        public double Size
        {
            get
            {
                return this.size;
            }
        }
        #endregion
    }
}
