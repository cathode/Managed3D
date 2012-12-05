using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Managed3D.Geometry;

namespace Managed3D.Modeling
{
    public class EditableMeshVertex : IVector3, IRenderableVertex
    {
        #region Fields
        private double x;
        private double y;
        private double z;
        private int e0, e1, e2, e3;
        #endregion
        #region Properties
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

        public double Y
        {
            get
            {
                return this.y;
            }
        }

        public double Z
        {
            get
            {
                return this.z;
            }
        }
        /// <summary>
        /// Gets or sets the index of the front clockwise edge.
        /// </summary>
        public int E0a
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the index of the back clockwise edge.
        /// </summary>
        public int E1a
        {
            get;
            set;
        }

        public int E0b
        {
            get;
            set;
        }

        public int E1b
        {
            get;
            set;
        }
        #endregion
        #region Methods
        public IEnumerable<EditableMeshFace> SurroundingFaces()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EditableMeshEdge> SurroundingEdges()
        {
            throw new NotImplementedException();
        }

        Vector3 IVector3.ToVector3()
        {
            return new Vector3(this.x, this.y, this.z);
        }

        Vector2 IVector2.ToVector2()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
