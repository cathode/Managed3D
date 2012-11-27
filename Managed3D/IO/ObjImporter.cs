using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Managed3D.SceneGraph;

namespace Managed3D.IO
{
    /// <summary>
    /// Imports Wavefront Object (.obj) 3d scenes.
    /// </summary>
    public class ObjImporter
    {
        public Node Import(Stream stream)
        {
            var reader = new StreamReader(stream, Encoding.UTF8);

            while (true)
            {
                var line = reader.ReadLine();

                if (line.Length == 0)
                    continue;

                var cmd = line.Substring(0, line.IndexOf(' '));

                switch (cmd)
                {
                    case "v":
                        // Vertex definition
                        break;

                    case "vt":
                        break;

                    case "vn":
                        break;

                    case "f":
                        // Face definition
                        break;

                    case "g":
                        break;

                    case "#":
                    default:
                        continue;
                }
            }
        }
    }
}
