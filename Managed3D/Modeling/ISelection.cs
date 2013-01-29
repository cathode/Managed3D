using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Modeling
{
    public interface ISelection
    {
        IEnumerable<T> GetItems<T>(SelectionTarget kind) where T : ISelectable;
    }
}
