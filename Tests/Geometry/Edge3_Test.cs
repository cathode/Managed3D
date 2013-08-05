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
        public void EnsureEdgeLengthsAreCorrect()
        {
            // Test for line along X axis.
            var e = new Edge3(new Vertex3(0, 0, 0), new Vertex3(1, 0, 0));

            var expected = 1.0;
            var actual = e.GetLength();
            Assert.AreEqual(expected, actual);

            // Test for line along Y axis.
            e = new Edge3(new Vertex3(0, 0, 0), new Vertex3(0, 1, 0));
            actual = e.GetLength();
            Assert.AreEqual(expected, actual);

            // Test for line along Z axis.
            e = new Edge3(new Vertex3(0, 0, 0), new Vertex3(0, 0, 1));
            actual = e.GetLength();
            Assert.AreEqual(expected, actual);

            // Test for line along XY plane.
            e = new Edge3(new Vertex3(0, 0, 0), new Vertex3(4, 3, 0));
            expected = 5.0;
            actual = e.GetLength();
            Assert.AreEqual(expected, actual);

            // Test for line along YZ plane.
            e = new Edge3(new Vertex3(0, 0, 0), new Vertex3(0, 4, 3));
            expected = 5.0;
            actual = e.GetLength();
            Assert.AreEqual(expected, actual);

            // Test for line along XZ plane.
            e = new Edge3(new Vertex3(0, 0, 0), new Vertex3(4, 0, 3));
            expected = 5.0;
            actual = e.GetLength();
            Assert.AreEqual(expected, actual);

            // Test for line along XYZ plane.

            // Not implemented test yet.
        }

        [Test]
        public void Edge3_Intersections()
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
