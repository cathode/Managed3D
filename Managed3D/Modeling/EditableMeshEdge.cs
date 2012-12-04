using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Modeling
{
    /// <summary>
    /// Represents a render-dynamic edge.
    /// </summary>
    public class EditableMeshEdge
    {
        #region Fields
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EMEdge"/> class.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public EditableMeshEdge()
        {

        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the edge data of the <see cref="EMEdge"/>.
        /// </summary>
        public EditableMeshEdgeData EdgeData
        {
            get;
            internal set;
        }

        public EditableMeshEdge Next
        {
            get;
            internal set;
        }

        public EditableMeshEdge Previous
        {
            get;
            internal set;
        }

        public EditableMeshFace AF
        {
            get
            {
                return this.EdgeData.F0;
            }
            internal set
            {
                this.EdgeData.F0 = value;
            }
        }

        public EditableMeshFace BF
        {
            get
            {
                return this.EdgeData.F1;
            }
            internal set
            {
                this.EdgeData.F1 = value;
            }
        }

        public EditableMeshVertex AV
        {
            get
            {
                return this.EdgeData.V0;
            }
            internal set
            {
                this.EdgeData.V0 = value;
            }
        }

        public EditableMeshVertex BV
        {
            get
            {
                return this.EdgeData.V1;
            }
            internal set
            {
                this.EdgeData.V1 = value;
            }
        }
        public EditableMeshEdge ACW
        {
            get
            {
                return this.EdgeData.E0;
            }
            internal set
            {
                this.EdgeData.E0 = value;
            }
        }

        public EditableMeshEdge ACCW
        {
            get
            {
                return this.EdgeData.E1;
            }
            internal set
            {
                this.EdgeData.E1 = value;
            }
        }

        public EditableMeshEdge BCW
        {
            get
            {
                return this.EdgeData.E2;
            }
            internal set
            {
                this.EdgeData.E2 = value;
            }
        }

        public EditableMeshEdge BCCW
        {
            get
            {
                return this.EdgeData.E3;
            }
            internal set
            {
                this.EdgeData.E3 = value;
            }
        }
        #endregion

    }
}
