using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Modeling
{
    [Flags]
    public enum SelectionTarget
    {
        /// <summary>
        /// Indicates no type of selection.
        /// </summary>
        None        = 0x0000,

        /// <summary>
        /// Indicates the selection includes vertices.
        /// </summary>
        Vertex      = 0x0001,

        /// <summary>
        /// Indicates the selection includes edges.
        /// </summary>
        Edge        = 0x0002,

        /// <summary>
        /// Indicates the selection includes triangles.
        /// </summary>
        Triangle    = 0x0004,

        /// <summary>
        /// Indicates the selection includes faces.
        /// </summary>
        Face        = 0x0008,

        /// <summary>
        /// Indicates the selection includes mesh elements.
        /// </summary>
        Element     = 0x0010,

        /// <summary>
        /// Indicates the selection includes complete meshes.
        /// </summary>
        Mesh        = 0x0020,

        /// <summary>
        /// Indicates the selection includes a complete model.
        /// </summary>
        Model       = 0x0040,

        /// <summary>
        /// Indicates the selection includes a scene graph node.
        /// </summary>
        Node        = 0x0080,

        /// <summary>
        /// Indicates the selection includes a scene graph node and all of it's children.
        /// </summary>
        NodeBranch  = 0x0100,
    }
}
