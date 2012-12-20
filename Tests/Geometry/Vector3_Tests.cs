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
    }
}
