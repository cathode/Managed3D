using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlimDX;

namespace Managed3D.Rendering.Direct3D
{
    public class D3DCameraControl
    {
        #region Fields
        Vector3 location;
        Vector3 target;
        Vector3 up = Vector3.UnitY;
        float fov;
        float aspect;
        float near;
        float far;

        Matrix view;
        Matrix proj;

        bool dirty = true;
        bool projectionDirty = true;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="D3DCameraControl"/> class.
        /// </summary>
        /// <param name="fov"></param>
        /// <param name="aspect"></param>
        /// <param name="near"></param>
        /// <param name="far"></param>
        public D3DCameraControl(float fov, float aspect, float near, float far)
        {
            this.fov = fov;
            this.aspect = aspect;
            this.near = near;
            this.far = far;
        }

        /// <summary>
        /// Gets or sets the location of the camera's eye point.
        /// </summary>
        public Vector3 Location
        {
            get
            {
                return this.location;
            }
            set
            {
                if (this.location == value)
                    return;

                this.location = value;
                this.dirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the location of the object or point that the camera is 'looking at'
        /// </summary>
        public Vector3 Target
        {
            get
            {
                return this.target;
            }
            set
            {
                if (this.target == value)
                    return;

                this.target = value;
                this.dirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the vector that describes 'up' relative to the camera.
        /// </summary>
        public Vector3 Up
        {
            get
            {
                return this.up;
            }
            set
            {
                if (this.up == value)
                    return;

                this.up = value;
                this.dirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the field of view angle of the camera (in radians).
        /// </summary>
        public float FieldOfView
        {
            get
            {
                return this.fov;
            }
            set
            {
                if (this.fov == value)
                    return;

                this.fov = value;
                this.projectionDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the aspect ratio of the viewport.
        /// </summary>
        public float AspectRatio
        {
            get
            {
                return this.aspect;
            }
            set
            {
                if (this.aspect == value)
                    return;

                this.aspect = value;
                this.projectionDirty = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float NearPlane
        {
            get
            {
                return this.near;
            }
            set
            {
                if (this.near == value)
                    return;

                this.near = value;
                this.projectionDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the far clipping plane.
        /// </summary>
        public float FarPlane
        {
            get
            {
                return this.far;
            }
            set
            {
                if (this.far == value)
                    return;

                this.far = value;
                this.projectionDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the view matrix.
        /// </summary>
        public Matrix ViewMatrix
        {
            get
            {
                if (this.dirty)
                    this.RebuildViewMatrix();

                return this.view;
            }
            protected set
            {

            }
        }

        /// <summary>
        /// Gets or sets the matrix used for projection.
        /// </summary>
        public Matrix ProjectionMatrix
        {
            get
            {
                if (this.projectionDirty)
                    this.RebuildProjectionMatrix();

                return this.view;
            }
        }

        protected virtual void RebuildViewMatrix()
        {
            this.view = Matrix.LookAtLH(this.location, this.target, up);
            this.dirty = false;
        }

        protected virtual void RebuildProjectionMatrix()
        {
            this.proj = Matrix.PerspectiveFovLH(this.fov, this.aspect, this.near, this.far);
            this.projectionDirty = false;
        }
    }
}
