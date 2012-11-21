/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using Managed3D.Geometry;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents a vertex of a polygon in three-dimensional space.
    /// </summary>
    public sealed class Vertex3 : IVector3
    {
        #region Fields
        private double x;
        private double y;
        private double z;
        private Vector4f color;
        private VertexFlags flags;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex3"/> class.
        /// </summary>
        public Vertex3()
        {
            this.color = Vector4f.Color(0, 1, 1, 1);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex3"/> class.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vertex3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            this.color = Vector4f.Color(0, 1, 1, 1);
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
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the vertex.
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

        /// <summary>
        /// Gets or sets the z-coordinate of the vertex.
        /// </summary>
        public double Z
        {
            get
            {
                return this.z;
            }
            set
            {
                this.z = value;
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
            return new Vector3(vertex.X, vertex.Y, vertex.Z);
        }

        public Vector3 ToVector3()
        {
            throw new System.NotImplementedException();
        }

        Vector2 IVector2.ToVector2()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        
    }
}
