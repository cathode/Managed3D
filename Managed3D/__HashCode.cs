/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Managed3D
{
    internal static class __HashCode
    {
        private static readonly int[] seeds = new int[] { 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67 };

        internal static int Calculate(params object[] fields)
        {
            int seed = seeds[fields.Length % seeds.Length];
            int prefix = seeds[(seed + 2) % seeds.Length];
            int result = seed;
            unchecked
            {
                for (int i = 0; i < fields.Length; i++)
                    if (fields[i] != null)
                        result += prefix + (result ^ fields[i].GetHashCode());
            }
            return result;
        }
    }
}
