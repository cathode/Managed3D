/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Collections.ObjectModel;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents an unstructured grid in 3d-space comprised of polygons.
    /// </summary>
    public class Mesh3 : IEnumerable<Polygon3>
    {
        #region Fields
        private Polygon3[] polygons;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Mesh3"/> class.
        /// </summary>
        public Mesh3()
        {
            this.polygons = new Polygon3[0];
        }

        public Mesh3(params Polygon3[] polygons)
        {
            this.polygons = polygons;
        }
        #endregion
        #region Properties
        public Polygon3[] Polygons
        {
            get
            {
                return this.polygons;
            }
            set
            {
                this.polygons = value;
            }
        }

        #endregion
        #region Methods
        public IEnumerator<Polygon3> GetEnumerator()
        {
            throw new NotImplementedException();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
