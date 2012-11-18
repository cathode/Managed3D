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
using Managed3D.Platform;

namespace Managed3D.Rendering.OpenGL
{
    [CLSCompliant(false)]
    public static partial class GL
    {
        #region Fields
        public const string DLL = "OpenGL32.dll";
        #region AttribMask
        internal const int GL_CURRENT_BIT = 0x00000001;
        internal const int GL_POINT_BIT = 0x00000002;
        internal const int GL_LINE_BIT = 0x00000004;
        internal const int GL_POLYGON_BIT = 0x00000008;
        internal const int GL_POLYGON_STIPPLE_BIT = 0x00000010;
        internal const int GL_PIXEL_MODE_BIT = 0x00000020;
        internal const int GL_LIGHTING_BIT = 0x00000040;
        internal const int GL_FOG_BIT = 0x00000080;
        internal const int GL_DEPTH_BUFFER_BIT = 0x00000100;
        internal const int GL_ACCUM_BUFFER_BIT = 0x00000200;
        internal const int GL_STENCIL_BUFFER_BIT = 0x00000400;
        internal const int GL_VIEWPORT_BIT = 0x00000800;
        internal const int GL_TRANSFORM_BIT = 0x00001000;
        internal const int GL_ENABLE_BIT = 0x00002000;
        internal const int GL_COLOR_BUFFER_BIT = 0x00004000;
        internal const int GL_HINT_BIT = 0x00008000;
        internal const int GL_EVAL_BIT = 0x00010000;
        internal const int GL_LIST_BIT = 0x00020000;
        internal const int GL_TEXTURE_BIT = 0x00040000;
        internal const int GL_SCISSOR_BIT = 0x00080000;
        internal const uint GL_ALL_ATTRIB_BITS = 0xFFFFFFFF;
        #endregion
     
        #region HintTarget
        internal const int GL_PERSPECTIVE_CORRECTION_HINT = 0x0C50;
        internal const int GL_POINT_SMOOTH_HINT = 0x0C51;
        internal const int GL_LINE_SMOOTH_HINT = 0x0C52;
        internal const int GL_POLYGON_SMOOTH_HINT = 0x0C53;
        internal const int GL_FOG_HINT = 0x0C54;
        #endregion
        public const int GL_DEPTH_TEST = 0;
        public const int GL_LEQUAL = 0;
        #endregion
        #region Methods
        [DllImport(GL.DLL, EntryPoint = "glClear")]
        public static extern void Clear(int mask);

        [DllImport(GL.DLL, EntryPoint = "glClearColor")]
        public static extern void ClearColor(float p, float p_2, float p_3, float p_4);

        [DllImport(GL.DLL, EntryPoint = "glClearDepth")]
        public static extern void ClearDepth(double depth);

        [DllImport(GL.DLL, EntryPoint = "glViewport")]
        public static extern void Viewport(int x, int y, int width, int height);

        [DllImport(GL.DLL, EntryPoint = "glMatrixMode")]
        public static extern void MatrixMode(MatrixMode mode);

        [DllImport(GL.DLL, EntryPoint = "glLoadIdentity")]
        public static extern void LoadIdentity();

        [DllImport(GL.DLL, EntryPoint = "glShadeModel")]
        public static extern void ShadeModel(ShadeModel mode);

        [DllImport(GL.DLL, EntryPoint = "glEnable")]
        public static extern void Enable(int p);

        [DllImport(GL.DLL, EntryPoint = "glDepthFunc")]
        public static extern void DepthFunc(int p);

        [DllImport(GL.DLL, EntryPoint = "glHint")]
        public static extern void Hint(HintTarget target, HintMode mode);

        [DllImport(GL.DLL, EntryPoint = "glTranslated")]
        public static extern void Translate(double x, double y, double z);

        [DllImport(GL.DLL, EntryPoint = "glVertex3d")]
        public static extern void Vertex3(double x, double y, double z);

        [DllImport(GL.DLL, EntryPoint = "glBegin")]
        public static extern void Begin(BeginMode mode);

        [DllImport(GL.DLL, EntryPoint = "glEnd")]
        public static extern void End();

        [DllImport(GL.DLL, EntryPoint = "glRotated")]
        public static extern void Rotate(double angle, double x, double y, double z);

        [DllImport(GL.DLL, EntryPoint = "glBindBuffer")]
        public static extern void BindBuffer(int target, uint buffer);

        [DllImport(GL.DLL, EntryPoint = "glPolygonMode")]
        public static extern void PolygonMode(MaterialFace face, PolygonMode mode);
        //public static extern void LightModel(int model, float[]
        #endregion
    }
}
