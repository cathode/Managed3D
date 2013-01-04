/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;

namespace Managed3D.Platform
{
    /// <summary>
    /// Represents the area on a display device where rendered frames are displayed.
    /// </summary>
    public abstract class DisplaySurface : IDisposable
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="DisplaySurface.Left"/> property.
        /// </summary>
        private int left;      
        
        /// <summary>
        /// Backing field for the <see cref="DisplaySurface.Top"/> property.
        /// </summary>
        private int top;      
        
        /// <summary>
        /// Backing field for the <see cref="DisplaySurface.Right"/> property.
        /// </summary>
        private int right;

        /// <summary>
        /// Backing field for the <see cref="DisplaySurface.Bottom"/> property.
        /// </summary>
        private int bottom;      
        
        /// <summary>
        /// Backing field for the <see cref="DisplaySurface.Profile"/> property.
        /// </summary>
        private DisplayProfile profile;  
        
        /// <summary>
        /// Backing field for the <see cref="DisplaySurface.IsDisposed"/> property.
        /// </summary>
        private bool isDisposed;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplaySurface"/> class.
        /// </summary>
        protected DisplaySurface()
        {
            this.profile = DisplayProfile.Default;
        }     
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplaySurface"/> class.
        /// </summary>
        /// <param name="profile">A <see cref="DisplayProfile"/> to use as the initial profile for the new <see cref="DisplaySurface"/>.</param>
        protected DisplaySurface(DisplayProfile profile)
        {
            this.profile = profile;
        }      
        
        /// <summary>
        /// Finalizes an instance of the <see cref="DisplaySurface"/> class.
        /// </summary>
        ~DisplaySurface()
        {
            this.Dispose(false);
        }
        #endregion
        #region Events
        /// <summary>
        /// Raised when the value of the <see cref="DisplaySurface.Profile"/> property changes.
        /// </summary>
        public event EventHandler ProfileChanged;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the position of the left edge of the display surface.
        /// </summary>
        public int Left
        {
            get
            {
                return this.left;
            }
            set
            {
                this.left = value;
            }
        }     
        
        /// <summary>
        /// Gets or sets the position of the top edge of the display surface.
        /// </summary>
        public int Top
        {
            get
            {
                return this.top;
            }
            set
            {
                this.top = value;
            }
        }    
        
        /// <summary>
        /// Gets or sets the position of the right edge of the display surface.
        /// </summary>
        public int Right
        {
            get
            {
                return this.right;
            }
            set
            {
                this.right = value;
            }
        }     
        
        /// <summary>
        /// Gets or sets the position of the bottom edge of the display surface.
        /// </summary>
        public int Bottom
        {
            get
            {
                return this.bottom;
            }
            set
            {
                this.bottom = value;
            }
        }     
        
        /// <summary>
        /// Gets or sets a value indicating whether the current <see cref="DisplaySurface"/> is disposed.
        /// </summary>
        public bool IsDisposed
        {
            get
            {
                return this.isDisposed;
            }
            protected set
            {
                this.isDisposed = value;
            }
        }       
        
        /// <summary>
        /// Gets or sets the active <see cref="DisplayProfile"/> for the current <see cref="DisplaySurface"/>.
        /// </summary>
        public DisplayProfile Profile
        {
            get
            {
                return this.profile;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                else if (this.profile == value)
                    return;
                this.profile = value;
                this.OnProfileChanged(EventArgs.Empty);
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Cleans up managed and unmanaged resources used by the current <see cref="DisplaySurface"/>.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }     
        
        /// <summary>
        /// Raises the <see cref="DisplaySurface.ProfileChanged"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        public virtual void OnProfileChanged(EventArgs e)
        {
            var handler = this.ProfileChanged;
            if (handler != null)
                handler(this, e);
        }    
        
        /// <summary>
        /// Cleans up managed and unmanaged resources used by the current <see cref="DisplaySurface"/>.
        /// </summary>
        /// <param name="disposing">true to release managed resources, otherwise only unmanaged resources will be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.IsDisposed)
                this.IsDisposed = true;
        }

        #endregion
    }
}
