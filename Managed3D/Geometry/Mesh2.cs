/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;

using System.Text;

namespace Managed3D.Geometry
{
    /// <summary>
    /// An unstructured grid of two-dimensional polygons.
    /// </summary>
    public class Mesh2 : IEnumerable<Polygon2>
    {
        #region Fields
        private Polygon2[] polygons;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Mesh2"/> class.
        /// </summary>
        public Mesh2()
        {
            this.polygons = new Polygon2[0];
        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets the enumerator for the current instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Polygon2> GetEnumerator()
        {
            for (int i = 0; i < this.polygons.Length; i++)
            {
                yield return this.polygons[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
