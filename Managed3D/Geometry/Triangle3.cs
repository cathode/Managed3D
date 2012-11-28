/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents a triangle in 3D-space, defined by three vertices.
    /// </summary>
    public class Triangle3 : Polygon3
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle3"/> class.
        /// </summary>
        /// <param name="radius"></param>
        public Triangle3(double radius)
            : base(3, radius)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle3"/> class.
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="mode"></param>
        public Triangle3(double radius, RadiusMode mode)
            : base(3, radius, mode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle3"/> class.
        /// </summary>
        /// <param name="a">The first vertex of the triangle.</param>
        /// <param name="b">The second vertex of the triangle.</param>
        /// <param name="c">The third vertex of the triangle.</param>
        public Triangle3(Vertex3 a, Vertex3 b, Vertex3 c)
            : base(a, b, c)
        {
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the first vertex position.
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
        /// Gets or sets the second vertex position.
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
        /// Gets or sets the third vertex position.
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

        public override PrimitiveKind Kind
        {
            get
            {
                return PrimitiveKind.Triangle;
            }
        }

        public override Vector3 Normal
        {
            get
            {
                return Vector3.CrossProduct((Vector3)this.B - (Vector3)this.A, (Vector3)this.C - (Vector3)this.A).Normalize();
            }
        }

        public override bool IsPlanar
        {
            get
            {
                return true;
            }
        }
        #endregion
        #region Methods
        public override bool Equals(object obj)
        {
            if (obj is Triangle3)
                return this == (Triangle3)obj;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", this.A, this.B, this.C);
        }
        /// <summary>
        /// Gets the position on the current polygon at which the specified edge intersects it.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public IVector3 GetIntersection(Edge3 line)
        {
            var e1 = this.B.ToVector3() - this.A.ToVector3();
            var e2 = this.C.ToVector3() - this.A.ToVector3();
            return null;
        }
        #endregion
        #region Operators
        public static bool operator ==(Triangle3 t1, Triangle3 t2)
        {
            return t1.A == t2.A && t1.B == t2.B && t1.C == t2.C;
        }

        public static bool operator !=(Triangle3 t1, Triangle3 t2)
        {
            return t1.A != t2.A || t1.B != t2.B || t1.C != t2.C;
        }
        #endregion
    }
}
