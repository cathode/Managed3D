/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;

using System.Text;
using Managed3D.Geometry;

namespace Managed3D.SceneGraph
{
    /// <summary>
    /// Provides a node that contains geometry data which is rendered.
    /// </summary>
    public class GeometryNode : Node
    {
        #region Constructors - Public
        /// <summary>
        /// Initializes a new instance of the <see cref="GeometryNode"/> class.
        /// </summary>
        public GeometryNode()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GeometryNode"/> class.
        /// </summary>
        /// <param name="geometry"></param>
        public GeometryNode(Mesh3 geometry)
        {
            this.geometry = geometry;
        }
        #endregion
        #region Fields - Private
        private Mesh3 geometry;
        #endregion
        #region Properties - Public
        /// <summary>
        /// Gets or sets the renderable geometry.
        /// </summary>
        public virtual Mesh3 Geometry
        {
            get
            {
                return this.geometry;
            }
            set
            {
                this.geometry = value;
            }
        }
        #endregion
        #region Methods
        public override Vector3 GetExtents()
        {
            var ext = new Vector3(0, 0, 0);

            foreach (var poly in this.geometry)
            {
                foreach (var vt in poly.Vertices)
                {
                    ext = new Vector3((vt.X > ext.X) ? vt.X : ext.X,
                                      (vt.Y > ext.Y) ? vt.Y : ext.Y,
                                      (vt.Z > ext.Z) ? vt.Z : ext.Z);
                }
            }

            return ext;
        }
        #endregion
    }
}
