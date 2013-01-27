using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Modeling
{
    /// <summary>
    /// Provides an <see cref="ISelectionModifier"/> implementation which selects items that are contained or which intersect a three-dimensional bounding volume.
    /// </summary>
    public class SelectVolumeModifier : ISelectionModifier
    {

        public SelectionAction Action
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public SelectionTarget Targets
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
