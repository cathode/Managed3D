/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/

namespace Managed3D.Geometry
{
    /// <summary>
    /// Represents modes for calculating the radius of a polygon or polygonal object.
    /// </summary>
    public enum RadiusMode
    {
        /// <summary>
        /// The radius is calculated from the center of the object to each vertex of the polygon.
        /// </summary>
        Vertex = 0x0,

        /// <summary>
        /// The radius is calculated from the center of the object to the midpoint of each edge of the polygon.
        /// </summary>   
        Edge = 0x1,

        /// <summary>
        /// The radicus is calculated from the center of the object to the incenter of the face.
        /// </summary>
        Face = 0x2,
    }
}
