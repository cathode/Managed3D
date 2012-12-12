using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Managed3D.Geometry;

namespace Managed3D.SceneGraph
{
    /// <summary>
    /// Represents a scene node constraint that can dynamically limit
    /// the position, rotation, and/or scale of a node.
    /// </summary>
    public class Constraint
    {
        #region Fields

        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Constraint"/> class.
        /// </summary>
        /// <param name="subject">The node that the constraint applies to.</param>
        /// <param name="target">The property of the node that is affected by the constraint.</param>
        public Constraint(ConstraintTarget target)
        {

        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="ConstraintTarget"/> that indicates the property or properties on the subject node which are constrained.
        /// </summary>
        public ConstraintTarget Target
        {
            get;
            set;
        }

        #endregion
        #region Methods
        public Vector3 ApplyPositionConstraint(Vector3 newPosition)
        {
            throw new NotImplementedException();
        }

        public Vector3 ApplyOrientationConstraint(Vector3 newOrientation)
        {
            throw new NotImplementedException();
        }

        public Vector3 ApplyScaleConstraint(Vector3 newScale)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
