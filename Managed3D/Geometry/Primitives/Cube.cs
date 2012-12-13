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
    public sealed class Cube : FastMesh
    {
        #region Constructors
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
            : base(8, 6, 12)
        {
            this.size = size;

            var r = size * 0.5;
            var verts = new FastVertex[8];
            verts[0] = new FastVertex(r, r, r);
            verts[1] = new FastVertex(r, -r, r);
            verts[2] = new FastVertex(-r, -r, r);
            verts[3] = new FastVertex(-r, r, r);

            verts[4] = new FastVertex(r, r, -r);
            verts[5] = new FastVertex(r, -r, -r);
            verts[6] = new FastVertex(-r, -r, -r);
            verts[7] = new FastVertex(-r, r, -r);

            var faces = new FastFace[] { 
                // Top and bottom
                new FastFace(0, 1, 2),
                new FastFace(2,3, 0),
                new FastFace(4,5,6),
                new FastFace(4,6,7),

                new FastFace(),
                new FastFace(7,1,0),
            };

            var edges = new FastEdge[] 
            {
                new FastEdge(0,1),
                new FastEdge(1,2),
                new FastEdge(2,3),
                new FastEdge(3,0),
                new FastEdge(0,4),
                new FastEdge(1,7),
                new FastEdge(2,6),
                new FastEdge(3,5),
                new FastEdge(4,5),
                new FastEdge(5,6),
                new FastEdge(6,7),
            };

        }
        #endregion
        #region Fields
        /// <summary>
        /// Holds the default size of a new <see cref="Cube"/>, if none is specified when the object is created.
        /// </summary>
        public const double DefaultSize = 1.0;
        private double size;
        #endregion
        #region Properties
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
