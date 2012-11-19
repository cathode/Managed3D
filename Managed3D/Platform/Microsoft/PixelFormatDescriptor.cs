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
    /// <summary>
    /// An interop type, representing the unmanaged PIXELFORMATDESCRIPTOR struct in the Win32 API.
    /// </summary>
    [CLSCompliant(false)]
    [StructLayoutAttribute(LayoutKind.Explicit, Size = 40, Pack = 1)]
    public sealed class PixelFormatDescriptor
    {
        #region Fields
        /// <summary>
        /// Used internally by Win32 API, this is always set to 40.
        /// </summary>
        [FieldOffset(0x00)]
        private readonly ushort size = 40;

        /// <summary>
        /// Used internally by Win32 API, this is always set to 1.
        /// </summary>
        [FieldOffset(0x02)]
        private ushort version = 1;

        [FieldOffset(0x04)]
        private PfdFlags flags;

        [FieldOffset(0x08)]
        private PfdPixelType pixelType;

        [FieldOffset(0x09)]
        private byte colorBits;

        [FieldOffset(0x0A)]
        private byte cRedBits;

        [FieldOffset(0x0B)]
        private byte cRedShift;

        [FieldOffset(0x0C)]
        private byte cGreenBits;

        [FieldOffset(0x0D)]
        private byte cGreenShift;

        [FieldOffset(0x0E)]
        private byte cBlueBits;

        [FieldOffset(0x0F)]
        private byte cBlueShift;

        [FieldOffset(0x10)]
        private byte cAlphaBits;

        [FieldOffset(0x11)]
        private byte cAlphaShift;

        [FieldOffset(0x12)]
        private byte cAccumBits;

        [FieldOffset(0x13)]
        private byte cAccumRedBits;

        [FieldOffset(0x14)]
        private byte cAccumGreenBits;

        [FieldOffset(0x15)]
        private byte cAccumBlueBits;

        [FieldOffset(0x16)]
        private byte cAccumAlphaBits;

        [FieldOffset(0x17)]
        private byte depthBits;

        [FieldOffset(0x18)]
        private byte stencilBits;

        [FieldOffset(0x19)]
        private byte cAuxBuffers;

        [FieldOffset(0x1A)]
        private byte iLayerType;

        [FieldOffset(0x1B)]
        private byte bReserved;

        [FieldOffset(0x1C)]
        private uint dwLayerMask;

        [FieldOffset(0x20)]
        private uint dwVisibleMask;

        [FieldOffset(0x24)]
        private uint dwDamageMask;
        #endregion
        #region Constructors
        public PixelFormatDescriptor()
        {

        }
        #endregion
        #region Properties
        public PfdFlags Flags
        {
            get
            {
                return this.flags;
            }
            set
            {
                this.flags = value;
            }
        }

        public PfdPixelType PixelType
        {
            get
            {
                return this.pixelType;
            }
            set
            {
                this.pixelType = value;
            }
        }

        public byte DepthBits
        {
            get
            {
                return this.depthBits;
            }
            set
            {
                this.depthBits = value;
            }
        }

        public byte StencilBits
        {
            get
            {
                return this.stencilBits;
            }
            set
            {
                this.stencilBits = value;
            }
        }
        #endregion
    }
}