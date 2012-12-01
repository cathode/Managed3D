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
using Managed3D.Geometry;
using System.Diagnostics.Contracts;

namespace Managed3D.Modeling.Primitives
{
    public class Box : ProceduralMesh
    {
        #region Fields
        //private
        #endregion
        #region Constructors
        public Box(double x, double y, double z)
        {

        }
        #endregion
        #region Properties

        #endregion
        #region Methods

        #endregion

        public override void RecalculateMesh()
        {
            throw new NotImplementedException();
        }

        protected override void ApplyParameters()
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<ProcedureParameter> InitializeParameters()
        {
            throw new NotImplementedException();
        }
    }
}
