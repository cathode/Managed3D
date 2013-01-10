using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents the extents of a 3D object.
    /// </summary>
    public class Extents3
    {
        #region Fields
        public static readonly Extents3 Empty = new Extents3(0, 0, 0);
        private readonly Vector3 a;
        private readonly Vector3 b;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Extents3"/> class.
        /// </summary>
        public Extents3()
        {
            this.a = Vector3.Zero;
            this.b = Vector3.Zero;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents3"/> class.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="height"></param>
        public Extents3(double width, double length, double height)
        {
            var x1 = width / 2.0;
            var x2 = width / -2.0;
            var y1 = length / 2.0;
            var y2 = length / -2.0;
            var z1 = height / 2.0;
            var z2 = height / -2.0;

            this.a = new Vector3(x1, y1, z1);
            this.b = new Vector3(x2, y2, z2);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents3"/> class.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public Extents3(Vector3 a, Vector3 b)
        {
            this.a = a;
            this.b = b;
        }
        #endregion
        #region Properties
        public Vector3 A
        {
            get
            {
                return this.a;
            }
        }

        public Vector3 B
        {
            get
            {
                return this.b;
            }

        }

        public double Right
        {
            get
            {
                return this.a.X;
            }
        }

        public double Left
        {
            get
            {
                return this.b.X;
            }
        }

        public double Front
        {
            get
            {
                return this.a.Y;
            }
        }

        public double Back
        {
            get
            {
                return this.b.Y;
            }
        }

        public double Top
        {
            get
            {
                return this.a.Z;
            }
        }

        public double Bottom
        {
            get
            {
                return this.b.Z;
            }
        }
        #endregion
        #region Methods
        public Vector3 FindMidpoint()
        {
            return new Vector3((this.a.X + this.b.X) / 2.0, (this.a.Y + this.b.Y) / 2.0, (this.a.Z + this.b.Z) / 2.0);
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", this.A, this.B);
        }
        [ContractInvariantMethod]
        private void Invariants()
        {

        }
        #endregion
        #region Operators
        /// <summary>
        /// Determines if the current <see cref="Extents3"/> fully contains the space defined by the specified <see cref="Extents3"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool FullyContains(Extents3 other)
        {
            return this.Right >= other.Right
                && this.Top >= other.Top
                && this.Front >= other.Front
                && this.Left <= other.Left
                && this.Bottom <= other.Bottom
                && this.Back <= other.Back;
        }

        public static Extents3 operator |(Extents3 e1, Extents3 e2)
        {
            return new Extents3(
                new Vector3(
                    Math.Max(e1.Right, e2.Right),
                    Math.Max(e1.Front, e2.Front),
                    Math.Max(e1.Top, e2.Top)),
                new Vector3(
                    Math.Min(e1.Left, e2.Left),
                    Math.Min(e1.Back, e2.Back),
                    Math.Min(e1.Bottom, e2.Bottom)));

        }

        public static bool operator ==(Extents3 e1, Extents3 e2)
        {
            return (e1.A == e2.A) && (e1.B == e2.B);
        }

        public static bool operator !=(Extents3 e1, Extents3 e2)
        {
            return (e1.A != e2.A) || (e1.B != e2.B);
        }
        #endregion


    }
}
