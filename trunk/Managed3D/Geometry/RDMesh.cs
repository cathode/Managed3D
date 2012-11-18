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
    /// Provides a 3D Render-Dynamic Mesh implementation.
    /// </summary>
    public class RDMesh
    {
        private RDVertex[] vertices;
        private RDFace[] triangles;
        private RDEdge[] edges;
        #region Types
        public struct RDFace
        {
            public int A;
            public int B;
            public int C;
        }

        public struct RDEdge
        {
            public int V1;
            public int V2;
            public int T1;
            public int T2;
        }

        public sealed class RDVertex
        {
            double X;
            double Y;
            double Z;

            int[] Faces;
        }
        #endregion
    }
}
