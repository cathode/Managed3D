/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics.Contracts;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents a polygon in three-dimensional space. A polygon is a shape made up of vertices.
    /// </summary>
    public class Polygon3 : IEnumerable<Vertex3>
    {
        #region Fields
        /// <summary>
        /// Holds the vertices of the current <see cref="Polygon3"/>.
        /// </summary>
        private readonly Vertex3[] vertices;
        private readonly Edge3[] edges;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon3"/> class.
        /// </summary>
        /// <param name="edges">The number of edges of the new polygon.</param>
        public Polygon3(int edges)
        {
            Contract.Requires(edges > 2);

            this.vertices = new Vertex3[edges];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon3"/> class.
        /// </summary>
        /// <param name="sides">The number of sides of the new polygon.</param>
        /// <param name="radius">The radius of the new polygon.</param>
        /// <remarks>
        /// Assumes <see cref="RadiusMode.Vertex"/>.
        /// </remarks>
        public Polygon3(int sides, double radius)
            : this(sides, radius, RadiusMode.Vertex)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon3"/> class as a regular polygon with the specified radius.
        /// </summary>
        /// <param name="sides">The number of sides of the new polygon.</param>
        /// <param name="radius">The radius of the new polygon.</param>
        /// <param name="mode">The <see cref="RadiusMode"/> that describes how the radius value is interpreted.</param>
        public Polygon3(int sides, double radius, RadiusMode mode)
        {
            Contract.Requires(sides > 2);

            var m = Matrix4.CreateRotationMatrix(Angle.FromDegrees(360.0 / sides), new Vector3(0, 1, 0));

            Vector3 v = new Vector3(radius, 0, 0);

            if (mode == RadiusMode.Edge)
            {
                var rm = Matrix4.CreateRotationMatrix(Angle.FromDegrees(360.0 / (sides * 2)), new Vector3(0, 1, 0));
                v = rm * v;
            }

            this.vertices = new Vertex3[sides];
            this.edges = new Edge3[sides];

            for (int i = sides - 1; i >= 0; i--)
            {
                this.vertices[i] = new Vertex3(v.X, v.Y, v.Z);
                v = m * v;
            }

            this.RegenerateEdges();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon3"/> class from the specified vertices.
        /// </summary>
        /// <param name="vertices">A collection of vertices to use for the new polygon.</param>
        public Polygon3(params Vertex3[] vertices)
        {
            if (vertices == null)
                throw new ArgumentNullException("vertices");
            if (vertices.Length < 3)
                throw new ArgumentException("Vertices contains less than 3 elements.", "vertices");
            this.vertices = vertices;
            this.edges = new Edge3[vertices.Length];
            this.RegenerateEdges();
        }

        public Polygon3(Vertex3[] verts, params int[] indices)
        {
            this.vertices = new Vertex3[indices.Length];
            this.edges = new Edge3[indices.Length];

            for (int i = 0; i < indices.Length; i++)
            {
                this.vertices[i] = verts[indices[i]];
            }

            this.RegenerateEdges();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets a value indicating whether the vertices of the current <see cref="Polygon3"/> lie on the same plane in 3d-space.
        /// </summary>
        public virtual bool IsPlanar
        {
            get
            {
                if (this.Vertices.Length == 3)
                    return true;
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the <see cref="PrimitiveKind"/> of the current <see cref="Polygon3"/>.
        /// </summary>
        public virtual PrimitiveKind Kind
        {
            get
            {
                return PrimitiveKind.Polygon;
            }
        }

        /// <summary>
        /// Gets the surface normal of the current <see cref="Polygon3"/>.
        /// </summary>
        public virtual Vector3 Normal
        {
            get
            {
                return Vector3.CrossProduct((Vector3)this.vertices[1] - (Vector3)this.vertices[0], (Vector3)this.vertices[2] - (Vector3)this.vertices[0]).Normalize();
            }
        }

        public Vertex3[] Vertices
        {
            get
            {
                return this.vertices;
            }
        }

        public IEnumerable<Edge3> Edges
        {
            get
            {
                return this.edges;
            }
        }
        #endregion
        #region Indexers
        /// <summary>
        /// Gets or sets the <see cref="Vertex3"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the <see cref="Vertex3"/> to access.</param>
        /// <returns>The <see cref="Vertex3"/> at the specified index.</returns>
        public Vertex3 this[int index]
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
        public void RegenerateEdges()
        {
            var m = this.vertices.Length;

            for (int i = 0; i < m; ++i)
                this.edges[i] = new Edge3(this.vertices[i], this.vertices[(i + 1) % m]);

        }

        /// <summary>
        /// Returns an enumerator for the current <see cref="Polygon3"/>.
        /// </summary>
        /// <returns>An enumerator that allows enumeration of the vertices of the current <see cref="Polygon3"/>.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator for the current <see cref="Polygon3"/>. 
        /// </summary>
        /// <returns>An enumerator that allows enumeration of the vertices of the current <see cref="Polygon3"/>.</returns>
        public IEnumerator<Vertex3> GetEnumerator()
        {
            for (int i = 0; i < this.vertices.Length; i++)
            {
                yield return this.vertices[i];
            }
        }

        /// <summary>
        /// Decomposes the current polygon into triangles.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<Triangle3> Decompose()
        {
            var a = (from v in this.Vertices
                     orderby v.X
                     select v).First();

            yield return new Triangle3(this.vertices[0], this.vertices[1], this.vertices[2]);
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Finds and returns a <see cref="Vector3"/> that describes the position of the incenter of the current <see cref="Polygon3"/>.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetIncenter()
        {
            var sum = new Vector3(0, 0, 0);
            foreach (var vt in this.vertices)
            {
                sum += vt;
            }

            return sum / this.vertices.Length;
        }

        /// <summary>
        /// Translates all vertices of the polygon by the specified x, y, and z values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Translate(double x, double y, double z)
        {
            foreach (var v in this.vertices)
            {
                v.X += x;
                v.Y += y;
                v.Z += z;
            }
        }

        /// <summary>
        /// Translates all the vertices of the polygon by the specified vector amount.
        /// </summary>
        /// <param name="value"></param>
        public void Translate(Vector3 value)
        {
            this.Translate(value.X, value.Y, value.Z);
        }

        /// <summary>
        /// Inverts direction that the polygon faces by reversing the order of the vertex list.
        /// </summary>
        public void Flip()
        {
            Array.Reverse(this.vertices);
        }
        #endregion
    }
}
