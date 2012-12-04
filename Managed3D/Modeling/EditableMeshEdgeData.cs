using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Modeling
{
    public class EditableMeshEdgeData
    {
        #region Properties
        /// <summary>
        /// aCWEdge
        /// </summary>
        public EditableMeshEdge E0
        {
            get;
            internal set;
        }

        /// <summary>
        /// aCCWEdge
        /// </summary>
        public EditableMeshEdge E1
        {
            get;
            internal set;
        }

        /// <summary>
        /// bCWEdge
        /// </summary>
        public EditableMeshEdge E2
        {
            get;
            internal set;
        }

        /// <summary>
        /// bCCWEdge
        /// </summary>
        public EditableMeshEdge E3
        {
            get;
            internal set;
        }

        /// <summary>
        /// aVertex
        /// </summary>
        public EditableMeshVertex V0
        {
            get;
            internal set;
        }

        /// <summary>
        /// bVertex
        /// </summary>
        public EditableMeshVertex V1
        {
            get;
            internal set;
        }

        /// <summary>
        /// aFace
        /// </summary>
        public EditableMeshFace F0
        {
            get;
            internal set;
        }

        /// <summary>
        /// bFace
        /// </summary>
        public EditableMeshFace F1
        {
            get;
            internal set;
        }

        public int Index
        {
            get;
            internal set;
        }

        public EditableMeshEdge Owner
        {
            get;
            internal set;
        }
        #endregion
    }
}
