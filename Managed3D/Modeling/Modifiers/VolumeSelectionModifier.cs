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
    public class VolumeSelectionModifier : Modifier, ISelectionModifier
    {
        #region Constructors
        public VolumeSelectionModifier()
        {

        }
        #endregion
        #region Properties
        public BoundingVolume SelectionVolume { get; set; }
        public SelectionAction Action { get; set; }
        public SelectionTarget Targets { get; set; }
        #endregion
        #region Methods
        public override void Apply(Model model)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
