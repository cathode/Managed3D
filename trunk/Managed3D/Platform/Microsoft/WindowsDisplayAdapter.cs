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

namespace Managed3D.Platform.Microsoft
{
    /// <summary>
    /// Represents a <see cref="DisplayAdapter"/> on a PC running Microsoft Windows operating system.
    /// </summary>
    public sealed class WindowsDisplayAdapter : DisplayAdapter
    {
        public override DisplayDevice PrimaryDevice
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override DisplayDevice[] GetDevices()
        {
            throw new NotImplementedException();
        }
    }
}
