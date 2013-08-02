using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Modeling.Parametrics
{
    /// <summary>
    /// Represents a relationship between two entities.
    /// </summary>
    public abstract class RelationshipBase
    {
        /// <summary>
        /// Gets or sets the first entity of the relationship.
        /// </summary>
        public Entity A { get; set; }

        /// <summary>
        /// Gets or sets the second entity of the relationship.
        /// </summary>
        public Entity B { get; set; }

        /// <summary>
        /// When implemented in a derived class, gets a value that indicates whether the relationship is solvable.
        /// </summary>
        /// <returns></returns>
        public abstract bool IsSolvable();
    }
}
