using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Managed3D.Geometry
{
    public static class VertexArrayExtensions
    {
        public static Triangle3 Triangle(this Vertex3[] array, int a, int b, int c)
        {
            Contract.Requires(array.Length >= 3);
            Contract.Requires(a < array.Length);
            Contract.Requires(b < array.Length);
            Contract.Requires(c < array.Length);
            Contract.Requires(array[a] != null);
            Contract.Requires(array[b] != null);
            Contract.Requires(array[c] != null);

            return new Triangle3(array[a], array[b], array[c]);
        }
        public static Quad3 Quad(this Vertex3[] array, int a, int b, int c, int d)
        {
            Contract.Requires(array.Length >= 4);
            Contract.Requires(a < array.Length);
            Contract.Requires(b < array.Length);
            Contract.Requires(c < array.Length);
            Contract.Requires(d < array.Length);

            return new Quad3(array[a], array[b], array[c], array[d]);
        }

        public static Polygon3 Polygon(this Vertex3[] array, params int[] indices)
        {
            Contract.Requires(array != null);
            Contract.Requires(indices != null);
            Contract.Requires(indices.Length > 2);
            Contract.Requires(indices.All(i => i < array.Length));
            Contract.Requires(indices.Distinct().Count() == indices.Length);

            return new Polygon3(array, indices);
        }
    }
}
