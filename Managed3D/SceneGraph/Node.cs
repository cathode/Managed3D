/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Managed3D.Geometry;

namespace Managed3D.SceneGraph
{
    /// <summary>
    /// Represents a node within the scene graph.
    /// </summary>
    public class Node : ICollection<Node>
    {
        #region Fields
        private readonly LinkedList<Node> parents = new LinkedList<Node>();
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
            Contract.Requires(children != null);

            foreach (var c in children)
                if (c != null)
                    this.Add(c);
        }

        public Node(params Mesh3[] renderables)
        {
            foreach (var r in renderables)
                if (r != null)
                    this.renderables.Add(r);
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
                foreach (var node in this.children)
                    if (node != null && !node.IsGeometryStatic)
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

        /// <summary>
        /// Gets the <see cref="Node"/> that is the parent of the current node, or null if there is no parent.
        /// </summary>
        public Node Parent
        {
            get
            {
                if (this.parents.Count > 0)
                    return this.parents.First.Value;
                else
                    return null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current node has a parent node relationship.
        /// </summary>
        public bool HasParent
        {
            get
            {
                return this.Parent != null;
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
            if (item != null)
            {
                if (item.ContainsDeep(this))
                    throw new InvalidOperationException("A recursive node addition was detected");

                if (!this.ContainsDeep(item))
                {
                    item.parents.AddFirst(this);
                    this.children.Add(item);
                }
            }
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
            if (!Node.ReferenceEquals(item, null))
                return this.children.Contains(item);

            return false;
        }

        /// <summary>
        /// Determines if the current <see cref="Node"/> contains the specified item,
        /// and searches all children for the specified item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool ContainsDeep(Node item)
        {
            if (!Node.ReferenceEquals(item, null))
                foreach (var node in this.children)
                    if (node != null)
                        if (node == item || node.ContainsDeep(item))
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
            if (item != null)
                if (this.children.Remove(item))
                    return item.parents.Remove(this);

            return false;
        }

        /// <summary>
        /// Calculates the extents of the current node.
        /// </summary>
        /// <returns></returns>
        public virtual Extents3 GetExtents()
        {
            double x1 = 0, y1 = 0, z1 = 0, x2 = 0, y2 = 0, z2 = 0;

            var pos = this.GetWorldPosition();
            Quaternion rot = this.Orientation;
            var rm = rot.ToRotationMatrix();
            Matrix4 m = Matrix4.Identity;
            if (this.TransformOrder == SceneGraph.TransformOrder.TranslateRotateScale)
            {
                m = rm * Matrix4.CreateTranslationMatrix(pos);
            }
            else
            {
                m = Matrix4.CreateTranslationMatrix(pos) * rm;
            }

            foreach (var mesh in this.Renderables)
                foreach (var poly in mesh.Polygons)
                    foreach (var vt in poly.Vertices)
                    {
                        Vertex3 v = m * vt;

                        x1 = (v.X > x1) ? v.X : x1;
                        x2 = (v.X < x2) ? v.X : x2;

                        y1 = (v.Y > y1) ? v.Y : y1;
                        y2 = (v.Y < y2) ? v.Y : y2;

                        z1 = (v.Z > z1) ? v.Z : z1;
                        z2 = (v.Z < z2) ? v.Z : z2;
                    }


            var v1 = new Vector3(x1, y1, z1);
            var v2 = new Vector3(x2, y2, z2);

            return new Extents3(v1, v2);
        }

        /// <summary>
        /// Gets the absolute orientation of the current node, relative to the world
        /// </summary>
        /// <returns></returns>
        public Quaternion GetWorldOrientation()
        {

            if (this.parents.Count == 0)
                return this.Orientation;

            var parent = this.Parent;

            var prot = parent.GetWorldOrientation();
            return this.Orientation * prot;
        }

        /// <summary>
        /// Recursively calculates the geometry extents of the current node and
        /// all child nodes of the current node.
        /// </summary>
        /// <returns></returns>
        public Extents3 GetGraphExtents()
        {
            var ext = this.GetExtents();

            foreach (var node in this.children)
                if (node != null)
                    ext |= node.GetGraphExtents();

            return ext;
        }

        /// <summary>
        /// Determines the node's position, relative to the position of each of it's parents.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetWorldPosition()
        {
            if (this.parents.Count == 0)
                return this.Position;

            var parent = this.Parent;

            var ppos = parent.GetWorldPosition();

            return ppos + this.Position;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Defines invariant contracts for the <see cref="Node"/> class.
        /// </summary>
        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.children != null);
            Contract.Invariant(this.parents != null);
        }
        #endregion
    }
}
