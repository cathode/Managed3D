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
    /// <summary>
    /// Tests for the <see cref="Managed3D.Geometry.Quaternion"/> class.
    /// </summary>
    [TestFixture]
    public class Quaternion_Tests
    {

        [Test]
        public void EnsureAxisAngleRoundtrip()
        {
            var axis = new Vector3(0.67, 0.33, 0.0).Normalize();
            var angle = Angle.FromDegrees(22.5);

            //axis = axis.Round(14);

            var q = new Quaternion(axis, angle);

            Vector3 axis2;
            Angle angle2;
            q.GetAxisAngle(out axis2, out angle2);
            axis2 = axis2.Normalize();//.Round(10);



            Assert.AreEqual(axis, axis2);
            Assert.AreEqual(angle, angle2);
        }
    }
}
