using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Modeling
{
    public interface IUnaryGeometryOperation
    {
        Managed3D.Geometry.Mesh3 Input
        {
            get;
            set;
        }
    }
}
