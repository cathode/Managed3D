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
using System.Runtime.InteropServices;

namespace Managed3D.Platform.Microsoft
{
    [CLSCompliant(false)]
    public static class Gdi32
    {
        #region Fields
        public const string DLL = "Gdi32.dll";
        #endregion
        #region Methods
        [DllImport(Gdi32.DLL)]
        public static extern int ChoosePixelFormat(IntPtr hdc, ref PixelFormatDescriptor pfd);

        [DllImport(Gdi32.DLL)]
        public static extern int DescribePixelFormat(IntPtr hdc, int pixelFormat, uint bytes, ref PixelFormatDescriptor pfd);

        [DllImport(Gdi32.DLL)]
        public static extern int GetPixelFormat(IntPtr hdc);

        [DllImport(Gdi32.DLL)]
        public static extern bool SetPixelFormat(IntPtr hdc, int pixelFormat, ref PixelFormatDescriptor pfd);
        #endregion
    }
}
