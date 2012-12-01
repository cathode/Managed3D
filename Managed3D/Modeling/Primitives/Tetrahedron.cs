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

namespace Managed3D.Modeling.Primitives
{
    public sealed class Tetrahedron : ProceduralMesh
    {
        #region Fields

        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Tetrahedron"/> class.
        /// </summary>
        public Tetrahedron()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tetrahedron"/> class.
        /// </summary>
        /// <param name="radius"></param>
        public Tetrahedron(double radius)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Methods
        protected override IEnumerable<ProcedureParameter> InitializeParameters()
        {
            yield return new ProcedureParameter("radius");
            yield return new ProcedureParameter("mode");
        }
        #endregion

        public override void RecalculateMesh()
        {
            throw new NotImplementedException();
        }

        protected override void ApplyParameters()
        {
            throw new NotImplementedException();
        }
    }
}
