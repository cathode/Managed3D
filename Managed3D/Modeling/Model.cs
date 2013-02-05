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
using Managed3D.Modeling.Modifiers;

namespace Managed3D.Modeling
{
    /// <summary>
    /// Represents an advanced mesh.
    /// </summary>
    public class Model
    {
        #region Fields
        /// <summary>
        /// Holds the list of modifiers that apply to the current <see cref="Model"/>.
        /// </summary>
        private readonly LinkedList<Modifier> modifiers;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        public Model()
        {
            this.modifiers = new LinkedList<Modifier>();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="IGeometrySource"/> that provides the base geometry which other modifiers operate on.
        /// </summary>
        public IGeometrySource BaseGeometry { get; set; }

        /// <summary>
        /// Gets or sets the current selection of the model.
        /// </summary>
        public Selection Selection { get; set; }

        public EditableMesh Geometry
        {
            get;
            private set;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Forces a full rebuild of the model and evaluates/applies all modifiers.
        /// </summary>
        public void RebuildModel()
        {
            if (this.BaseGeometry == null)
                return;

            this.Geometry = this.BaseGeometry.Generate();

            foreach (var mod in this.modifiers)
            {
                mod.IsDirty = true;
                mod.Apply(this);
            }
        }
        #endregion
    }
}
