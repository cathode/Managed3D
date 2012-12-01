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

namespace Managed3D.Geometry
{
    /// <summary>
    /// Describes the absolute minimum requirements for a 3d mesh to be rendered.
    /// </summary>
    public interface IRenderable
    {
        #region Properties
        /// <summary>
        /// Gets a collection of vertices to render.
        /// </summary>
        IEnumerable<IRenderableVertex> Vertices
        {
            get;
        }

        IEnumerable<IRenderableFace> Faces
        {
            get;
        }

        IEnumerable<IRenderableEdge> Edges
        {
            get;
        }
        #endregion
    }

    public interface IRenderableVertex
    {
        #region Properties
        double X
        {
            get;
        }
        double Y
        {
            get;
        }
        double Z
        {
            get;
        }
        #endregion
    }

    public interface IRenderableFace
    {
        int VertexA
        {
            get;
        }
        int VertexB
        {
            get;
        }
        int VertexC
        {
            get;
        }
    }
    public interface IRenderableEdge
    {
        int P
        {
            get;
        }
        int Q
        {
            get;
        }
    }
}
