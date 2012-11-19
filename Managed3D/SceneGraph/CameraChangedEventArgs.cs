/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;

using System.Text;

namespace Managed3D.SceneGraph
{
    public sealed class CameraChangedEventArgs : EventArgs
    {
        #region Constructors
        public CameraChangedEventArgs(Camera camera)
        {
            this.camera = camera;
        }
        #endregion
        #region Fields
        private Camera camera;
        #endregion
        #region Properties
        public Camera Camera
        {
            get
            {
                return this.camera;
            }
        }
        #endregion
    }
}
