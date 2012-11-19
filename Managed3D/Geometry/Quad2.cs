/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents a quadrilateral polygon in two-dimensional space.
    /// </summary>
    public class Quad2 : Polygon2
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Quad2"/> class.
        /// </summary>
        public Quad2()
            : base(4)
        {
        }     
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Quad2"/> class.
        /// </summary>
        /// <param name="size">The width and height of the new quad.</param>
        public Quad2(double size)
            : this(size, size)
        {
        }  
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Quad2"/> class.
        /// </summary>
        /// <param name="width">The width of the quad.</param>
        /// <param name="height">The height of the quad.</param>
        public Quad2(double width, double height)
            : base(4)
        {
            var w = width / 2.0;
            var h = height / 2.0;
            var xa = -w;
            var xb = w;
            var ya = -h;
            var yb = h;            this[0] = new Vertex2(xa, ya);
            this[1] = new Vertex2(xb, ya);
            this[2] = new Vertex2(xb, yb);
            this[3] = new Vertex2(xa, yb);
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the upper-left vertex of the quad.
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
        /// Gets or sets the upper-right vertex of the quad.
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
        /// Gets or sets the lower-right vertex of the quad.
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
        
        /// <summary>
        /// Gets or sets the lower-left vertex of the quad.
        /// </summary>
        public Vertex2 D
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
        #endregion
    }
}
