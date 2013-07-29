/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Runtime.InteropServices;
using Managed3D.Platform;

namespace Managed3D.Rendering.OpenGL
{
    /// <summary>
    /// Exposes unmanaged functions of the OpenGL Utility Toolkit (GLUT) library.
    /// </summary>
    [CLSCompliant(false)]
    public unsafe static class GLUT
    {
        #region Fields
        public const string DLL = "glut32.dll";
        #region Display Mode Bit Masks
        public const int RGB = 0;
        public const int RGBA = GLUT.RGB;
        public const int INDEX = 1;
        public const int SINGLE = 0;
        public const int DOUBLE = 2;
        public const int ACCUM = 4;
        public const int ALPHA = 8;
        public const int DEPTH = 16;
        public const int STENCIL = 32;
        public const int MULTISAMPLE = 128;
        public const int STEREO = 256;
        public const int LUMINANCE = 512;
        #endregion
        #region Mouse Buttons/Callbacks
        public const int LEFT_BUTTON = 0;
        public const int MIDDLE_BUTTON = 1;
        public const int RIGHT_BUTTON = 2;
        public const int DOWN = 0;
        public const int UP = 1;
        #endregion
        #endregion
        #region Methods
        #region Initialization
        [DllImport(GLUT.DLL, EntryPoint = "glutInit", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Init(int* argcp, string[] argv);

        [DllImport(GLUT.DLL, EntryPoint = "glutInitDisplayMode")]
        public static extern void InitDisplayMode(uint mode);

        [DllImport(GLUT.DLL, EntryPoint = "glutInitWindowPosition")]
        public static extern void InitWindowPosition(int x, int y);

        [DllImport(GLUT.DLL, EntryPoint = "glutInitWindowSize")]
        public static extern void InitWindowSize(int width, int height);

        [DllImport(GLUT.DLL, EntryPoint = "glutMainLoop")]
        public static extern void MainLoop();
        #endregion
        #region Windowing
        [DllImport(GLUT.DLL, EntryPoint = "glutCreateWindow")]
        public static extern int CreateWindow(string title);

        [DllImport(GLUT.DLL, EntryPoint = "glutCreateSubWindow")]
        public static extern int CreateSubWindow(int win, int x, int y, int width, int height);

        [DllImport(GLUT.DLL, EntryPoint = "glutDestroyWindow")]
        public static extern void DestroyWindow(int win);
        //public static extern void PostRedisplay();

        [DllImport(GLUT.DLL, EntryPoint = "glutSwapBuffers")]
        public static extern void SwapBuffers();

        [DllImport(GLUT.DLL, EntryPoint = "glutPostRedisplay")]
        public static extern void PostRedisplay();
        //public static extern int GetWindow();
        //public static extern void SetWindow(int win);
        #endregion
        #region Callback
        [DllImport(GLUT.DLL, EntryPoint = "glutDisplayFunc")]
        public static extern void DisplayFunc(Action callback);

        [DllImport(GLUT.DLL, EntryPoint = "glutIdleFunc")]
        public static extern void IdleFunc(Action callback);

        [DllImport(GLUT.DLL, EntryPoint = "glutTimerFunc")]
        public static extern void TimerFunc(int msecs, Action callback, int value);

        #endregion
        #endregion
    }
}
