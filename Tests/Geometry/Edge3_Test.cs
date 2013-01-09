/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using NUnit.Framework;
using Managed3D.Geometry;

namespace Tests.Geometry
{
    [TestFixture]
    public class Edge3_Test
    {
        [Test]
        public void Intersections()
        {
            var e = new Edge3(new Vertex3(15, 22, 4), new Vertex3(-8, -33, -12));

            //TODO: use some values that we would know the results for.
            var expected = Vector3.Zero;

            var actual = e.GetIntersectionYZ();

            Assert.AreEqual(expected, actual);
        }
    }
}
