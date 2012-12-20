/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Runtime.InteropServices;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Provides a 4-by-4 double-precision floating point matrix implementation. This type is immutable.
    /// </summary>
    /// <remarks>
    /// Values are internally represented using row-major ordering, and are addressed with A-D for rows and X-W for columns.
    /// <code>
    ///        Columns
    ///        
    ///   | X  | Y  | Z  | W  |
    /// --+----+----+----+----+
    /// A |  0 |  1 |  2 |  3 |
    /// --+----+----+----+----+
    /// B |  4 |  5 |  6 |  7 |   Rows
    /// --+----+----+----+----+
    /// C |  8 |  9 | 10 | 11 |
    /// --+----+----+----+----+
    /// D | 12 | 13 | 14 | 15 |
    /// </code>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 128, Pack = 8)]
    public struct Matrix4 : IEquatable<Matrix4>
    {
        #region Fields
        /// <summary>
        /// Holds the identity matrix.
        /// </summary>
        public static readonly Matrix4 Identity = new Matrix4(1, 0, 0, 0,
                                                              0, 1, 0, 0,
                                                              0, 0, 1, 0,
                                                              0, 0, 0, 1);
        /// <summary>
        /// Element at the first row, first column.
        /// </summary>
        [FieldOffset(0x00)]
        private readonly double ax;

        /// <summary>
        /// Element at the first row, second column.
        /// </summary>
        [FieldOffset(0x08)]
        private readonly double ay;

        /// <summary>
        /// Element at the first row, third column.
        /// </summary>
        [FieldOffset(0x10)]
        private readonly double az;

        /// <summary>
        /// Element at the first row, fourth column.
        /// </summary>
        [FieldOffset(0x18)]
        private readonly double aw;

        /// <summary>
        /// Element at the second row, first column.
        /// </summary>
        [FieldOffset(0x20)]
        private readonly double bx;

        /// <summary>
        /// Element at the second row, second column.
        /// </summary>
        [FieldOffset(0x28)]
        private readonly double by;

        /// <summary>
        /// Element at the second row, third column.
        /// </summary>
        [FieldOffset(0x30)]
        private readonly double bz;

        /// <summary>
        /// Element at the second row, fourth column.
        /// </summary>
        [FieldOffset(0x38)]
        private readonly double bw;

        /// <summary>
        /// Element at the third row, first column.
        /// </summary>
        [FieldOffset(0x40)]
        private readonly double cx;

        /// <summary>
        /// Element at the third row, second column.
        /// </summary>
        [FieldOffset(0x48)]
        private readonly double cy;

        /// <summary>
        /// Element at the third row, third column.
        /// </summary>
        [FieldOffset(0x50)]
        private readonly double cz;

        /// <summary>
        /// Element at the third row, fourth column.
        /// </summary>
        [FieldOffset(0x58)]
        private readonly double cw;

        /// <summary>
        /// Element at the fourth row, first column.
        /// </summary>
        [FieldOffset(0x60)]
        private readonly double dx;

        /// <summary>
        /// Element at the fourth row, second column.
        /// </summary>
        [FieldOffset(0x68)]
        private readonly double dy;

        /// <summary>
        /// Element at the fourth row, third column.
        /// </summary>
        [FieldOffset(0x70)]
        private readonly double dz;

        /// <summary>
        /// Element at the fourth row, fourth column.
        /// </summary>
        [FieldOffset(0x78)]
        private readonly double dw;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix4"/> struct.
        /// </summary>
        /// <param name="a">The value of the first row.</param>
        /// <param name="b">The value of the second row.</param>
        /// <param name="c">The value of the third row.</param>
        /// <param name="d">The value of the fourth row.</param>
        public Matrix4(Vector4 a, Vector4 b, Vector4 c, Vector4 d)
        {
            this.ax = a.X;
            this.ay = a.Y;
            this.az = a.Z;
            this.aw = a.W;

            this.bx = b.X;
            this.by = b.Y;
            this.bz = b.Z;
            this.bw = b.W;

            this.cx = c.X;
            this.cy = c.Y;
            this.cz = c.Z;
            this.cw = c.W;

            this.dx = d.X;
            this.dy = d.Y;
            this.dz = d.Z;
            this.dw = d.W;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix4"/> class.
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="az"></param>
        /// <param name="aw"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="bz"></param>
        /// <param name="bw"></param>
        /// <param name="cx"></param>
        /// <param name="cz"></param>
        /// <param name="cy"></param>
        /// <param name="cw"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="dz"></param>
        /// <param name="dw"></param>
        public Matrix4(double ax, double ay, double az, double aw,
                       double bx, double by, double bz, double bw,
                       double cx, double cy, double cz, double cw,
                       double dx, double dy, double dz, double dw)
        {
            this.ax = ax;
            this.ay = ay;
            this.az = az;
            this.aw = aw;

            this.bx = bx;
            this.by = by;
            this.bz = bz;
            this.bw = bw;

            this.cx = cx;
            this.cy = cy;
            this.cz = cz;
            this.cw = cw;

            this.dx = dx;
            this.dy = dy;
            this.dz = dz;
            this.dw = dw;
        }
        #endregion
        #region Indexers
        public unsafe double this[int row, int col]
        {
            get
            {
                fixed (Matrix4* ptr = &this)
                    return *((double*)ptr + (((row * 4) + col) % 16));
            }
        }
        public unsafe double this[int index]
        {
            get
            {
                fixed (Matrix4* ptr = &this)
                    return *((double*)ptr + (index % 16));
            }
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the first row of the matrix.
        /// </summary>
        public Vector4 A
        {
            get
            {
                return new Vector4(ax, ay, az, aw);
            }
        }

        /// <summary>
        /// Gets the second row of the matrix.
        /// </summary>
        public Vector4 B
        {
            get
            {
                return new Vector4(bx, by, bz, bw);
            }
        }

        /// <summary>
        /// Gets the third row of the matrix.
        /// </summary>
        public Vector4 C
        {
            get
            {
                return new Vector4(cx, cy, cz, cw);
            }
        }

        /// <summary>
        /// Gets the fourth row of the matrix.
        /// </summary>
        public Vector4 D
        {
            get
            {
                return new Vector4(dx, dy, dz, dw);
            }
        }

        /// <summary>
        /// Gets the first column of the matrix.
        /// </summary>
        public Vector4 X
        {
            get
            {
                return new Vector4(ax, bx, cx, dx);
            }
        }

        /// <summary>
        /// Gets the second column of the matrix.
        /// </summary>
        public Vector4 Y
        {
            get
            {
                return new Vector4(ay, by, cy, dy);
            }
        }

        /// <summary>
        /// Gets the third column of the matrix.
        /// </summary>
        public Vector4 Z
        {
            get
            {
                return new Vector4(az, bz, cz, dz);
            }
        }

        /// <summary>
        /// Gets the fourth column of the matrix.
        /// </summary>
        public Vector4 W
        {
            get
            {
                return new Vector4(aw, bw, cw, dw);
            }
        }
        #endregion
        #region Methods
        #region Utility Methods
        /// <summary>
        /// Overridden. Determines whether the specified <see cref="System.Object"/>
        /// is equal to the current <see cref="Managed3D.Matrix4"/>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the
        /// current <see cref="Managed3D.Matrix4"/>.</param>
        /// <returns>true if the specified <see cref="System.Object"/> is equal to
        /// the current <see cref="Managed3D.Matrix4"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Matrix4)
                return this.Equals((Matrix4)obj);

            return false;
        }

        /// <summary>
        /// Determines whether two matrices are equal.
        /// </summary>
        /// <param name="other">The <see cref="Matrix4"/> to compare to.</param>
        /// <returns>true if the specified <see cref="Matrix4"/> is equal to
        /// the current <see cref="Matrix4"/>, otherwise false.</returns>
        public bool Equals(Matrix4 other)
        {
            return this == other;
        }

        /// <summary>
        /// Determines if two matrices are equal.
        /// </summary>
        /// <param name="a">The first <see cref="Matrix4"/> to compare.</param>
        /// <param name="b">The second <see cref="Matrix4"/> to compare.</param>
        /// <returns>true if both matrices have the same values, otherwise false.</returns>
        public static bool Equals(Matrix4 a, Matrix4 b)
        {
            return a == b;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
        #region Arithmetic Methods
        /// <summary>
        /// Adds the current matrix and the specified matrix.
        /// </summary>
        /// <param name="other"></param>
        public Matrix4 Add(Matrix4 other)
        {
            return Matrix4.Add(this, other);
        }

        /// <summary>
        /// Adds two specified <see cref="Matrix4"/> instances and returns the result as a new instance.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Matrix4 Add(Matrix4 left, Matrix4 right)
        {
            return new Matrix4(left.ax + right.ax, left.ay + right.ay, left.az + right.az, left.aw + right.aw,
                               left.bx + right.bx, left.by + right.by, left.bz + right.bz, left.bw + right.bw,
                               left.cx + right.cx, left.cy + right.cy, left.cz + right.cz, left.cw + right.cw,
                               left.dx + right.dx, left.dy + right.dy, left.dz + right.dz, left.dw + right.dw);
        }

        public Matrix4 Multiply(Matrix4 other)
        {
            return Matrix4.Multiply(this, other);
        }

        public static Matrix4 Multiply(Matrix4 left, Matrix4 right)
        {
            return new Matrix4(left.ax * right.ax + left.ay * right.bx + left.az * right.cx + left.aw * right.dx,
                               left.ax * right.ay + left.ay * right.by + left.az * right.cy + left.aw * right.dy,
                               left.ax * right.az + left.ay * right.bz + left.az * right.cz + left.aw * right.dz,
                               left.ax * right.aw + left.ay * right.bw + left.az * right.cw + left.aw * right.dw,

                               left.bx * right.ax + left.by * right.bx + left.bz * right.cx + left.bw * right.dx,
                               left.bx * right.ay + left.by * right.by + left.bz * right.cy + left.bw * right.dy,
                               left.bx * right.az + left.by * right.bz + left.bz * right.cz + left.bw * right.dz,
                               left.bx * right.aw + left.by * right.bw + left.bz * right.cw + left.bw * right.dw,

                               left.cx * right.ax + left.cy * right.bx + left.cz * right.cx + left.cw * right.dx,
                               left.cx * right.ay + left.cy * right.by + left.cz * right.cy + left.cw * right.dy,
                               left.cx * right.az + left.cy * right.bz + left.cz * right.cz + left.cw * right.dz,
                               left.cx * right.aw + left.cy * right.bw + left.cz * right.cw + left.cw * right.dw,

                               left.dx * right.ax + left.dy * right.bx + left.dz * right.cx + left.dw * right.dx,
                               left.dx * right.ay + left.dy * right.by + left.dz * right.cy + left.dw * right.dy,
                               left.dx * right.az + left.dy * right.bz + left.dz * right.cz + left.dw * right.dz,
                               left.dx * right.aw + left.dy * right.bw + left.dz * right.cw + left.dw * right.dw);

        }

        public Matrix4 Multiply(double s)
        {
            return Matrix4.Multiply(this, s);
        }

        /// <summary>
        /// Calculates the scalar product of a specified matrix and scalar value.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Matrix4 Multiply(Matrix4 m, double s)
        {
            return new Matrix4(m.ax * s, m.ay * s, m.az * s, m.aw * s,
                               m.bx * s, m.by * s, m.bz * s, m.bw * s,
                               m.cx * s, m.cy * s, m.cz * s, m.cw * s,
                               m.dx * s, m.dy * s, m.dz * s, m.dw * s);

        }
        public Vector3 Multiply(Vector3 vector)
        {
            return Matrix4.Multiply(this, vector);
        }
        public static Vector3 Multiply(Matrix4 matrix, Vector3 vector)
        {
            var v4 = matrix * new Vector4(vector.X, vector.Y, vector.Z, 1.0);
            return new Vector3(v4.X / v4.W, v4.Y / v4.W, v4.Z / v4.W);
        }

        public Vector4 Multiply(Vector4 v)
        {
            return Matrix4.Multiply(this, v);
        }

        public static Vector4 Multiply(Matrix4 m, Vector4 v)
        {
            return new Vector4(m.ax * v.X + m.ay * v.Y + m.az * v.Z + m.aw * v.W,
                               m.bx * v.X + m.by * v.Y + m.bz * v.Z + m.bw * v.W,
                               m.cx * v.X + m.cy * v.Y + m.cz * v.Z + m.cw * v.W,
                               m.dx * v.X + m.dy * v.Y + m.dz * v.Z + m.dw * v.W);
        }

        public Matrix4 Subtract(Matrix4 other)
        {
            return Matrix4.Subtract(this, other);
        }

        public static Matrix4 Subtract(Matrix4 a, Matrix4 b)
        {
            return new Matrix4(a.ax - b.ax, a.ay - b.ay, a.az - b.az, a.aw - b.aw,
                               a.bx - b.bx, a.by - b.by, a.bz - b.bz, a.bw - b.bw,
                               a.cx - b.cx, a.cy - b.cy, a.cz - b.cz, a.cw - b.cw,
                               a.dx - b.dx, a.dy - b.dy, a.dz - b.dz, a.dw - b.dw);
        }
        /// <summary>
        /// Transposes the current matrix and returns the result as a new instance.
        /// </summary>
        /// <returns>A new <see cref="Matrix4"/> instance that is the result of the transposition.</returns>
        public Matrix4 Transpose()
        {
            return Matrix4.Transpose(this);
        }

        /// <summary>
        /// Transposes the specified matrix and returns the result as a new instance.
        /// </summary>
        /// <param name="m">A <see cref="Matrix4"/> instance to transpose.</param>
        /// <returns>A new <see cref="Matrix4"/> instance that is the result of the transposition.</returns>
        public static Matrix4 Transpose(Matrix4 m)
        {
            return new Matrix4(m.ax, m.bx, m.cx, m.dx,
                               m.ay, m.by, m.cy, m.dy,
                               m.az, m.bz, m.cz, m.dz,
                               m.aw, m.bw, m.cw, m.dw);
        }
        #endregion

        /// <summary>
        /// Creates and returns an orthographic projection matrix for the specified width and height.
        /// </summary>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <returns>A <see cref="Matrix4"/> that is the orthographic projection matrix for the specified width and height.</returns>
        public static Matrix4 CreateOrthographicProjectionMatrix(double width, double height, double nearZ, double farZ)
        {
            //   return Matrix4.CreateOrthographicProjectionMatrix(width / 2.0, width / -2.0, height / -2.0, height / 2.0, nearZ, farZ);

            return new Matrix4(1, 0, 0, 0,
                               0, 1, 0, 0,
                               0, 0, 1, 0,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// Creates and returns an orthographic projection matrix for the specified set of clipping planes.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <param name="near"></param>
        /// <param name="far"></param>
        /// <returns></returns>
        public static Matrix4 CreateOrthographicProjectionMatrix(double left, double right, double top, double bottom, double near, double far)
        {
            return Matrix4.Identity;

            var x = 2.0 / (right - left);
            var y = 2.0 / (top - bottom);
            var z = -2.0 / (far - near);
            var a = (right + left) / (right - left) * -1.0;
            var b = (top + bottom) / (top - bottom) * -1.0;
            var c = (far + near) / (far - near) * -1.0;

            return new Matrix4(x, 0, 0, a,
                               0, y, 0, b,
                               0, 0, z, c,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// Creates and returns a perspective projection matrix for the specified values.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="nearZ">The distance to the near-z clipping plane.</param>
        /// <param name="farZ">The distance to the far-z clipping plane.</param>
        /// <returns>A <see cref="Matrix4"/> that is the perspective projection matrix for the specified values.</returns>
        public static Matrix4 CreatePerspectiveProjectionMatrix(Angle fov, double aspect, double nearZ, double farZ)
        {
            return Matrix4.Identity;

            var fv = fov.Degrees;
            //aspect = 840.0 / 600.0;
            nearZ = 1.0;
            farZ = 1000.0;
            var s = 1.0 / Math.Tan(fov.Degrees * 0.5 * (Math.PI / 180.0));

            return new Matrix4(s, 0, 0, 0,
                               0, s, 0, 0,
                               0, 0, -(farZ / (farZ - nearZ)), -1,
                               0, 0, -((farZ * nearZ) / (farZ - nearZ)), 0);
        }

        /// <summary>
        /// Creates and returns a new translation matrix
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Matrix4 CreateTranslationMatrix(Vector3 t)
        {

            return new Matrix4(1, 0, 0, t.X,
                               0, 1, 0, t.Y,
                               0, 0, 1, t.Z,
                               0, 0, 0, 1);
        }

        public static Matrix4 CreateTranslationMatrix(double x, double y, double z)
        {
            return new Matrix4(1, 0, 0, x,
                               0, 1, 0, y,
                               0, 0, 1, z,
                               0, 0, 0, 1);
        }

        public static Matrix4 CreateScalingMatrix(Vector3 s)
        {
            return new Matrix4(s.X, 0, 0, 0,
                               0, s.Y, 0, 0,
                               0, 0, s.Z, 0,
                               0, 0, 0, 1.0);
        }

        public static Matrix4 CreateScalingMatrix(double x, double y, double z)
        {
            return new Matrix4(x, 0, 0, 0,
                               0, y, 0, 0,
                               0, 0, z, 0,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a matrix that rotates around an axis.
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static Matrix4 CreateRotationMatrix(Angle angle, Vector3 axis)
        {
            var q = new Quaternion(axis, angle.Radians);

            return q.ToRotationMatrix();
        }
        public static Matrix4 CreateRotationMatrix(double deg, double x, double y, double z)
        {
            return Matrix4.CreateRotationMatrix(Angle.FromDegrees(deg), new Vector3(x, y, z));
        }
        public static Matrix4 CreateRotationMatrix(Vector3 vec)
        {
            return Matrix4.CreateRotationMatrix(vec.X, vec.Y, vec.Z);
        }
        /// <summary>
        /// Creates a new matrix that represents a Euler rotation.
        /// </summary>
        /// <param name="x">The rotation amount (in degress) on the X plane.</param>
        /// <param name="y">The rotation amount (in degrees) on the Y plane.</param>
        /// <param name="z">The rotation amount (in degrees) on the Z plane.</param>
        /// <returns>A new <see cref="Matrix4"/> instance containing the result of the rotation.</returns>
        public static Matrix4 CreateRotationMatrix(double x, double y, double z)
        {
            x = Angle.RadiansFromDegrees(x % 360.0);
            y = Angle.RadiansFromDegrees(y % 360.0);
            z = Angle.RadiansFromDegrees(z % 360.0);

            var cosX = Math.Cos(x);
            var sinX = Math.Sin(x);
            var cosY = Math.Cos(y);
            var sinY = Math.Sin(y);
            var cosZ = Math.Cos(z);
            var sinZ = Math.Sin(z);

            var X = new Matrix4(1, 0, 0, 0,
                                0, cosX, -sinX, 0,
                                0, sinX, cosX, 0,
                                0, 0, 0, 1);
            var Y = new Matrix4(cosY, 0, sinY, 0,
                                0, 1, 0, 0,
                                -sinY, 0, cosY, 0,
                                0, 0, 0, 1);
            var Z = new Matrix4(cosZ, -sinZ, 0, 0,
                                sinZ, cosZ, 0, 0,
                                0, 0, 1, 0,
                                0, 0, 0, 1);

            return Z * Y * X;
        }

        public static Matrix4 CreateRotationMatrix(Quaternion rotation)
        {
            return rotation.ToRotationMatrix();
        }
        #endregion
        #region Operators
        /// <summary>
        /// Calculates the sum of two matrices.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix4 operator +(Matrix4 left, Matrix4 right)
        {
            return Matrix4.Add(left, right);
        }

        /// <summary>
        /// Calculates the difference of two matrices.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix4 operator -(Matrix4 left, Matrix4 right)
        {
            return Matrix4.Subtract(left, right);
        }

        /// <summary>
        /// Determines if two matrices are equal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Matrix4 left, Matrix4 right)
        {
            if (Matrix4.ReferenceEquals(left, null) || Matrix4.ReferenceEquals(right, null))
                return false;

            return left.ax == right.ax
                && left.bx == right.bx
                && left.cx == right.cx
                && left.dx == right.dx

                && left.ay == right.ay
                && left.by == right.by
                && left.cy == right.cy
                && left.dy == right.dy

                && left.az == right.az
                && left.bz == right.bz
                && left.cz == right.cz
                && left.dz == right.dz

                && left.aw == right.aw
                && left.bw == right.bw
                && left.cw == right.cw
                && left.dw == right.dw;
        }

        /// <summary>
        /// Determines if two matrices are inequal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Matrix4 left, Matrix4 right)
        {
            return left.ax != right.ax
                || left.bx != right.bx
                || left.cx != right.cx
                || left.dx != right.dx

                || left.ay != right.ay
                || left.by != right.by
                || left.cy != right.cy
                || left.dy != right.dy

                || left.az != right.az
                || left.bz != right.bz
                || left.cz != right.cz
                || left.dz != right.dz

                || left.aw != right.aw
                || left.bw != right.bw
                || left.cw != right.cw
                || left.dw != right.dw;
        }

        /// <summary>
        /// Calculates the product of two matrices.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix4 operator *(Matrix4 left, Matrix4 right)
        {
            return Matrix4.Multiply(left, right);
        }

        /// <summary>
        /// Calculates the scalar product of a matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Matrix4 operator *(Matrix4 matrix, double scalar)
        {
            return Matrix4.Multiply(matrix, scalar);
        }

        /// <summary>
        /// Multiplies a matrix by a 3-dimensional vector.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3 operator *(Matrix4 matrix, Vector3 vector)
        {
            return Matrix4.Multiply(matrix, vector);
        }

        public static Vertex3 operator *(Matrix4 matrix, Vertex3 vertex)
        {
            var result = Matrix4.Multiply(matrix, vertex.Position);
            return new Vertex3(result.X, result.Y, result.Z)
            {
                Color = vertex.Color,
                Flags = vertex.Flags
            };
        }

        /// <summary>
        /// Multiplies a matrix by a 4-dimensional vector.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector4 operator *(Matrix4 matrix, Vector4 vector)
        {
            return Matrix4.Multiply(matrix, vector);
        }
        #endregion


    }
}
