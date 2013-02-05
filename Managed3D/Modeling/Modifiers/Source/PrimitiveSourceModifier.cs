using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Modeling.Modifiers.Source
{
    /// <summary>
    /// Provides a modifier implementation that produces geometry from a primitive object such as a box, sphere, or cone.
    /// </summary>
    public class PrimitiveSourceModifier : Modifier, IGeometrySource
    {
        #region Properties
        public PrimitiveKind Kind { get; set; }
        #endregion

        public EditableMesh Generate()
        {
            if (this.Kind == PrimitiveKind.Box)
            {
                // default to creating a unit cube.
                var x = 1;
                var y = 1;
                var z = 1;

                var mesh = new EditableMesh();

                var v0 = mesh.CreateVertex(x, y, z);
                var v1 = mesh.CreateVertex(x, -y, z);
                var v2 = mesh.CreateVertex(x, -y, -z);
                var v3 = mesh.CreateVertex(x, y, -z);

                var v4 = mesh.CreateVertex(-x, -y, -z);
                var v5 = mesh.CreateVertex(-x, -y, z);
                var v6 = mesh.CreateVertex(-x, y, z);
                var v7 = mesh.CreateVertex(-x, y, -z);

                mesh.CreateFace(v0, v1, v2, v3);
                mesh.CreateFace(v4, v5, v6, v7);
                mesh.CreateFace(v0, v3, v7, v6);
                mesh.CreateFace(v1, v0, v6, v5);
                mesh.CreateFace(v2, v1, v5, v4);
                mesh.CreateFace(v3, v2, v4, v7);

                return mesh;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override void Apply(Model model)
        {
            throw new NotImplementedException();
        }
    }
}
