using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents a quaternion.
    /// </summary>
    public class Quaternion : IVector4
    {
        #region Fields
        private double x, y, z, w;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Quaternion"/> class.
        /// </summary>
        /// <param name="angle">The angle of rotation, in radians.</param>
        /// <param name="axis">The axis about which to rotate.</param>
        public Quaternion(double angle, Vector3 axis)
        {
            this.x = axis.X;
            this.y = axis.Y;
            this.z = axis.Z;
            this.w = angle;
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
        #endregion
        #region Methods
        public Vector4 ToVector4()
        {
            return new Vector4(this.X, this.Y, this.Z, this.W);
        }

        public Vector3 ToVector3()
        {
            throw new NotImplementedException();
        }

        public Vector2 ToVector2()
        {
            throw new NotImplementedException();
        }

        public Matrix4 ToRotationMatrix()
        {
            var x = this.x;
            var y = this.y;
            var z = this.z;
            var s = this.w;
            var matrix = new Matrix4(
                1 - 2 * ((y * y) + (z * z)), 2 * ((x * y) - (s * z)), 2 * ((x * z) + (s * y)), 0,
                2 * ((x * y) + (s * z)), 1 - 2 * ((x * x) + (z * z)), 2 * ((y * z) - (s * x)), 0,
                2 * ((x * z) - (s * y)), 2 * ((y * z) + (s * x)), 1 - 2 * ((x * x) + (y * y)), 0,
                0, 0, 0, 0);

            return matrix;
        }
        #endregion
    }
}
