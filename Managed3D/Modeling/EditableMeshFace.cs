using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed3D.Modeling
{
    /// <summary>
    /// Represents an <see cref="EditableMesh"/> face (triangle).
    /// </summary>
    public class EditableMeshFace
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EditableMeshFace"/> class.
        /// </summary>
        public EditableMeshFace()
        {
        }
        #endregion
        #region Properties
        public EditableMeshEdge EdgeRing
        {
            get;
            internal set;
        }

        public EditableMeshFace Next
        {
            get;
            internal set;
        }

        public EditableMeshFace Previous
        {
            get;
            internal set;
        }

        public int Index
        {
            get;
            internal set;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Given an edge (which needs to be an edge of the current face), returns the face on the other side of the edge,
        /// relative to the current face.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public EditableMeshFace FaceAcrossEdge(EditableMeshEdge edge)
        {
            if (this == edge.AF)
                return edge.BF;
            else if (this == edge.BF)
                return edge.AF;
            else
                throw new NotImplementedException();
        }

        /// <summary>
        /// Allows enumeration over all the edges of the current face.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EditableMeshEdge> GetEdges()
        {
            var start = this.EdgeRing;

            while (true)
            {

            }
        }
        #endregion
    }
}
