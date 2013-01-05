using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Managed3D.Geometry;

namespace Tests.SceneGraph
{
    [TestClass]
    public class Extents3_Tests
    {
        [TestMethod]
        public void MidpointCalculation()
        {
            var exts = new Extents3(new Vector3(10, 10, 10), new Vector3(-10, -10, -10));

            var expected = Vector3.Zero;

            var actual = exts.FindMidpoint();

            Assert.AreEqual(expected, actual);
        }
    }
}
