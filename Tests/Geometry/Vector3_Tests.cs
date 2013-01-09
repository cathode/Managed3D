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
    public class Vector3_Tests
    {
        [Test]
        public void EqualityComparisons()
        {
            var v1 = new Vector3(55, 44, 0).Normalize();
            var v2 = new Vector3(55, 44, 0).Normalize();

            Assert.AreEqual(v1, v2);
        }

        [Test]
        public void OrthoNormal()
        {
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(0, 1, 0);

            var c = Vector3.CrossProduct(a, b);

            var expected = new Vector3(0, 0, 1);

            Assert.AreEqual(expected, c);

            var dot = Vector3.DotProduct(a, b);
        }

        [Test]
        public void OrthoNormal_ZeroResult()
        {
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(1, 0, 0);

            var actual = Vector3.CrossProduct(a, b);

            var expected = Vector3.Zero;

            Assert.AreEqual(expected, actual);
        }
    }
}
