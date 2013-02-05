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

namespace Managed3D.Modeling
{
    public interface IHalfEdge : ISelectable
    {
        #region Properties
        int Id { get; }
        IHalfEdge Next { get; }
        IHalfEdge Previous { get; }
        IHalfEdge Opposite { get; }
        IVertex Start { get; }
        IVertex End { get; }
        IFace Face { get; }
        object Tag { get; set; }
        #endregion
        #region Methods

        #endregion
    }
}
