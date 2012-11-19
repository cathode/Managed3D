using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX.Windows;
using SlimDX.Multimedia;

namespace Managed3D.Rendering.Direct3D
{
    public class BitmapRenderer : Renderer
    {
        protected override void OnInitializing(RendererInitializationEventArgs e)
        {
            base.OnInitializing(e);
        }


        public System.Drawing.Bitmap RenderToBitmap(Managed3D.SceneGraph.Scene scene)
        {
            
            throw new NotImplementedException();
        }
    }
}
