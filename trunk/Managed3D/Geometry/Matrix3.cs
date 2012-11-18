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
    /// Represents a 3-by-3 double-precision floating point matrix. This type is immutable.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct Matrix3
    {
        #region Fields
        [FieldOffset(0x00)]
        private readonly double ax;
        [FieldOffset(0x08)]
        private readonly double ay;
        [FieldOffset(0x10)]
        private readonly double az;

        [FieldOffset(0x18)]
        private readonly double bx;
        [FieldOffset(0x20)]
        private readonly double by;
        [FieldOffset(0x28)]
        private readonly double bz;

        [FieldOffset(0x30)]
        private readonly double cx;
        [FieldOffset(0x38)]
        private readonly double cy;
        [FieldOffset(0x40)]
        private readonly double cz;
        #endregion
        #region Constructors
        public Matrix3(Vector3 a, Vector3 b, Vector3 c)
        {
            this.ax = a.X;
            this.ay = a.Y;
            this.az = a.Z;

            this.bx = b.X;
            this.by = b.Y;
            this.bz = b.Z;

            this.cx = c.X;
            this.cy = c.Y;
            this.cz = c.Z;
        }

        public Matrix3(double ax, double ay, double az,
                       double bx, double by, double bz,
                       double cx, double cy, double cz)
        {
            this.ax = ax;
            this.ay = ay;
            this.az = az;

            this.bx = bx;
            this.by = by;
            this.bz = bz;

            this.cx = cx;
            this.cy = cy;
            this.cz = cz;
        }
        #endregion
        #region Indexers
        /// <summary>
        /// Gets or sets the value at the specified row and column within the matrix.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public unsafe double this[int row, int col]
        {
            get
            {
                // Using unsafe code and pointers should speed things up a lot.
                // The modulo of the index is used to prevent overflow without branching logic.
                fixed (Matrix3* ptr = &this)
                    return *((double*)ptr + (row * 3 + col) % 9);
            }
        }


        public unsafe double this[int index]
        {
            get
            {
                fixed (Matrix3* ptr = &this)
                    return *((double*)ptr + (index % 9));
            }
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the value at column A, row 0.
        /// </summary>
        public double AX
        {
            get
            {
                return this.ax;
            }
        }

        /// <summary>
        /// Gets or sets the value at column A, row 1.
        /// </summary>
        public double AY
        {
            get
            {
                return this.ay;
            }
        }

        /// <summary>
        /// Gets or sets the value at column A, row 2.
        /// </summary>
        public double AZ
        {
            get
            {
                return this.az;
            }
        }

        /// <summary>
        /// Gets or sets the value at column B, row 0.
        /// </summary>
        public double BX
        {
            get
            {
                return this.bx;
            }
        }

        public double BY
        {
            get
            {
                return this.by;
            }
        }
        public double BZ
        {
            get
            {
                return this.bz;
            }
        }
        #endregion
        #region Methods
        #endregion
        #region Operators
        #endregion
    }
}
