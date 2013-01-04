/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Diagnostics.Contracts;

namespace Managed3D.Platform
{
    /// <summary>
    /// Represents various options for a display device.
    /// </summary>
    public sealed class DisplayProfile
    {
        #region Fields
        private static readonly DisplayProfile genericVGA = new DisplayProfile(640, 480);
        private static readonly DisplayProfile genericWVGA = new DisplayProfile(800, 480);
        private static readonly DisplayProfile genericWVGAPlus = new DisplayProfile(854, 480);
        private static readonly DisplayProfile genericSVGA = new DisplayProfile(800, 600);
        private static readonly DisplayProfile genericWSVGA = new DisplayProfile(1024, 600);
        private static readonly DisplayProfile genericXGA = new DisplayProfile(1024, 768);
        private static readonly DisplayProfile genericXGAPlus = new DisplayProfile(1152, 864);
        private static readonly DisplayProfile generic720p = new DisplayProfile(1280, 720);
        private static readonly DisplayProfile genericWXGA = new DisplayProfile(1280, 768);
        private static readonly DisplayProfile genericWXGA1610 = new DisplayProfile(1280, 800);
        private static readonly DisplayProfile genericUVGA = new DisplayProfile(1280, 960);
        private static readonly DisplayProfile genericSXGA = new DisplayProfile(1280, 1024);
        private static readonly DisplayProfile genericHD = new DisplayProfile(1366, 768);
        private static readonly DisplayProfile genericSXGAPlus = new DisplayProfile(1400, 1050);
        private static readonly DisplayProfile genericWXGAPlus = new DisplayProfile(1440, 900);
        private static readonly DisplayProfile genericHDPlus = new DisplayProfile(1600, 900);
        private static readonly DisplayProfile genericUXGA = new DisplayProfile(1600, 1200);
        private static readonly DisplayProfile genericWSXGAPlus = new DisplayProfile(1680, 1050);
        private static readonly DisplayProfile genericFHD = new DisplayProfile(1920, 1080);
        private static readonly DisplayProfile genericWUXGA = new DisplayProfile(1920, 1200);
        private static readonly DisplayProfile genericQWXGA = new DisplayProfile(2048, 1152);
        private static readonly DisplayProfile genericWQHD = new DisplayProfile(2560, 1440);
        private static readonly DisplayProfile genericWQXGA = new DisplayProfile(2560, 1600);

        /// <summary>
        /// Backing field for the <see cref="DisplayProfile.Depth"/> property.
        /// </summary>
        private int depth;

        /// <summary>
        /// Backing field for the <see cref="DisplayProfile.DeviceId"/> property.
        /// </summary>
        private int deviceId;

        /// <summary>
        /// Backing field for the <see cref="DisplayProfile.Fullscreen"/> property.
        /// </summary>
        private bool fullscreen;

        /// <summary>
        /// Backing field for the <see cref="DisplayProfile.Height"/> property.
        /// </summary>
        private int height;

        /// <summary>
        /// Backing field for the <see cref="DisplayProfile.ProfileId"/> property.
        /// </summary>
        private int profileId;

        /// <summary>
        /// Backing field for the <see cref="DisplayProfile.RefreshRate"/> property.
        /// </summary>
        private double refreshRate;

        /// <summary>
        /// Backing field for the <see cref="DisplayProfile.Width"/> property.
        /// </summary>
        private int width;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayProfile"/> class.
        /// </summary>
        /// <param name="width">The width in pixels of the new <see cref="DisplayProfile"/>.</param>
        /// <param name="height">The height in pixels of the new <see cref="DisplayProfile"/>.</param>
        public DisplayProfile(int width, int height)
        {
            Contract.Requires(width > 0);
            Contract.Requires(height > 0);

            this.width = width;
            this.height = height;
            this.refreshRate = 60.0;
            this.depth = 8;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the default <see cref="DisplayProfile"/>.
        /// </summary>
        public static DisplayProfile Default
        {
            get
            {
                return DisplayProfile.GenericVGA;
            }
        }

        public static DisplayProfile GenericVGA
        {
            get
            {
                return DisplayProfile.genericVGA;
            }
        }

        /// <summary>
        /// Gets a device-independent <see cref="DisplayProfile"/> for WVGA (800 x 480).
        /// </summary>
        public static DisplayProfile GenericWVGA
        {
            get
            {
                return DisplayProfile.genericWVGA;
            }
        }

        /// <summary>
        /// Gets a device-independent <see cref="DisplayProfile"/> for WVGA+ (854 x 480).
        /// </summary>
        public static DisplayProfile GenericWVGAPlus
        {
            get
            {
                return DisplayProfile.genericWVGAPlus;
            }
        }

        /// <summary>
        /// Gets a device-independant <see cref="DisplayProfile"/> for SVGA (800 x 600).
        /// </summary>
        public static DisplayProfile GenericSVGA
        {
            get
            {
                return DisplayProfile.genericSVGA;
            }
        }

        /// <summary>
        /// Gets a device-independent <see cref="DisplayProfile"/> for WSVGA (1024 x 600).
        /// </summary>
        public static DisplayProfile GenericWSVGA
        {
            get
            {
                return DisplayProfile.genericWSVGA;
            }
        }

        /// <summary>
        /// Gets a device-independent <see cref="DisplayProfile"/> for 720p HDTV (1280x720).
        /// </summary>
        public static DisplayProfile Generic720p
        {
            get
            {
                return DisplayProfile.generic720p;
            }
        }

        /// <summary>
        /// Gets a device-independent <see cref="DisplayProfile"/> for WXGA (1280x768).
        /// </summary>
        public static DisplayProfile GenericWXGA
        {
            get
            {
                return DisplayProfile.genericWXGA;
            }
        }

        /// <summary>
        /// Gets a generic <see cref="DisplayProfile"/> for WSXGA+ (1680 x 1050).
        /// </summary>
        public static DisplayProfile GenericWSXGAPlus
        {
            get
            {
                return DisplayProfile.genericWSXGAPlus;
            }
        }

        /// <summary>
        /// Gets a generic <see cref="DisplayProfile"/> for WQHD (2560 x 1440).
        /// </summary>
        public static DisplayProfile GenericWQHD
        {
            get
            {
                return DisplayProfile.genericWQHD;
            }
        }
        /// <summary>
        /// Gets a generic <see cref="DisplayProfile"/> for WQXGA (2560 x 1600).
        /// </summary>
        public static DisplayProfile GenericWQXGA
        {
            get
            {
                return DisplayProfile.genericWQXGA;
            }
        }

        /// <summary>
        /// Gets or sets the adapter-specific id of the <see cref="DisplayDevice"/> for which the current profile
        /// pertains to.
        /// </summary>
        public int DeviceId
        {
            get
            {
                return this.deviceId;
            }
            set
            {
                this.deviceId = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the current profile represents a full-screen display mode.
        /// </summary>
        public bool Fullscreen
        {
            get
            {
                return this.fullscreen;
            }
            set
            {
                this.fullscreen = value;
            }
        }

        /// <summary>
        /// Gets or sets the device-specific ID of the current <see cref="DisplayProfile"/>.
        /// </summary>
        public int ProfileId
        {
            get
            {
                return this.profileId;
            }
            set
            {
                this.profileId = value;
            }
        }

        /// <summary>
        /// Gets or sets the color depth (bits per pixel) of the current <see cref="DisplayProfile"/>.
        /// </summary>
        public int Depth
        {
            get
            {
                return this.depth;
            }
            set
            {
                this.depth = value;
            }
        }

        /// <summary>
        /// Gets or sets the refresh rate (Hz) of the display device.
        /// </summary>
        public double RefreshRate
        {
            get
            {
                return this.refreshRate;
            }
            set
            {
                this.refreshRate = value;
            }
        }

        /// <summary>
        /// Gets the height in pixels of the current <see cref="DisplayProfile"/>.
        /// </summary>
        public int Height
        {
            get
            {
                return this.height;
            }
        }

        /// <summary>
        /// Gets the width in pixels of the current <see cref="DisplayProfile"/>.
        /// </summary>
        public int Width
        {
            get
            {
                return this.width;
            }
        }
        #endregion
        #region Methods
        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.Width > 0);
            Contract.Invariant(this.Height > 0);
        }
        #endregion
    }
}
