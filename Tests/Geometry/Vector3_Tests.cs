using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Managed3D.Geometry;

namespace Tests.Geometry
{
    [TestClass]
    public class Vector3_Tests
    {
        [TestMethod]
        public void EqualityComparisons()
        {
            var v1 = new Vector3(55, 44, 0).Normalize();
            var v2 = new Vector3(55, 44, 0).Normalize();

            Assert.AreEqual(v1, v2);
        }

        [TestMethod]
        public void OrthoNormal()
        {
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(0, 1, 0);

            var c = Vector3.CrossProduct(a, b);

            var expected = new Vector3(0, 0, 1);

            Assert.AreEqual(expected, c);

            var dot = Vector3.DotProduct(a, b);
        }

        [TestMethod]
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
