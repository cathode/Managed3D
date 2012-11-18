using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Managed3D.Platform;

namespace Managed3D.Rendering
{
    public interface IManagedRendererTarget
    {
        void UpdateDisplayProfile(DisplayProfile profile);
        void ConsumeFrameBuffer(ManagedBuffer buffer);
    }
}
