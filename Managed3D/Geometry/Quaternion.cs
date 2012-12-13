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
        private double w;
        private double x;
        private double y;
        private double z;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Quaternion"/> class.
        /// </summary>
        /// <param name="angle">The angle of rotation, in degrees.</param>
        /// <param name="axis">The axis about which to rotate.</param>
        public Quaternion(double angle, Vector3 axis)
        {
            angle = Managed3D.Geometry.Angle.RadiansFromDegrees(angle);

            double res = Math.Sin(angle / 2.0);
            var w = Math.Cos(angle / 2.0);
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
        public double W
        {
            get
            {
                return this.w;
            }
        }
        public double X
        {
            get
            {
                return this.x;
            }
        }

        public double Y
        {
            get
            {
                return this.y;
            }
        }

        public double Z
        {
            get
            {
                return this.z;
            }
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(w * w + x * x + y * y + z * z);
            }
        }
        #endregion
        #region Methods
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
            if (this.Length != 1)
                this.Normalize();

            var x = this.x;
            var y = this.y;
            var z = this.z;
            var s = this.w;
            var matrix = new Matrix4(
                1 - 2 * ((y * y) + (z * z)), 2 * ((x * y) - (s * z)), 2 * ((x * z) + (s * y)), 0,
                2 * ((x * y) + (s * z)), 1 - 2 * ((x * x) + (z * z)), 2 * ((y * z) - (s * x)), 0,
                2 * ((x * z) - (s * y)), 2 * ((y * z) + (s * x)), 1 - 2 * ((x * x) + (y * y)), 0,
                0, 0, 0, 1);

            return matrix;
        }

        public void Normalize()
        {
            var m = Math.Sqrt(w * w + x * x + y * y + z * z);
            this.w = w / m;
            this.x = x / m;
            this.y = y / m;
            this.z = z / m;
        }

        public void Rotate(double degrees)
        {
            Vector3 axis;
            double angle;
            this.GetAxisAngle(out axis, out angle);
            var rq = new Quaternion(degrees, axis);

            var q = rq * this;
            this.w = q.w;
            this.x = q.x;
            this.y = q.y;
            this.z = q.z;
        }

        public Vector3 FindAxis()
        {
            var angle = Math.Acos(this.w);
            var tinv = 1.0 / Math.Sin(angle);

            return new Vector3(this.x * tinv, this.y * tinv, this.z * tinv);
        }

        public Angle FindAngle()
        {
            return Angle.FromRadians(Math.Acos(this.w) * 2);
        }
        public Quaternion GetConjugate()
        {
            return new Quaternion(w, -x, -y, -z);
        }

        public void GetAxisAngle(out Vector3 axis, out double angle)
        {
            var scale = Math.Sqrt(w * w + x * x + y * y + z * z);

            axis = new Vector3(x / scale, y / scale, z / scale);
            angle = Math.Acos(w) * 2.0;
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
