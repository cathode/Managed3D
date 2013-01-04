/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents a polygon in two-dimensional space.
    /// </summary>
    public class Polygon2 : IEnumerable<Vertex2>
    {
        #region Fields
        /// <summary>
        /// Holds the actual vertices of the polygon.
        /// </summary>
        private readonly Vertex2[] vertices;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon2"/> class.
        /// </summary>
        /// <param name="vertices">The number of sides of the new polygon.</param>
        public Polygon2(int vertices)
        {
            Contract.Requires(vertices > 2);

            this.vertices = new Vertex2[vertices];
        }      
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon2"/> class.
        /// </summary>
        /// <param name="sides">The number of sides of the new polygon.</param>
        /// <param name="radius">The radius of the new polygon.</param>
        public Polygon2(int sides, double radius)
            : this(sides, radius, RadiusMode.Vertex)
        {
        }    
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon2"/> class.
        /// </summary>
        /// <remarks>
        /// Creates a regular polygon.
        /// </remarks>
        /// <param name="sides">The number of sides of the new polygon.</param>
        /// <param name="radius">The radius of the new polygon.</param>
        /// <param name="mode">The <see cref="RadiusMode"/> that indicates how the <paramref name="radius"/> parameter is interpreted.</param>
        public Polygon2(int sides, double radius, RadiusMode mode)
        {
            this.vertices = new Vertex2[sides];
            for (int s = 0; s < sides; s++)
            {
                double a = ((2 * Math.PI) / sides) * s;
                this[s] = new Vertex2(Math.Sin(a) * radius, Math.Cos(a) * radius);
            }
        }       
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon2"/> class.
        /// </summary>
        /// <param name="vertices"></param>
        public Polygon2(params Vertex2[] vertices)
        {
            if (vertices.Length < 3)
                throw new ArgumentException("Polygons must contain at least 3 vertices.", "vertices");
            this.vertices = vertices;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the number of sides of the current polygon.
        /// </summary>
        public int Sides
        {
            get
            {
                return this.vertices.Length;
            }
        }
        #endregion
        #region Indexers
        /// <summary>
        /// Gets or sets a <see cref="Vector2"/> representing the vertex with the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Vertex2 this[int index]
        {
            get
            {
                return this.vertices[index];
            }
            set
            {
                this.vertices[index] = value;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets the enumerator for the current instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Vertex2> GetEnumerator()
        {
            for (int i = 0; i < this.vertices.Length; i++)
            {
                yield return this.vertices[i];
            }
        }    
        
        /// <summary>
        /// Gets the enumerator for the current instance.
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
