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

namespace Managed3D.Modeling.Modifiers
{
    /// <summary>
    /// Represents a modifier applied to a model to transform or operate on the model.
    /// </summary>
    public abstract class Modifier
    {
        #region Properties
        /// <summary>
        /// Gets a value indicating if any of the modifier's values have changed.
        /// </summary>
        public bool IsDirty { get; set; }

        /// <summary>
        /// Gets or sets a user-visible name for the modifier instance to distinguish it from other like instances.
        /// </summary>
        public string Name { get; set; }
        #endregion
        #region Methods
        /// <summary>
        /// Applies the modifier to a model.
        /// </summary>
        /// <param name="model"></param>
        public abstract void Apply(Model model);
        #endregion
    }
}
