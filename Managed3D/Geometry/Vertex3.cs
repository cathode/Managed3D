/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using Managed3D.Geometry;
using System;
using System.Diagnostics.Contracts;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents a vertex of a polygon in three-dimensional space.
    /// </summary>
    public class Vertex3
    {
        #region Fields
        private Vector3 position;
        private Vector3 normal;
        private Vector4f color;
        private Vector2 textureCoordinates;
        private VertexFlags flags;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex3"/> class.
        /// </summary>
        public Vertex3()
        {
            this.color = Vector4f.Color(0, 0, 0, 1);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex3"/> class.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vertex3(double x, double y, double z)
        {
            this.position = new Vector3(x, y, z);
            this.color = Vector4f.Color(0, 0, 0, 1);
        }
        #endregion
        #region Properties
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
        /// Gets or sets the x-coordinate of the vertex.
        /// </summary>
        public double X
        {
            get
            {
                return this.position.X;
            }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the vertex.
        /// </summary>
        public double Y
        {
            get
            {
                return this.position.Y;
            }
        }

        /// <summary>
        /// Gets or sets the z-coordinate of the vertex.
        /// </summary>
        public double Z
        {
            get
            {
                return this.position.Z;
            }
        }

        public Vector3 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

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

        public Vector2 TextureCoordinates
        {
            get
            {
                return this.textureCoordinates;
            }
            set
            {
                this.textureCoordinates = value;
            }
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", this.X, this.Y, this.Z);
        }
        #endregion
        #region Operators
        public static explicit operator Vector3(Vertex3 vertex)
        {
            Contract.Requires(vertex != null);

            return new Vector3(vertex.X, vertex.Y, vertex.Z);
        }

        public Vector3 ToVector3()
        {
            return new Vector3(this.X, this.Y, this.Z);
        }
        #endregion
    }
}
