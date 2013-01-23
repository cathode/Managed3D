using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Managed3D.Geometry;

namespace Managed3D.Geometry
{
    [TestFixture]
    public class Matrix4_Tests
    {

        [Test]
        public void Matrix4_MakePerspectiveProjectionMatrix()
        {
            var fovyDegrees = 60;
            var fovy = Angle.RadiansFromDegrees(fovyDegrees);
            var width = 800.0;
            var height = 600.0;
            var aspect = width / height;

            var near = 1.0;
            var far = 1000.0;

            var f = Math.Cos(fovy) / Math.Sin(fovy);

            var m00 = f / aspect;

            var m11 = f;
            var m22 = (near + far) / (near - far);
            var m23 = (2 * (far * near)) / (near - far);
            var m32 = -1;

            var expected = new Matrix4(m00, 0, 0, 0,
                                       0, m11, 0, 0,
                                       0, 0, m22, m23,
                                       0, 0, m32, 0);

            var actual = Matrix4.CreatePerspectiveProjectionMatrix(Angle.FromDegrees(fovyDegrees), aspect, near, far);

            Assert.AreEqual(expected, actual);
        }
    }
}
