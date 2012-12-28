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

        /// <summary>
        /// Backing field for the <see cref="Camera.ModifierLock"/> property.
        /// </summary>
        private CameraModifierLock modifierLock;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Managed3D.SceneGraph.Camera"/> class.
        /// </summary>
        public Camera()
        {
            this.fieldOfView = Angle.FromDegrees(45);
            this.VisibleGroups = VisibilityGroup.All;
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

        public CameraModifierLock ModifierLock
        {
            get
            {
                return this.modifierLock;
            }
            set
            {
                this.modifierLock = value;
            }
        }

        /// <summary>
        /// Gets or sets a predefined value that indicates the facing of the camera.
        /// </summary>
        public CameraFacing Facing
        {
            get;
            set;
        }

        public VisibilityGroup VisibleGroups
        {
            get;
            set;
        }
        #endregion
        #region Methods

        public static Camera CreateIsometric()
        {
            var cam = new Camera();
            cam.mode = CameraMode.Orthographic;
            cam.Orientation = new Quaternion(Vector3.Up, Angle.FromDegrees(45)) * new Quaternion(Vector3.Left, Angle.FromDegrees(45));
            cam.Position = new Vector3(0, 0, 0);

            return cam;
        }

        public static Camera CreateWithFacing(CameraFacing facing)
        {
            var cam = new Camera();
            cam.mode = CameraMode.Orthographic;
            cam.Facing = facing;
            cam.UpdateFacing();
            return cam;
        }

        public void UpdateFacing()
        {
            switch (this.Facing)
            {
                case CameraFacing.Up:
                    this.Orientation = Quaternion.LookAt(Vector3.Up);
                    break;

                case CameraFacing.Down:
                    this.Orientation = Quaternion.LookAt(Vector3.Down);
                    break;

                case CameraFacing.East:
                    this.Orientation = Quaternion.LookAt(Vector3.Right);
                    break;

                case CameraFacing.West:
                    this.Orientation = Quaternion.LookAt(Vector3.Left);
                    break;

                case CameraFacing.North:
                    this.Orientation = Quaternion.LookAt(Vector3.Forward);
                    break;

                case CameraFacing.South:
                    this.Orientation = Quaternion.LookAt(Vector3.Backward);
                    break;

                case CameraFacing.Isometric:
                    this.Orientation = new Quaternion(Vector3.Left, Angle.FromDegrees(45)) * new Quaternion(Vector3.Up, Angle.FromDegrees(45));
                    break;
            }
        }
        #endregion
    }
}
