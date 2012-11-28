/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Managed3D.Geometry;

namespace Managed3D.SceneGraph
{
    /// <summary>
    /// Represents a node within the scene graph.
    /// </summary>
    public abstract class Node
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        protected Node()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        /// <param name="children"></param>
        protected Node(params Node[] children)
        {
            this.children.AddRange(children);
        }
        #endregion
        #region Fields
        private readonly List<Node> children = new List<Node>();
        private IVector3 position = Node.DefaultPosition;
        private IVector3 scale = Node.DefaultScale;
        private IVector3 orientation = Node.DefaultOrientation;
        private NodeRenderFlags renderFlags;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the number of child in the current <see cref="Node"/>.
        /// </summary>
        public int Count
        {
            get
            {
                return this.children.Count;
            }
        }
        public static IVector3 DefaultOrientation
        {
            get
            {
                return new Vector3(0, 0, 0);
            }
        }
        public static IVector3 DefaultPosition
        {
            get
            {
                return Vector3.Zero;
            }
        }
        public static IVector3 DefaultScale
        {
            get
            {
                return new Vector3(1, 1, 1);
            }
        }

        /// <summary>
        /// Gets a value that indicates if the current <see cref="Node"/> is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets or sets an <see cref="IRotation3"/> which determines the orientation of the current <see cref="Node"/> relative to it's parent.
        /// </summary>
        public IVector3 Orientation
        {
            get
            {
                return this.orientation;
            }
            set
            {
                this.orientation = value ?? Node.DefaultOrientation;
            }
        }

        /// <summary>
        /// Gets or sets an <see cref="IVector3"/> which determines the position of the current <see cref="Node"/> relative to it's parent.
        /// </summary>
        public IVector3 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value ?? Node.DefaultPosition;
            }
        }

        /// <summary>
        /// Gets or sets flags that determine how (or if) the current <see cref="Node"/> is rendered.
        /// </summary>
        public virtual NodeRenderFlags RenderFlags
        {
            get
            {
                return this.renderFlags;
            }
            set
            {
                this.renderFlags = value;
            }
        }

        /// <summary>
        /// Gets or sets an <see cref="IVector3"/> which determines the scale of the current <see cref="Node"/> relative to it's parent.
        /// </summary>
        public IVector3 Scale
        {
            get
            {
                return this.scale;
            }
            set
            {
                this.scale = value ?? Node.DefaultScale;
            }
        }

        public virtual bool HasChildren
        {
            get
            {
                return false;
            }
        }

        public ReferenceSpace OrientationSpace
        {
            get;
            set;
        }

        public ReferenceSpace PositionSpace
        {
            get;
            set;
        }
        public ReferenceSpace ScalingSpace
        {
            get;
            set;
        }

        public virtual bool IsStatic
        {
            get
            {
                foreach (var child in this.children)
                    if (!child.IsStatic)
                        return false;

                return true;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Adds a child <see cref="Node"/> to the current <see cref="Node"/>.
        /// </summary>
        /// <param name="item">The <see cref="Node"/> instance to be added.</param>
        public void Add(Node item)
        {
            if (item.ContainsDeep(this))
                throw new InvalidOperationException("A recursive node addition was detected");

            this.children.Add(item);
        }
        /// <summary>
        /// Removes all child nodes from the current <see cref="Node"/>.
        /// </summary>
        public void Clear()
        {
            this.children.Clear();
        }
        /// <summary>
        /// Determines if the specified <see cref="Node"/> is a child of the current <see cref="Node"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(Node item)
        {
            return this.children.Contains(item);
        }
        /// <summary>
        /// Determines if the current <see cref="Node"/> contains the specified item,
        /// and searches all children for the specified item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool ContainsDeep(Node item)
        {
            foreach (var child in this.children)
                if (child == item || child.ContainsDeep(item))
                    return true;
            return false;
        }
        /// <summary>
        /// Copies the child nodes of the current <see cref="Node"/> into <paramref name="array"/>, starting at <paramref name="arrayIndex"/>.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(Node[] array, int arrayIndex)
        {
            this.children.CopyTo(array, arrayIndex);
        }
        /// <summary>
        /// Gets an enumerator that allows iteration of the child nodes in the current <see cref="Node"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Node> GetEnumerator()
        {
            return this.children.GetEnumerator();
        }
        /// <summary>
        /// Removes the specifed <see cref="Node"/> from the current <see cref="Node"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(Node item)
        {
            return this.children.Remove(item);
        }

        /// <summary>
        /// Forces the node to update it's bounding volume information for potential visibility determination.
        /// </summary>
        public virtual void UpdateBoundingVolume()
        {

        }

        /// <summary>
        /// Calculates the extents of the current node.
        /// </summary>
        /// <returns></returns>
        public virtual Vector3 GetExtents()
        {
            return new Vector3(0, 0, 0);
        }

        /// <summary>
        /// Recursively calculates the geometry extents of the current node and
        /// all child nodes of the current node.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetGraphExtents()
        {
            var ext = this.GetExtents();

            foreach (var node in this.children)
            {
                var nx = node.GetGraphExtents();

                ext = new Vector3((nx.X > ext.X) ? nx.X : ext.X,
                                  (nx.Y > ext.Y) ? nx.Y : ext.Y,
                                  (nx.Z > ext.Z) ? nx.Z : ext.Z);
            }

            return ext;
        }
        #endregion
    }
}
