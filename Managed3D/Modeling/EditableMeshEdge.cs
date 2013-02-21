/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Managed3D.Modeling
{
    /// <summary>
    /// Represents a render-dynamic edge.
    /// </summary>
    public class EditableMeshEdge : IHalfEdge
    {
        #region Fields
        private int id;
        private EditableMeshEdge opposite;
        private EditableMeshFace face;
        private EditableMeshEdge next;
        private EditableMeshEdge prev;
        private EditableMeshVertex end;
        private EditableMeshVertex start;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EMEdge"/> class.
        /// </summary>
        internal EditableMeshEdge(int id, EditableMeshVertex v0, EditableMeshVertex v1)
        {
            // TODO: Complete member initialization
            this.id = id;
            this.start = v0;
            this.end = v1;
        }
        #endregion
        #region Properties
        public EditableMeshEdge Opposite
        {
            get
            {
                return this.opposite;
            }
        }

        public EditableMeshEdge Next
        {
            get
            {
                return this.next;
            }
            internal set
            {
                this.next = value;
            }
        }

        public EditableMeshEdge Previous
        {
            get
            {
                return this.prev;
            }
            internal set
            {
                this.prev = value;
            }
        }

        public EditableMeshVertex Start
        {
            get
            {
                return this.start;
            }
        }
        public EditableMeshVertex End
        {
            get
            {
                return this.end;
            }
        }

        public EditableMeshFace Face
        {
            get
            {
                return this.face;
            }
            internal set
            {
                this.face = value;
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }
        }

        public object Tag
        {
            get;
            set;
        }
        #endregion
        #region MEthods
        [ContractInvariantMethod]
        private void Invariants()
        {
            //Contract.Invariant(this != this.Next);
            //Contract.Invariant(this == this.Next.Opposite.Next);
            //Contract.Invariant(this == this.Opposite.Opposite);
            //Contract.Invariant(this.Face == this.Next.Face);
        }
        #endregion


        IHalfEdge IHalfEdge.Next
        {
            get
            {
                return this.Next;
            }
        }

        IHalfEdge IHalfEdge.Previous
        {
            get
            {
                return this.Opposite.Next;
            }
        }

        IHalfEdge IHalfEdge.Opposite
        {
            get
            {
                return this.Opposite;
            }
        }

        IVertex IHalfEdge.Start
        {
            get
            {
                return this.Start;
            }
        }

        IVertex IHalfEdge.End
        {
            get
            {
                return this.End;
            }
        }

        IFace IHalfEdge.Face
        {
            get
            {
                return this.Face;
            }
        }

        public SelectionTarget SelectAs
        {
            get
            {
                return SelectionTarget.Edge;
            }
        }
    }
}
