/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using NUnit.Framework;
using Managed3D.Geometry;

namespace Tests.SceneGraph
{
    [TestFixture]
    public class Extents3_Tests
    {
        [Test]
        public void Extents3_MidpointCalculation()
        {
            var exts = new Extents3(new Vector3(10, 10, 10), new Vector3(-10, -10, -10));

            var expected = Vector3.Zero;

            var actual = exts.FindMidpoint();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Extents3_ConstructorBehavior()
        {
            var e1 = new Extents3();
            var e2 = new Extents3(0, 0, 0);
            var e3 = new Extents3(0, 0, 0, 0, 0, 0);
            var e4 = new Extents3(new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            Assert.AreEqual(e1, e2);
            Assert.AreEqual(e2, e3);
            Assert.AreEqual(e3, e4);

            // Test nonzero values
            e2 = new Extents3(10, 8, 6);
            e3 = new Extents3(5, 4, 3, -5, -4, -3);
            e4 = new Extents3(new Vector3(5, 4, 3), new Vector3(-5, -4, -3));

            Assert.AreEqual(e2, e3);
            Assert.AreEqual(e3, e4);
        }

        [Test]
        public void Extents3_EqualityComparison()
        {
            var e1 = new Extents3(0, 1, 2);
            var e2 = new Extents3(0, 1, 2);
            var e3 = new Extents3(3, 2, 1);
            var e4 = new Extents3(3, 2, 1);

            Assert.AreEqual(e1, e2);
            Assert.AreEqual(e3, e4);
            Assert.AreNotEqual(e1, e3);
            Assert.AreNotEqual(e2, e4);

            Assert.IsTrue(e1 == e2);
            Assert.IsTrue(e3 == e4);
        }
    }
}
