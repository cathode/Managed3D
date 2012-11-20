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
        private Dictionary<ushort, RDVertex> vertices;
        private Dictionary<ushort, RDEdge> edges;
        private Dictionary<ushort, RDFace> faces;

        private Dictionary<ushort, RDSparseAttributes> attributes;
    }

    public class RDSparseAttributes
    {
        public bool IsHidden;
        public Vector4f Color;
    }

    public class RDVertex
    {
        public double X;
        public double Y;
        public double Z;

        public uint[] Edges;
    }

    public class RDFace
    {
        public uint EdgeA;
        public uint EdgeB;
        public uint EdgeC;
    }

    public class RDEdge
    {
        public uint VertexA;
        public uint VertexB;

        public uint FaceA;
        public uint FaceB;

        public uint EdgeA;
        public uint EdgeB;
        public uint EdgeC;
        public uint EdgeD;
    }
}
