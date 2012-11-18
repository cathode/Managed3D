/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;

namespace Managed3D.UI
{
    /// <summary>
    /// A user-interface element that exists as a quad in 3d space. 
    /// </summary>
    public abstract class Element
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="Element.Enabled"/> property.
        /// </summary>
        private bool enabled;        /// <summary>
        /// Backing field for the <see cref="Element.Visible"/> property.
        /// </summary>
        private bool visible;        /// <summary>
        /// Backing field for the <see cref="Element.Children"/> property.
        /// </summary>
        private readonly ElementChildrenCollection children;        /// <summary>
        /// Backing field for the <see cref="Element.Parent"/> property.
        /// </summary>
        private Element parent;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Managed3D.UI.Element"/> class.
        /// </summary>
        public Element()
        {
            this.children = new ElementChildrenCollection(this);
        }        /// <summary>
        /// Initializes a new instance of the <see cref="Managed3D.UI.Element"/> class with the specified controls as children.
        /// </summary>
        /// <param name="children">Child elements of the new <see cref="Element"/>.</param>
        public Element(params Element[] children)
            : this()
        {

        }
        #endregion
        #region Events
        /// <summary>
        /// Raised when the current <see cref="Managed3D.UI.Element"/> becomes enabled or disabled.
        /// </summary>
        public event EventHandler EnabledChanged;        /// <summary>
        /// Raised when the current <see cref="Managed3D.UI.Element"/> becomes visible or hidden.
        /// </summary>
        public event EventHandler VisibleChanged;
        #endregion
        #region Properties
        /// <summary>
        /// Gets a list of all the child elements.
        /// </summary>
        public ElementChildrenCollection Children
        {
            get
            {
                return this.children;
            }
        }      
        
        /// <summary>
        /// Gets the parent-child depth of the current <see cref="Element"/>.
        /// </summary>
        public int Depth
        {
            get
            {
                return this.HasParent ? this.Parent.Depth + 1 : 0;
            }
        }      
        
        /// <summary>
        /// Gets the <see cref="Element"/> that is the parent of the current <see cref="Element"/>.
        /// </summary>
        public Element Parent
        {
            get
            {
                return this.parent;
            }
            internal set
            {
                this.parent = value;
            }
        }     
        
        /// <summary>
        /// Gets a value indicating whether the current <see cref="Element"/> has a parent element relationship.
        /// </summary>
        public bool HasParent
        {
            get
            {
                return this.parent != null;
            }
        }      
        
        /// <summary>
        /// Gets or sets a value indicating whether the current <see cref="Element"/> can receive user input.
        /// </summary>
        /// <remarks>
        /// Subscribe to the <see cref="Element.EnabledChanged"/> event to be notified when the value of this property changes.
        /// </remarks>
        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                if (this.enabled != value)
                {
                    this.enabled = value;
                    this.OnEnabledChanged(EventArgs.Empty);
                }
            }
        }   
        
        /// <summary>
        /// Gets or sets a value indicating whether the current <see cref="Element"/> will be rendered.
        /// </summary>
        /// <remarks>
        /// Subscribe to the <see cref="Element.VisibleChanged"/> event to be notified when the value of this property changes.
        /// </remarks>
        public bool Visible
        {
            get
            {
                return this.visible;
            }
            set
            {
                if (this.visible != value)
                {
                    this.visible = value;
                    this.OnVisibleChanged(EventArgs.Empty);
                }
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Raises the <see cref="Element.EnabledChanged"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnEnabledChanged(EventArgs e)
        {
            if (this.EnabledChanged != null)
                this.EnabledChanged(this, e);
        }       
        
        /// <summary>
        /// Raises the <see cref="Element.VisibleChanged"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnVisibleChanged(EventArgs e)
        {
            if (this.VisibleChanged != null)
                this.VisibleChanged(this, e);
        }
        #endregion
    }
}
