using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Modeling
{
    /// <summary>
    /// Represents an element that can be selected.
    /// </summary>
    public interface ISelectable
    {
        /// <summary>
        /// Gets the <see cref="SelectionTarget"/> of the selectable element.
        /// </summary>
        SelectionTarget SelectionKind
        {
            get;
        }
    }
}
