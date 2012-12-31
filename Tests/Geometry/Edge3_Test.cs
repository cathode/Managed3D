using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Managed3D.Geometry;

namespace Tests.Geometry
{
    [TestClass]
    public class Edge3_Test
    {
        [TestMethod]
        public void Intersections()
        {

            var e = new Edge3(new Vertex3(15, 22, 4), new Vertex3(-8, -33, -12));

            var expected = Vector3.Zero;

            var actual = e.GetIntersectionYZ();

            Assert.AreEqual(expected, actual);
        }
    }
}
