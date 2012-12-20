using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Managed3D.Geometry;

namespace Tests.Geometry
{
    /// <summary>
    /// Tests for the <see cref="Managed3D.Geometry.Quaternion"/> class.
    /// </summary>
    [TestClass]
    public class Quaternion_Tests
    {

        [TestMethod]
        public void EnsureAxisAngleRoundtrip()
        {
            var axis = new Vector3(0.67, 0.33, 0.0).Normalize();
            var angle = Angle.FromDegrees(22.5);

            axis = axis.Round(14);

            var q = new Quaternion(axis, angle);

            Vector3 axis2;
            Angle angle2;
            q.GetAxisAngle(out axis2, out angle2);
            axis2 = axis2.Normalize().Round(14);



            Assert.AreEqual(axis, axis2);
            Assert.AreEqual(angle, angle2);
        }
    }
}
