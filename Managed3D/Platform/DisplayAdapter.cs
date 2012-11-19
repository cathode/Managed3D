/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Managed3D.Platform
{
    /// <summary>
    /// Represents a video graphics adapter. Provides methods to enumerate connected display devices (monitors, projectors, televisions, etc).
    /// </summary>
    public abstract class DisplayAdapter
    {
        #region Constructors

        #endregion
        #region Properties
        /// <summary>
        /// Gets an array of all <see cref="DisplayDevice" />s connected to the current <see cref="DisplayAdapter"/>.
        /// </summary>
        public abstract DisplayDevice PrimaryDevice
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating if the current <see cref="DisplayAdapter"/> is a virtual device.
        /// </summary>
        public bool IsVirtual
        {
            get;
            protected set;
        }
        #endregion
        #region Methods
        public abstract DisplayDevice[] GetDevices();
        #endregion
    }
}
