/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents a vertex of a polygon in two-dimensional space.
    /// </summary>
    public sealed class Vertex2 : IVector2
    {
        #region Fields - Private
        /// <summary>
        /// Backing field for the <see cref="Vertex2.Color"/> property.
        /// </summary>
        private Vector4f color;
        
        /// <summary>
        /// Backing field for the <see cref="Vertex2.Flags"/> property.
        /// </summary>
        private VertexFlags flags;

        /// <summary>
        /// Backing field for the <see cref="Vertex2.X"/> property.
        /// </summary>
        private double x;       
        
        /// <summary>
        /// Backing field for the <see cref="Vertex2.Y"/> property.
        /// </summary>
        private double y;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex2"/> class.
        /// </summary>
        public Vertex2()
        {
        }      
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex2"/> class.
        /// </summary>
        /// <param name="x">The x-coordinate of the new vertex.</param>
        /// <param name="y">The y-coordinate of the new vertex.</param>
        public Vertex2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion
        #region Properties - Public
        /// <summary>
        /// Gets or sets the color of the vertex.
        /// </summary>
        public Vector4f Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

        public VertexFlags Flags
        {
            get
            {
                return this.flags;
            }
            set
            {
                this.flags = value;
            }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the current <see cref="Vertex2"/>.
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }     
        
        /// <summary>
        /// Gets or sets the y-coordinate of the current <see cref="Vertex2"/>.
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }
        #endregion
    }
}
