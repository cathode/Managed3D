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
        {
            this.size = size;

            var r = size * 0.5;
            var verts = new Vertex3[8];
            verts[0] = new Vertex3(r, r, r);
            verts[1] = new Vertex3(r, -r, r);
            verts[2] = new Vertex3(-r, -r, r);
            verts[3] = new Vertex3(-r, r, r);

            verts[4] = new Vertex3(r, r, -r);
            verts[5] = new Vertex3(r, -r, -r);
            verts[6] = new Vertex3(-r, -r, -r);
            verts[7] = new Vertex3(-r, r, -r);

            var edges = new Edge3[]
            {
                new Edge3(verts, 0, 1),
                new Edge3(verts, 1, 2),
                new Edge3(verts, 2, 3),
                new Edge3(verts, 3, 0),

                new Edge3(verts, 4, 5),
                new Edge3(verts, 5, 6),
                new Edge3(verts, 6, 7),
                new Edge3(verts, 7, 4),

                new Edge3(verts, 0, 6),
                new Edge3(verts, 1, 5),
                new Edge3(verts, 2, 4),
                new Edge3(verts, 3, 7),
            };

            this.Polygons = new Quad3[] { 
                // Top and bottom
                new Quad3(edges, 0, 1, 2, 3),
                new Quad3(edges, 4, 5, 6, 7),
                //new Quad3(edges,  
                new Quad3(verts, 0, 3, 7, 4),
                new Quad3(verts, 1, 0, 4, 5),
                new Quad3(verts, 2, 1, 5, 6),
                new Quad3(verts, 3, 2, 6, 7),
                
            };
        }
        #endregion
        #region Fields
        private double size;
        #endregion
        #region Fields
        /// <summary>
        /// Holds the default size of a new <see cref="Cube"/>, if none is specified when the object is created.
        /// </summary>
        public const double DefaultSize = 1.0;
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
