using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Modeling
{
    public class Selection : ISelection
    {
        #region Fields
        private List<ISelectable> items;
        #endregion
        #region Methods
        public IEnumerable<T> GetItems<T>(SelectionTarget kind) where T : ISelectable
        {
            return from i in this.items
                   where i.SelectionKind == kind
                   where i is T
                   select (T)i;
        }
        #endregion
    }
}
