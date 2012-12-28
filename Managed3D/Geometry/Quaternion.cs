using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents a quaternion.
    /// </summary>
    public class Quaternion
    {
        #region Fields
        public static readonly Quaternion Identity = new Quaternion(1.0, 0.0, 0.0, 0.0);
        private readonly double w;
        private readonly double x;
        private readonly double y;
        private readonly double z;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Quaternion"/> class.
        /// </summary>
        /// <param name="axis">The axis about which to rotate.</param>
        /// <param name="angle">The angle of rotation, in radians.</param>
        public Quaternion(Vector3 axis, double angle)
            : this(axis, Angle.FromRadians(angle))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quaterion"/> class.
        /// </summary>
        /// <param name="axis">A <see cref="Vector3"/> that describes the axis of the rotation.</param>
        /// <param name="angle"></param>
        public Quaternion(Vector3 axis, Angle angle)
        {
            var rads = angle.Radians;

            axis = axis.Normalize();

            double res = Math.Sin(rads / 2.0);
            var w = Math.Cos(rads / 2.0);
            var x = axis.X * res;
            var y = axis.Y * res;
            var z = axis.Z * res;

            var len = Math.Sqrt(w * w + x * x + y * y + z * z);
            w = w / len;
            x = x / len;
            y = y / len;
            z = z / len;

            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quaternion"/> class.
        /// </summary>
        /// <param name="thetaX"></param>
        /// <param name="thetaY"></param>
        /// <param name="thetaZ"></param>
        public Quaternion(double thetaX, double thetaY, double thetaZ)
        {
            var cx = Math.Cos(0.5 * thetaX);
            var cy = Math.Cos(0.5 * thetaY);
            var cz = Math.Cos(0.5 * thetaZ);

            var sx = Math.Sin(0.5 * thetaX);
            var sy = Math.Sin(0.5 * thetaY);
            var sz = Math.Sin(0.5 * thetaZ);

            this.w = (cz * cy * cx) + (sz * sy * sx);
            this.x = (cz * cy * sx) - (sz * sy * cx);
            this.y = (cz * sy * cx) + (sz * cy * sx);
            this.z = (sz * cy * cx) - (cz * sy * sx);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quaternion"/> class.
        /// </summary>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Quaternion(double w, double x, double y, double z)
        {
            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the W component of the quaternion.
        /// </summary>
        public double W
        {
            get
            {
                return this.w;
            }
        }

        /// <summary>
        /// Gets the X component of the quaternion.
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }
        }

        /// <summary>
        /// Gets the Y component of the quaternion.
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }
        }

        /// <summary>
        /// Gets the Z component of the quaternion.
        /// </summary>
        public double Z
        {
            get
            {
                return this.z;
            }
        }

        /// <summary>
        /// Gets the length (magnitude) of the quaternion.
        /// </summary>
        public double Length
        {
            get
            {
                return Math.Sqrt(w * w + x * x + y * y + z * z);
            }
        }
        #endregion
        #region Methods
        public static Quaternion Create(double x, double y, double z, double a)
        {
            throw new NotImplementedException();
        }

        public static Quaternion LookAt(Vector3 lookAt)
        {
            var up = Vector3.Up;
            var forward = lookAt;
            Vector3.OrthoNormalize(ref forward, ref up);
            var right = Vector3.CrossProduct(up, forward);

            var w = 1.0 + right.X + up.Y + forward.Z;

            w = Math.Sqrt(w) * 0.5;


            var w4Recip = 1.0 / (4.0 * w);
            var x = (up.Z - forward.Y) * w4Recip;
            var y = (forward.X - right.Z) * w4Recip;
            var z = (right.Y - up.X) * w4Recip;

            return new Quaternion(w, x, y, z).Normalized();

        }

        public static Quaternion Multiply(Quaternion q1, Quaternion q2)
        {
            var w = q1.w * q2.w - q1.x * q2.x - q1.y * q2.y - q1.z * q2.z;
            var x = q1.w * q2.x + q1.x * q2.w + q1.y * q2.z - q1.z * q2.y;
            var y = q1.w * q2.y - q1.x * q2.z + q1.y * q2.w + q1.z * q2.x;
            var z = q1.w * q2.z + q1.x * q2.y - q1.y * q2.x + q1.z * q2.w;

            return new Quaternion(w, x, y, z);
        }

        public Matrix4 ToRotationMatrix()
        {
            var q = (this.Length == 1) ? this : this.Normalized();

            var x = q.x;
            var y = q.y;
            var z = q.z;
            var s = q.w;
            var matrix = new Matrix4(
                1 - 2 * ((y * y) + (z * z)), 2 * ((x * y) - (s * z)), 2 * ((x * z) + (s * y)), 0,
                2 * ((x * y) + (s * z)), 1 - 2 * ((x * x) + (z * z)), 2 * ((y * z) - (s * x)), 0,
                2 * ((x * z) - (s * y)), 2 * ((y * z) + (s * x)), 1 - 2 * ((x * x) + (y * y)), 0,
                0, 0, 0, 1);

            return matrix;
        }

        public Quaternion Normalized()
        {
            var m = Math.Sqrt(w * w + x * x + y * y + z * z);
            return new Quaternion(w / m, x / m, y / m, z / m);
        }


        public Quaternion RotateBy(double degrees)
        {
            var rq = new Quaternion(this.GetAxis(), degrees);

            var q = rq * this;
            return new Quaternion(q.w, q.x, q.y, q.z);
        }

        public Vector3 GetAxis()
        {
            if (this.w == 1.0)
                return Vector3.Up;

            var m = Math.Sqrt(1 - (this.w * this.w));

            return new Vector3(this.x / m, this.y / m, this.z / m);
        }

        public Angle GetAngle()
        {
            return Angle.FromRadians(2.0 * Math.Acos(this.w));
        }

        public Quaternion GetConjugate()
        {
            return new Quaternion(w, -x, -y, -z);
        }

        public void GetAxisAngle(out Vector3 axis, out Angle angle)
        {
            var a = 2.0 * Math.Acos(this.w);
            var m = Math.Sqrt(1 - (this.w * this.w));

            axis = new Vector3(this.x / m, this.y / m, this.z / m);
            angle = Angle.FromRadians(a);
        }
        #endregion
        #region Operators
        /// <summary>
        /// Multiplies two quaternions. Quaternion multiplication is not commutative, so order matters.
        /// </summary>
        /// <param name="q1"></param>
        /// <param name="q2"></param>
        /// <returns></returns>
        public static Quaternion operator *(Quaternion q1, Quaternion q2)
        {
            return Quaternion.Multiply(q1, q2);
        }
        #endregion
    }
}
