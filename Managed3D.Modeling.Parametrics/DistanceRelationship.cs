using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Modeling.Parametrics
{
    /// <summary>
    /// Represents a relationship between two entities that preserves distance between them.
    /// </summary>
    public class DistanceRelationship : RelationshipBase
    {
        /// <summary>
        /// Gets or sets a value indicating the distance between the two referenced entities.
        /// </summary>
        /// <remarks>
        /// If the relationship is broken, this property returns 0.
        /// </remarks>
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the distance relationship is being driven by the entities it references.
        /// </summary>
        public bool IsDriving { get; set; }

        public override bool IsSolvable()
        {
            throw new NotImplementedException();
        }
    }
}
