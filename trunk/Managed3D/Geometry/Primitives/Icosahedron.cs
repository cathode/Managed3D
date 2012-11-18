/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Managed3D.Geometry.Primitives
{
    public class Icosahedron : Mesh3
    {
        public Icosahedron()
            : this(1.0)
        {
        }

        public Icosahedron(double radius)
        {
            var verts = new Vertex3[12];

            double phiaa = 26.56505;

            double r = radius;

            double phia = Math.PI * phiaa / 180.0;
            double theb = Math.PI * 36.0 / 180.0;
            double the72 = Math.PI * 72.0 / 180.0;

            verts[0] = new Vertex3(0.0, 0.0, r);
            verts[11] = new Vertex3(0.0, 0.0, -r);
            double the = 0.0;

            for (int i = 1; i < 6; i++)
            {
                double x = r * Math.Cos(the) * Math.Cos(phia);
                double y = r * Math.Sin(the) * Math.Cos(phia);
                double z = r * Math.Sin(phia);
                verts[i] = new Vertex3(x, y, z);
                the += the72;
            }

            the = theb;
            for (int i = 6; i < 11; i++)
            {
                double x = r * Math.Cos(the) * Math.Cos(-phia);
                double y = r * Math.Sin(the) * Math.Cos(-phia);
                double z = r * Math.Sin(-phia);
                verts[i] = new Vertex3(x, y, z);
                the += the72;
            }
            var polys = new Polygon3[20];

            polys[0] = new Triangle3(verts[0], verts[1], verts[2]);
            polys[1] = new Triangle3(verts[0], verts[2], verts[3]);
            polys[2] = new Triangle3(verts[0], verts[3], verts[4]);
            polys[3] = new Triangle3(verts[0], verts[4], verts[5]);
            polys[4] = new Triangle3(verts[0], verts[5], verts[1]);
            polys[5] = new Triangle3(verts[11], verts[6], verts[7]);
            polys[6] = new Triangle3(verts[11], verts[7], verts[8]);
            polys[7] = new Triangle3(verts[11], verts[8], verts[9]);
            polys[8] = new Triangle3(verts[11], verts[9], verts[10]);
            polys[9] = new Triangle3(verts[11], verts[10], verts[6]);
            polys[10] = new Triangle3(verts[1], verts[2], verts[3]);
            polys[11] = new Triangle3(verts[2], verts[3], verts[7]);
            polys[12] = new Triangle3(verts[3], verts[4], verts[8]);
            polys[13] = new Triangle3(verts[4], verts[5], verts[9]);
            polys[14] = new Triangle3(verts[5], verts[1], verts[10]);
            polys[15] = new Triangle3(verts[6], verts[7], verts[2]);
            polys[16] = new Triangle3(verts[7], verts[8], verts[3]);
            polys[17] = new Triangle3(verts[8], verts[9], verts[4]);
            polys[18] = new Triangle3(verts[9], verts[10], verts[5]);
            polys[19] = new Triangle3(verts[10], verts[6], verts[1]);

            this.Polygons = polys;
        }
    }
}
