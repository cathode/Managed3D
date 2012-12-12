using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Managed3D.Geometry;

namespace Managed3D.Modeling
{
    /// <summary>
    /// Represents a vertex of an editable mesh.
    /// </summary>
    public class EditableMeshVertex : IRenderableVertex
    {
        #region Fields
        private double x;
        private double y;
        private double z;
        private int e0, e1, e2, e3;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the x-coordinate of the vertex.
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the vertex.
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// Gets or sets the z-coordinate of the vertex.
        /// </summary>
        public double Z
        {
            get
            {
                return this.z;
            }
            set
            {
                this.z = value;
            }
        }

        /// <summary>
        /// Gets or sets the index of the front clockwise edge.
        /// </summary>
        public int EdgeFCW
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the index of the front counterclockwise edge.
        /// </summary>
        public int EdgeFCCW
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the index of the back clockwise edge.
        /// </summary>
        public int EdgeBCW
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the index of the back counterclockwise edge.
        /// </summary>
        public int EdgeBCCW
        {
            get;
            set;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Allows enumeration over faces surrounding the current vertex.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EditableMeshFace> SurroundingFaces()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows enumeration over edges surrounding the current vertex.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EditableMeshEdge> SurroundingEdges()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
