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

namespace Managed3D.Modeling
{
    public interface IFace : ISelectable
    {
        #region Properties
        int Id { get; }
        IHalfEdge StartingEdge { get; }
        object Tag { get; set; }
        FaceFlags Flags { get; }
        #endregion
        #region Methods
        IEnumerable<IHalfEdge> GetEdges();
        IEnumerable<IFace> GetNeighboringFaces();
        IEnumerable<IVertex> GetVertices();
        
        #endregion
    }
}
