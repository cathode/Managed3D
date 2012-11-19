/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Managed3D.UI
{
    /// <summary>
    /// Represents a collection of child <see cref="Element"/> instances relative to a parent <see cref="Element"/>.
    /// </summary>
    public sealed class ElementChildrenCollection : ICollection<Element>
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="ElementChildrenCollection.Parent"/> property.
        /// </summary>
        private readonly Element parent;

        /// <summary>
        /// Holds the collection of <see cref="Element"/>s.
        /// </summary>
        private readonly Collection<Element> children;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementChildrenCollection"/> class.
        /// </summary>
        /// <param name="parent"></param>
        internal ElementChildrenCollection(Element parent)
        {
            this.parent = parent;
            this.children = new Collection<Element>();
        }
        #endregion
        #region Indexers
        public Element this[int index]
        {
            get
            {
                return this.children[index];
            }
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the <see cref="Element"/> that the children contained in this collection belong to.
        /// </summary>
        public Element Parent
        {
            get
            {
                return this.parent;
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="ElementChildrenCollection"/>.
        /// </summary>
        public int Count
        {
            get
            {
                return this.children.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ElementChildrenCollection"/> is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Adds an item to the <see cref="ElementChildrenCollection"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="NotSupportedException">The <see cref="ElementChildrenCollection"/> is read-only.</exception>
        public void Add(Element item)
        {
            if (this.IsReadOnly)
                throw new NotSupportedException("TODO: return localized exception message string.");
        }

        public void Clear()
        {
            while (this.Count > 0)
                this.Remove(this.children[0]);
        }

        public bool Contains(Element item)
        {
            return this.children.Contains(item);
        }

        public void CopyTo(Element[] array, int arrayIndex)
        {
            this.children.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the first occurence of a specific <see cref="Element"/> from the collection.
        /// </summary>
        /// <param name="item">The <see cref="Element"/> to remove from the collection.</param>
        /// <returns>
        /// true if the item was successfully removed from the collection; otherwise, false.
        /// This method also returns false if <paramref name="item"/> was null or was not
        /// found in the collection.
        /// </returns>
        /// <exception cref="NotSupportedException">The <see cref="ElementChildrenCollection"/> is read-only.</exception>
        public bool Remove(Element item)
        {
            if (this.IsReadOnly)
                throw new NotSupportedException();
            if (item == null)
                return false;
            if (item.Parent != this.Parent)
                return false;
            if (this.children.Remove(item))
            {
                item.Parent = null;
                return true;
            }
            return false;
        }

        public IEnumerator<Element> GetEnumerator()
        {
            return this.children.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
