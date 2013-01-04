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
    /// Represents a <see cref="DisplayProfileEnumerator"/> that is compatible with Microsoft Windows.
    /// </summary>
    public class WindowsDisplayProfileEnumerator
    {
        public DisplayProfile[] GetProfiles()
        {
            DISPLAY_DEVICE d = new DISPLAY_DEVICE();
            d.cb = Marshal.SizeOf(d);
            List<DisplayProfile> profiles = new List<DisplayProfile>();
            for (uint id = 0; EnumDisplayDevices(null, id, ref d, 0); id++)
            {
                //Console.WriteLine("--- Display Device {0} ---\r\nDeviceName: {1}\r\nDeviceString: {2}\r\nStateFlags: {3}\r\nDeviceID: {4}\r\nDeviceKey: {5}",
                //             id, d.DeviceName, d.DeviceString, d.StateFlags, d.DeviceID, d.DeviceKey);
                int i = 0;
                DEVMODE devmode = new DEVMODE();
                while (EnumDisplaySettings(d.DeviceName, i++, ref devmode))
                {
                    //Console.WriteLine("Device Mode {0}: {1}x{2}, {3}-bit, {4}hz",
                    //                  i, devmode.dmPelsWidth, devmode.dmPelsHeight, devmode.dmBitsPerPel, devmode.dmDisplayFrequency);
                    if (devmode.dmPelsWidth > 0 && devmode.dmPelsHeight > 0)
                        profiles.Add(new DisplayProfile(devmode.dmPelsWidth, devmode.dmPelsHeight)
                        {
                            DeviceId = (int)id,
                            RefreshRate = devmode.dmDisplayFrequency / 1.0,
                            Depth = devmode.dmBitsPerPel,
                            ProfileId = i,
                        });
                }
                d.cb = Marshal.SizeOf(d);
            }
            return profiles.ToArray();
        }

        #region Methods
        [DllImport("user32.dll")]
        private static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);
        [DllImport("user32.dll")]
        private static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

        #endregion
        #region Types
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct DISPLAY_DEVICE
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceString;
            [MarshalAs(UnmanagedType.U4)]
            public DisplayDeviceStateFlags StateFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceKey;
        }
        [Flags]
        enum DisplayDeviceStateFlags : int
        {
            /// <summary>The device is part of the desktop.</summary>
            AttachedToDesktop = 0x1,
            MultiDriver = 0x2,
            /// <summary>The device is part of the desktop.</summary>
            PrimaryDevice = 0x4,
            /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
            MirroringDriver = 0x8,
            /// <summary>The device is VGA compatible.</summary>
            VGACompatible = 0x16,
            /// <summary>The device is removable; it cannot be the primary display.</summary>
            Removable = 0x20,
            /// <summary>The device has more display modes than its output devices support.</summary>
            ModesPruned = 0x8000000,
            Remote = 0x4000000,
            Disconnect = 0x2000000
        }
        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
        struct DEVMODE
        {
            public const int CCHDEVICENAME = 32;
            public const int CCHFORMNAME = 32;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            [FieldOffset(0)]
            public string dmDeviceName;
            [FieldOffset(32)]
            public Int16 dmSpecVersion;
            [FieldOffset(34)]
            public Int16 dmDriverVersion;
            [FieldOffset(36)]
            public Int16 dmSize;
            [FieldOffset(38)]
            public Int16 dmDriverExtra;
            [FieldOffset(40)]
            public DM dmFields;
            [FieldOffset(44)]
            Int16 dmOrientation;
            [FieldOffset(46)]
            Int16 dmPaperSize;
            [FieldOffset(48)]
            Int16 dmPaperLength;
            [FieldOffset(50)]
            Int16 dmPaperWidth;
            [FieldOffset(52)]
            Int16 dmScale;
            [FieldOffset(54)]
            Int16 dmCopies;
            [FieldOffset(56)]
            Int16 dmDefaultSource;
            [FieldOffset(58)]
            Int16 dmPrintQuality;
            [FieldOffset(44)]
            public POINTL dmPosition;
            [FieldOffset(52)]
            public Int32 dmDisplayOrientation;
            [FieldOffset(56)]
            public Int32 dmDisplayFixedOutput;
            [FieldOffset(60)]
            public short dmColor;
            [FieldOffset(62)]
            public short dmDuplex;
            [FieldOffset(64)]
            public short dmYResolution;
            [FieldOffset(66)]
            public short dmTTOption;
            [FieldOffset(68)]
            public short dmCollate;
            [FieldOffset(72)]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
            public string dmFormName;
            [FieldOffset(102)]
            public Int16 dmLogPixels;
            [FieldOffset(104)]
            public Int32 dmBitsPerPel;
            [FieldOffset(108)]
            public Int32 dmPelsWidth;
            [FieldOffset(112)]
            public Int32 dmPelsHeight;
            [FieldOffset(116)]
            public Int32 dmDisplayFlags;
            [FieldOffset(116)]
            public Int32 dmNup;
            [FieldOffset(120)]
            public Int32 dmDisplayFrequency;
        }
        struct POINTL
        {
            public Int32 x;
            public Int32 y;
        }
        [Flags()]
        enum DM : int
        {
            Orientation = 0x1,
            PaperSize = 0x2,
            PaperLength = 0x4,
            PaperWidth = 0x8,
            Scale = 0x10,
            Position = 0x20,
            NUP = 0x40,
            DisplayOrientation = 0x80,
            Copies = 0x100,
            DefaultSource = 0x200,
            PrintQuality = 0x400,
            Color = 0x800,
            Duplex = 0x1000,
            YResolution = 0x2000,
            TTOption = 0x4000,
            Collate = 0x8000,
            FormName = 0x10000,
            LogPixels = 0x20000,
            BitsPerPixel = 0x40000,
            PelsWidth = 0x80000,
            PelsHeight = 0x100000,
            DisplayFlags = 0x200000,
            DisplayFrequency = 0x400000,
            ICMMethod = 0x800000,
            ICMIntent = 0x1000000,
            MediaType = 0x2000000,
            DitherType = 0x4000000,
            PanningWidth = 0x8000000,
            PanningHeight = 0x10000000,
            DisplayFixedOutput = 0x20000000
        }
        #endregion
    }
}
