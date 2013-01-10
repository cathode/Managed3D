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
        public void MidpointCalculation()
        {
            var exts = new Extents3(new Vector3(10, 10, 10), new Vector3(-10, -10, -10));

            var expected = Vector3.Zero;

            var actual = exts.FindMidpoint();

            Assert.AreEqual(expected, actual);
        }

    }
}
