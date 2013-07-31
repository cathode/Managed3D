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
        public decimal Value { get; set; }


        public override bool IsSolvable()
        {
            throw new NotImplementedException();
        }
    }
}
