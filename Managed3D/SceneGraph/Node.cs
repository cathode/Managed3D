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
    public class Node
    {
        #region Fields
        private readonly Stack<Node> parents = new Stack<Node>();
        private readonly List<Node> children = new List<Node>();
        private readonly List<Constraint> constraints = new List<Constraint>();
        private readonly List<Mesh3> renderables = new List<Mesh3>();
        private Vector3 position = Node.DefaultPosition;
        private Vector3 scale = Node.DefaultScale;
        private Quaternion orientation = Node.DefaultOrientation;
        private NodeRenderFlags renderFlags;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        public Node()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        /// <param name="children"></param>
        public Node(params Node[] children)
        {
            this.children.AddRange(children);
        }

        public Node(params Mesh3[] renderables) : this()
        {
            this.renderables.AddRange(renderables);
        }
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

        public static Quaternion DefaultOrientation
        {
            get
            {
                return new Quaternion(Vector3.Up, 0.0);
            }
        }

        public static Vector3 DefaultPosition
        {
            get
            {
                return Vector3.Zero;
            }
        }

        public static Vector3 DefaultScale
        {
            get
            {
                return new Vector3(1, 1, 1);
            }
        }

        public TransformOrder TransformOrder
        {
            get;
            set;
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
        /// Gets or sets a <see cref="Quaternion"/> which determines the orientation of the current <see cref="Node"/> relative to it's parent.
        /// </summary>
        public Quaternion Orientation
        {
            get
            {
                return this.orientation;
            }
            set
            {
                this.orientation = value;
            }
        }

        /// <summary>
        /// Gets or sets an <see cref="Vector3"/> which determines the position of the current <see cref="Node"/> relative to it's parent.
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
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
        /// Gets or sets an <see cref="Vector3"/> which determines the scale of the current <see cref="Node"/> relative to it's parent.
        /// </summary>
        public Vector3 Scale
        {
            get
            {
                return this.scale;
            }
            set
            {
                this.scale = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the current <see cref="Node"/> contains any child nodes.
        /// </summary>
        public virtual bool HasChildren
        {
            get
            {
                return this.children.Count > 0;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="PositionSpace"/> that indicates how the node's orientation is interpreted by the renderer.
        /// </summary>
        public ReferenceSpace OrientationSpace
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <see cref="PositionSpace"/> that indicates how the node's position is interpreted by the renderer.
        /// </summary>
        public ReferenceSpace PositionSpace
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <see cref="PositionSpace"/> that indicates how the node's scale is interpreted by the renderer.
        /// </summary>
        public ReferenceSpace ScalingSpace
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value that indicates if the current node, and all of it's child nodes, contain only static geometry.
        /// </summary>
        public virtual bool IsGeometryStatic
        {
            get
            {
                foreach (var child in this.children)
                    if (!child.IsGeometryStatic)
                        return false;

                return true;
            }
        }

        /// <summary>
        /// Gets a collection of child nodes of the current <see cref="Node"/>.
        /// </summary>
        public IEnumerable<Node> Children
        {
            get
            {
                return this.children;
            }
        }

        public List<Mesh3> Renderables
        {
            get
            {
                return this.renderables;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates the <see cref="VisibilityGroup"/>(s) which the current node belongs to.
        /// Cameras can be configured to show or hide specific visibility groups, preventing entire node branches from
        /// being rendered if the topmost node in the branch would be hidden.
        /// </summary>
        public VisibilityGroup Visibility
        {
            get;
            set;
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
            throw new NotImplementedException();
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

        public Vector3 GetWorldPosition()
        {
            if (this.parents.Count == 0)
                return this.Position;

            var parent = this.parents.Peek();

            var ppos = parent.GetWorldPosition();

            return ppos + this.Position;
        }
        
        #endregion
    }
}
