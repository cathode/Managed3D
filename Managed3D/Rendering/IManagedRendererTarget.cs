using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Managed3D.Platform;
using System.Diagnostics.Contracts;

namespace Managed3D.Rendering
{
    [ContractClass(typeof(__ContractsForIManagedRendererTarget))]
    public interface IManagedRendererTarget
    {
        #region Methods
        void UpdateDisplayProfile(DisplayProfile profile);
        void ConsumeFrameBuffer(ManagedBuffer buffer);
        #endregion
    }
    
    [ContractClassFor(typeof(IManagedRendererTarget))]
    internal abstract class __ContractsForIManagedRendererTarget : IManagedRendererTarget
    {
        void IManagedRendererTarget.UpdateDisplayProfile(DisplayProfile profile)
        {
            Contract.Requires(profile != null);
        }

        void IManagedRendererTarget.ConsumeFrameBuffer(ManagedBuffer buffer)
        {
            Contract.Requires(buffer != null);
        }
    }
}
