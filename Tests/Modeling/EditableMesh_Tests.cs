/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using NUnit.Framework;

namespace Managed3D.Modeling
{
    [TestFixture]
    public class EditableMesh_Tests
    {

        [Test]
        public void InsertVertexOnEdge()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void InsertVertexOnFace()
        {
            var mesh = new EditableMesh();
            var face = mesh.CreateFace(-1, -1, -1, 1, 1, 1, 0, 0, 0);
            var vert = mesh.InsertVertexInFace(face.Id);

            Assert.AreEqual(4, mesh.VertexCount);
            Assert.AreEqual(6, mesh.EdgeCount);
            
            Assert.Inconclusive();
        }

        [Test]
        public void FaceEdgeTraversal()
        {
            Assert.Inconclusive();
        }
    }
}
