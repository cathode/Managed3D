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
        public void Edge3_Midpoint()
        {
            var e = new Edge3(new Vertex3(1, 0, 0), new Vertex3(2, 0, 0));

            var expected = new Vector3(1.5, 0, 0);

            var actual = e.GetMidpoint();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Length()
        {
            var e = new Edge3(new Vertex3(0, 0, 0), new Vertex3(1, 0, 0));

            var expected = 1.0d;

            var actual = e.GetLength();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Intersections()
        {
            var e1 = new Edge3(new Vertex3(0, 0, -1), new Vertex3(0, 0, 1));
            var e2 = new Edge3(new Vertex3(0, -5, 0), new Vertex3(0, 5, 0));

            var e3 = new Edge3(new Vertex3(0, 0, -5), new Vertex3(0, 0, 5));


            //TODO: use some values that we would know the results for.
            var expected = Vector3.Zero;

            var actual = e1.GetIntersectionXY();

            Assert.AreEqual(expected, actual);
        }
    }
}
