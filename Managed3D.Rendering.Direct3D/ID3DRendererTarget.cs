using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using SlimDX;

namespace Managed3D.Rendering.Direct3D
{
    public interface ID3DRendererTarget
    {
        SlimDX.Direct2D.RenderTarget Target
        {
            get;
            set;
        }
    }
}
