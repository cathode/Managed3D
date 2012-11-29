/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;

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
            // Set up vector variables;
            Vector3 a, b, c, p, q;
            a = this.A.ToVector3();
            b = this.B.ToVector3();
            c = this.C.ToVector3();
            p = line.A;
            q = line.B;

            // Vector from A to C
            var ex = this.C.X - this.A.X;
            var ey = this.C.Y - this.A.Y;
            var ez = this.C.Z - this.A.Z;

            // Vector from A to B
            var fx = this.B.X - this.A.X;
            var fy = this.B.Y - this.A.Y;
            var fz = this.B.Z - this.A.Z;

            // Cross product of e and f
            var nx = (fy * ez) - (fz * ey);
            var ny = (fz * ex) - (fx * ez);
            var nz = (fx * ey) - (fy * ex);

            // Normalize
            var m = Math.Sqrt((nx * nx) + (ny * ny) + (nz * nz));
            nx /= m;
            ny /= m;
            nz /= m;

            // Ray direction vector
            var dx = line.B.X - line.A.X;
            var dy = line.B.Y - line.A.Y;
            var dz = line.B.Z - line.A.Z;

            m = Math.Sqrt((dx * dx) + (dy * dy) + (dz * dz));
            dx /= m;
            dy /= m;
            dz /= m;

            // Dot product of ray and normal, tells us if ray is pointing towards the triangle.
            m = (dx * nx) + (dy * ny) + (dz * nz);

            // Value of m determines the relationship of the vectors:
            // m < 0: Vectors are opposed
            // m == 0: Vectors are perpendicular
            // m > 0: Vectors point the same way
            if (m < 0)
            {
                var gx = this.A.X - line.A.X;
                var gy = this.A.Y - line.A.Y;
                var gz = this.A.Z - line.A.Z;

                var t = (gx * nx) + (gy * ny) + (gz * nz);

                if (t < 0)
                {
                    var k = t / m;

                    var sx = p.X + (dx * k);
                    var sy = p.Y + (dy * k);
                    var sz = p.Z + (dz * k);

                    return new Vector3(sx, sy, sz);
                }
            }

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
