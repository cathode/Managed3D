using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managed3D.Modeling
{
    /// <summary>
    /// Represents supported constructive solid geometry operations.
    /// </summary>
    public enum CsgOperation
    {
        /// <summary>
        /// Represents the merger of two objects into one.
        /// </summary>
        Union,

        /// <summary>
        /// Subtraction of the operator from the operand.
        /// </summary>
        Difference,

        /// <summary>
        /// 
        /// </summary>
        Intersection,
    }
}
