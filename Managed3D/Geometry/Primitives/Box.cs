using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Geometry.Primitives
{
    public class Box : Mesh3
    {
        public Box(double width, double length, double height)
        {


            var x = width * 0.5;
            var y = length * 0.5;
            var z = height * 0.5;

            var verts = new Vertex3[8];
            verts[0] = new Vertex3(x, y, z);
            verts[1] = new Vertex3(x, -y, z);
            verts[2] = new Vertex3(-x, -y, z);
            verts[3] = new Vertex3(-x, y, z);

            verts[4] = new Vertex3(x, y, -z);
            verts[5] = new Vertex3(x, -y, -z);
            verts[6] = new Vertex3(-x, -y, -z);
            verts[7] = new Vertex3(-x, y, -z);

            this.Polygons = new Quad3[] { 
                // Top and bottom
                new Quad3(verts, 0, 1, 2, 3),
                new Quad3(verts, 7, 6, 5, 4),

                new Quad3(verts, 0, 3, 7, 4),
                new Quad3(verts, 1, 0, 4, 5),
                new Quad3(verts, 2, 1, 5, 6),
                new Quad3(verts, 3, 2, 6, 7),
                
            };
        }
    }
}
