/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Managed3D.Geometry;

namespace Managed3D.SceneGraph
{
    /// <summary>
    /// Represents a point of view and associated properties.
    /// </summary>
    public sealed class Camera : Node
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="Camera.FieldOfView"/> property.
        /// </summary>
        private Angle fieldOfView;

        /// <summary>
        /// Backing field for the <see cref="Camera.FocalDistance"/> property.
        /// </summary>
        private double focalDistance;

        /// <summary>
        /// Backing field for the <see cref="Camera.Mode"/> property.
        /// </summary>
        private CameraMode mode;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Managed3D.SceneGraph.Camera"/> class.
        /// </summary>
        public Camera()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Managed3D.SceneGraph.Camera"/> class
        /// with the specified field-of-view.
        /// </summary>
        /// <param name="fieldOfView">An <see cref="Managed3D.Angle"/> determining the new
        /// camera's field-of-view range.</param>
        public Camera(Angle fieldOfView)
        {
            this.fieldOfView = fieldOfView;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the field-of-view of the current <see cref="Managed3D.SceneGraph.Camera"/>.
        /// </summary>
        public Angle FieldOfView
        {
            get
            {
                return this.fieldOfView;
            }
            set
            {
                this.fieldOfView = value;
            }
        }

        /// <summary>
        /// Gets or sets the focal distance of the current <see cref="Managed3D.SceneGraph.Camera"/>
        /// </summary>
        public double FocalDistance
        {
            get
            {
                return this.focalDistance;
            }
            set
            {
                this.focalDistance = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Managed3D.SceneGraph.CameraMode"/> of the current
        /// <see cref="Managed3D.SceneGraph.Camera"/>.
        /// </summary>
        public CameraMode Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                this.mode = value;
            }
        }
        #endregion
        #region Methods

        public static Camera CreateIsometric()
        {
            var cam = new Camera();
            cam.mode = CameraMode.Isometric;
            cam.Orientation = new Vector3(45, 35.264, 0);
            cam.Position = new Vector3(0, 0, 0);

            return cam;
        }
        #endregion
    }
}
