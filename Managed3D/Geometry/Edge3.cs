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
        public Edge3(Vertex3 p, Vertex3 q)
        {
            this.P = p;
            this.Q = q;
        }
        #endregion
        #region Properties
        public Vertex3 P
        {
            get;
            set;
        }
        public Vertex3 Q
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
            throw new NotImplementedException();

            //return (this.P + this.Q) / 2.0;
        }

        public Vector3 GetIntersectionXY()
        {
            return this.GetIntersectionXY(0.0);
        }

        /// <summary>
        /// Calculates the point at which the edge intersects the XY plane.
        /// </summary>
        /// <param name="z">The offset of the XY plane along the Z axis.</param>
        /// <returns></returns>
        public Vector3 GetIntersectionXY(double z)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the point at which the edge intersects the XY plane.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetIntersectionYZ()
        {
            return this.GetIntersectionYZ(0.0);
        }

        /// <summary>
        /// Calculates the point at which the edge intersects the YZ plane.
        /// </summary>
        /// <param name="x">The offset of the YZ plane along the X axis.</param>
        /// <returns></returns>
        public Vector3 GetIntersectionYZ(double x)
        {
            var x1 = this.P.X;
            var x2 = this.Q.X;
            var y1 = this.P.Y;
            var y2 = this.Q.Y;
            var z1 = this.P.Z;
            var z2 = this.P.Z;

            var k1 = -x2 / (x1 - x2);
            var k2 = 1 - k1;
            var xf =  1.0 / (x2 - x1);

            //var xdist = 
            //k1 *= x;
            //k2 *= 1.0 - x;
            var isect = ((k1 * this.P.ToVector3()) + (this.Q.ToVector3() * k2)) * xf;

            return isect;
        }

        /// <summary>
        /// Calculates the edge's intersection with the ZX plane.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetIntersectionZX()
        {
            return this.GetIntersectionZX(0.0);
        }

        /// <summary>
        /// Calculates the point at which the edge intersects the ZX plane.
        /// </summary>
        /// <param name="y">The offset of the ZX plane along the Y axis.</param>
        /// <returns></returns>
        public Vector3 GetIntersectionZX(double y)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
