/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents an edge of a polygon.
    /// </summary>
    public class Edge3
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Edge3"/> class.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public Edge3(Vertex3 a, Vertex3 b)
        {
            this.A = a;
            this.B = b;
        }
        #endregion
        #region Properties
        public Vertex3 A
        {
            get;
            set;
        }
        public Vertex3 B
        {
            get;
            set;
        }

        public Polygon3 Left
        {
            get;
            set;
        }
        public Polygon3 Right
        {
            get;
            set;
        }
        #endregion
        #region Methods
        public Vector3 GetCenter()
        {
            return (this.A.ToVector3() + this.B.ToVector3()) / 2.0;
        }
        #endregion
    }
}
