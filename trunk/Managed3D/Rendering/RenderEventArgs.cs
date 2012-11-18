/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Drawing;

namespace Managed3D.Rendering
{
    public class RenderEventArgs : EventArgs
    {
        #region Constructors
        public RenderEventArgs()
        {
        }
        public RenderEventArgs(FrameOptions options)
        {
            this.Options = options;
        }
        #endregion
        #region Properties
        public FrameOptions Options
        {
            get;
            set;
        }
        #endregion
    }
}
