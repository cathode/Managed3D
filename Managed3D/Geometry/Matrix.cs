/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;

using System.Text;

namespace Managed3D
{
    /// <summary>
    /// Provides a matrix implementation.
    /// </summary>
    public sealed class Matrix
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> type with the specified number of rowCount and columnCount.
        /// </summary>
        /// <param name="rowCount">The number of rowCount the new <see cref="Matrix"/> will have. Must be non-zero.</param>
        /// <param name="columnCount">The number of columnCount the new <see cref="Matrix"/> will have. Must be non-zero.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if either <paramref name="rowCount"/> or <paramref name="columnCount"/> are 0.</exception>
        public Matrix(int rowCount, int columnCount)
        {
            if (rowCount < 1)
                throw new ArgumentOutOfRangeException("rowCount", "Row count must be nonzero.");
            else if (columnCount < 1)
                throw new ArgumentOutOfRangeException("columnCount", "Column count must be nonzero.");

            this.elements = new double[rowCount * columnCount];
            this.rowCount = rowCount;
            this.columnCount = columnCount;
        }
        #endregion
        #region Fields
        private readonly double[] elements;
        private readonly int rowCount;
        private readonly int columnCount;
        #endregion
        #region Indexers
        /// <summary>
        /// Gets the value within the current <see cref="Matrix"/> at the specified row and column intersection.
        /// </summary>
        /// <param name="row">The zero-based row index of the value to get or set.</param>
        /// <param name="column">The zero-based column index of the value to get or set.</param>
        /// <returns>The <see cref="Double"/> value at the specified row and column intersection.</returns>
        /// <remarks>
        /// Elements of a matrix are indexed starting from zero, not one as with normal matrices.
        /// </remarks>
        public double this[int row, int column]
        {
            get
            {
                return this.elements[(row * this.ColumnCount) + column]; // Row-major ordering
            }
            set
            {
                this.elements[(row * this.ColumnCount) + column] = value; // Row-major ordering
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Calculates the product of a <see cref="Matrix"/> and a scalar value. The result
        /// is returned as a new <see cref="Matrix"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Matrix Product(Matrix source, double scalar)
        {
            Matrix result = new Matrix(source.RowCount, source.ColumnCount);

            for (byte r = 0; r < source.RowCount; r++)
            {
                for (byte c = 0; c < source.ColumnCount; c++)
                {
                    result[r, c] = source[r, c] * scalar;
                }
            }

            return result;
        }
        /// <summary>
        /// Calculates the product of two <see cref="Matrix">matrices</see>. The result
        /// is returned as a new <see cref="Matrix"/>.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        /// <remarks>This implementation is faster than the common O(n^3), at worst case, the
        /// product of an m by n matrix times an n by c matrix is an O((m * n) + (n * c) + (m * c)) operation.</remarks>
        public static Matrix Product(Matrix first, Matrix second)
        {
            if (first == null)
                throw new ArgumentNullException("first");

            else if (second == null)
                throw new ArgumentNullException("second");

            else if (first.ColumnCount != second.RowCount)
                throw new ArgumentException("Column count on first matrix does not match row count on second matrix - behavior is undefined.");


            Matrix result = new Matrix(first.RowCount, second.ColumnCount);

            double[] columnProducts = new double[result.ColumnCount];
            double[] rowProducts = new double[result.RowCount];

            for (int r = 0; r < first.RowCount; r++)
            {
                rowProducts[r] = first[r, 0];
                for (int c = 1; c < first.ColumnCount; c++)
                {
                    double v = first[r, c];
                    if (v == 0.0)
                    {
                        rowProducts[r] = 0;
                        break;
                    }
                    else
                    {
                        rowProducts[r] *= v;
                    }
                }
            }

            for (int c = 0; c < second.ColumnCount; c++)
            {
                columnProducts[c] = second[0, c];
                for (int r = 1; r < second.RowCount; r++)
                {
                    double v = second[r, c];
                    if (v == 0.0)
                    {
                        columnProducts[c] = 0;
                        break;
                    }
                    else
                    {
                        columnProducts[c] *= v;
                    }
                }
            }

            for (int r = 0; r < result.RowCount; r++)
            {
                for (int c = 0; c < result.ColumnCount; c++)
                {
                    result[r, c] = columnProducts[c] + rowProducts[r];
                }
            }

            return result;
        }
        #endregion
        #region Operators
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a == null)
                throw new ArgumentNullException("a");
            else if (b == null)
                throw new ArgumentNullException("b");
            else if (a.RowCount != b.RowCount || a.ColumnCount != b.ColumnCount)
                throw new ArgumentException("Matrices of different dimensions cannot be added.");

            Matrix m = new Matrix(a.RowCount, a.ColumnCount);

            for (int i = 0; i < a.elements.Length; i++)
                m.elements[i] = a.elements[i] + b.elements[i];
            return m;
        }
        public static Matrix operator *(Matrix m, double scalar)
        {
            return Matrix.Product(m, scalar);
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the number of columns in the current <see cref="Matrix"/>.
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return this.columnCount;
            }
        }
        /// <summary>
        /// Indicates if the current <see cref="Matrix"/> is square.
        /// </summary>
        public bool IsSquare
        {
            get
            {
                return (this.ColumnCount == this.RowCount);
            }
        }
        /// <summary>
        /// Gets the number of rows in the current <see cref="Matrix"/>.
        /// </summary>
        public int RowCount
        {
            get
            {
                return this.rowCount;
            }
        }
        #endregion
    }
}
