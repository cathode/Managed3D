using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.SceneGraph
{
    /// <summary>
    /// Indicates the order that camera transformations are applied to the view matrix.
    /// </summary>
    public enum TransformOrder
    {
        /// <summary>
        /// Rotation, then translation, then scale.
        /// </summary>
        RotateTranslateScale,
        /// <summary>
        /// Translation, then rotation, then scale.
        /// </summary>
        TranslateRotateScale,
    }
}
