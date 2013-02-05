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

namespace Managed3D.Modeling.Modifiers.Topology
{
    /// <summary>
    /// Represents a modifier that operates on the topology of a model.
    /// </summary>
    public abstract class TopologyModifier : Modifier
    {
        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="SelectionTarget"/> that describes what type of topology is appropriate for the modifier to operate on.
        /// </summary>
        public virtual SelectionTarget OperatesOn { get; set; }
        #endregion
    }
}
