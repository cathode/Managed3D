using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managed3D.Geometry
{
    public static class VertexArrayExtensions
    {
        public static Triangle3 Triangle(this Vertex3[] array, int a, int b, int c)
        {
            return new Triangle3(array[a], array[b], array[c]);
        }
        public static Quad3 Quad(this Vertex3[] array, int a, int b, int c, int d)
        {
            return new Quad3(array[a], array[b], array[c], array[d]);
        }

        public static Polygon3 Polygon(this Vertex3[] array, params int[] indices)
        {
            return new Polygon3(array, indices);
        }
    }
}
