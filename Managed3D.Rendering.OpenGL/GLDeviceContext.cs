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

namespace Managed3D.Rendering.OpenGL
{
    internal sealed class GLDeviceContext : IDisposable
    {
        #region Fields
        private bool isDisposed;
        #endregion
        #region Constructors
        private GLDeviceContext()
        {

        }
        #endregion
        #region Properties
        public bool IsDisposed
        {
            get
            {
                return this.isDisposed;
            }
        }
        #endregion
        #region Methods
        public void Dispose()
        {
            if (this.IsDisposed)
                return;

            try
            {

            }
            catch
            {
                return;
            }
            this.isDisposed = true;
        }
        #endregion
    }
}
