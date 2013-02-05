/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Managed3D.Geometry;

namespace Managed3D.Modeling.Modifiers.Topology
{
    /// <summary>
    /// Provides a mesh topology modifier that slices all faces and edges which pass through a given slice plane.
    /// </summary>
    public class SliceModifier : TopologyModifier
    {
        #region Fields
        private Plane3 slicePlane;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SliceModifier"/> class.
        /// </summary>
        public SliceModifier()
        {
            this.OperatesOn = SelectionTarget.Mesh;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SliceModifier"/> class.
        /// </summary>
        /// <param name="slicePlane"></param>
        public SliceModifier(Plane3 slicePlane)
        {
            this.slicePlane = slicePlane;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="Plane3"/> used to slice the mesh on.
        /// </summary>
        public Plane3 SlicePlane
        {
            get
            {
                return this.slicePlane;
            }
            set
            {
                this.slicePlane = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to weld together vertices that are created by slicing the mesh along the slice plane.
        /// </summary>
        /// <remarks>
        /// If this value is false, when the mesh is sliced it will be divided into two noncontigous elements with duplicate vertices along
        /// the slice plane. 
        /// </remarks>
        public bool WeldVertices
        {
            get;
            set;
        }
        #endregion
        #region Methods
        public override void Apply(Model model)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
