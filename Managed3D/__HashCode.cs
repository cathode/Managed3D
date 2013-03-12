/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace Managed3D
{
    [ContractVerification(false)]
    internal static class __HashCode
    {
        /// <summary>
        /// Holds all prime numbers less than 1000 to use for seeding the hash function.
        /// </summary>
        private static readonly int[] seeds = new int[] 
        {
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61,
            67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137,
            139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199,
            211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277,
            281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359,
            367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439,
            443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521,
            523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607,
            613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683,
            691, 701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 
            787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 
            877, 881, 883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 
            971, 977, 983, 991, 997
        };

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
