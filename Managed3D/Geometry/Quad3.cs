/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents a quad in 3D-space, in other words a four sided planar polygon.
    /// </summary>
    public class Quad3 : Polygon3
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Quad3"/> class.
        /// </summary>
        public Quad3()
            : base(4)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quad3"/> class.
        /// </summary>
        /// <param name="radius">The radius of the new <see cref="Quad3"/>.</param>
        public Quad3(double radius)
            : base(4, radius)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quad3"/> class.
        /// </summary>
        /// <param name="radius">The radius of the new <see cref="Quad3"/>.</param>
        /// <param name="mode">The <see cref="RadiusMode"/> that describes how the value of the radius parameter is interpreted.</param>
        public Quad3(double radius, RadiusMode mode)
            : base(4, radius, mode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quad3"/> class.
        /// </summary>
        /// <param name="a">The top-left vertex of the quad.</param>
        /// <param name="b">The top-right vertex of the quad.</param>
        /// <param name="c">The bottom-right vertex of the quad.</param>
        /// <param name="d">The bottom-left vertex of the quad.</param>
        public Quad3(Vertex3 a, Vertex3 b, Vertex3 c, Vertex3 d)
            : base(a, b, c, d)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Quad3"/> class.
        /// </summary>
        /// <param name="verts"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        public Quad3(Vertex3[] verts, int a, int b, int c, int d)
            : base(verts[a], verts[b], verts[c], verts[d])
        {
        }

        public Quad3(Edge3[] edges, int a, int b, int c, int d)
            : base(edges, a, b, c, d)
        {

        }

        public Quad3(double width, double height)
        {
            var x1 = width / -2.0;
            var x2 = width / 2.0;
            var y1 = height / -2.0;
            var y2 = height / 2.0;

            this.A = new Vertex3(x1, y1, 0.0);
            this.B = new Vertex3(x1, y2, 0.0);
            this.C = new Vertex3(x2, y2, 0.0);
            this.D = new Vertex3(x2, y1, 0.0);
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the top-left vertex of the quad.
        /// </summary>
        public Vertex3 A
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
        /// Gets or sets the top-right vertex of the quad.
        /// </summary>
        public Vertex3 B
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
        /// Gets or sets the bottom-right vertex of the quad.
        /// </summary>
        public Vertex3 C
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

        /// <summary>
        /// Gets or sets the bottom-left vertex of the quad.
        /// </summary>
        public Vertex3 D
        {
            get
            {
                return this[3];
            }
            set
            {
                this[3] = value;
            }
        }

        public Edge3 AB
        {
            get
            {
                return this.edges[0];
            }
        }

        public Edge3 BC
        {
            get
            {
                return this.edges[1];
            }
        }

        public Edge3 CD
        {
            get
            {
                return this.edges[2];
            }
        }
        public Edge3 DA
        {
            get
            {
                return this.edges[3];
            }
        }

        /// <summary>
        /// Overridden. Returns the primitive kind.
        /// </summary>
        public override PrimitiveKind Kind
        {
            get
            {
                return PrimitiveKind.Quad;
            }
        }
        #endregion
    }
}
