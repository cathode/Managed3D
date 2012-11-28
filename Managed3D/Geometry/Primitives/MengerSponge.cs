using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Geometry.Primitives
{
    /// <summary>
    /// Provides a solid primitive that implements the Menger sponge algorithm.
    /// </summary>
    public class MengerSponge : Managed3D.SceneGraph.GeometryNode
    {
        public MengerSponge(double radius, int iterations)
        {
            if (iterations > 0)
            {
                var r = radius * 0.5;

                var positions = new Vector3[20];
                positions[0] = new Vector3(r, r, r);
                positions[1] = new Vector3(r, 0, r);
                positions[2] = new Vector3(r, -r, r);
                positions[3] = new Vector3(0, -r, r);
                positions[4] = new Vector3(-r, -r, r);
                positions[5] = new Vector3(-r, 0, r);
                positions[6] = new Vector3(-r, r, r);
                positions[7] = new Vector3(0, r, r);

                positions[8] = new Vector3(r, r, 0);
                positions[9] = new Vector3(r, -r, 0);
                positions[10] = new Vector3(-r, -r, 0);
                positions[11] = new Vector3(-r, r, 0);

                positions[12] = new Vector3(r, r, -r);
                positions[13] = new Vector3(r, 0, -r);
                positions[14] = new Vector3(r, -r, -r);
                positions[15] = new Vector3(0, -r, -r);
                positions[16] = new Vector3(-r, -r, -r);
                positions[17] = new Vector3(-r, 0, -r);
                positions[18] = new Vector3(-r, r, -r);
                positions[19] = new Vector3(0, r, -r);

                foreach (var pos in positions)
                {
                    this.Add(new MengerSponge(r, iterations - 1)
                    {
                        Position = pos
                    });
                }
            }
            else
            {
                this.Geometry = new Cube(radius);
            }
        }
    }
}
