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
            this.A = a.ToVector3();
            this.B = b.ToVector3();
        }
        #endregion
        #region Properties
        public Vector3 A
        {
            get;
            set;
        }
        public Vector3 B
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
            return (this.A + this.B) / 2.0;
        }

        public Vector3 GetIntersectionX()
        {
            return this.GetIntersectionX(0.0);
        }

        /// <summary>
        /// Calculates the point at which the edge intersects the X plane.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public Vector3 GetIntersectionX(double x)
        {
            throw new NotImplementedException();
        }

        public Vector3 GetIntersectionY()
        {
            return this.GetIntersectionY(0.0);
        }

        /// <summary>
        /// Calculates the point at which the edge intersects the Y plane.
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector3 GetIntersectionY(double y)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the point at which the edge intersects the Z plane.
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        public Vector3 GetIntersectionZ(double z)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
