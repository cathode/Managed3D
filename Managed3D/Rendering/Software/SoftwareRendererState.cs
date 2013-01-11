using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Managed3D.Geometry;
using System.Diagnostics.Contracts;
using Managed3D.SceneGraph;

namespace Managed3D.Rendering.Software
{
    /// <summary>
    /// Represents state information used for rendering a frame.
    /// </summary>
    public sealed class SoftwareRendererState
    {
        #region Fields
        internal static SoftwareRendererState GlobalState;
        private readonly SoftwareRenderer renderer;
        private Stack<Matrix4> worldMatrices = new Stack<Matrix4>();
        private Stack<Matrix4> viewMatrices = new Stack<Matrix4>();
        private Stack<Matrix4> projectionMatrices = new Stack<Matrix4>();
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SoftwareRendererState"/> class.
        /// </summary>
        /// <param name="renderer">The <see cref="SoftwareRenderer"/> that this instance represents state for.</param>
        public SoftwareRendererState(SoftwareRenderer renderer)
        {
            this.renderer = renderer;

            this.WorldMatrix = Matrix4.Identity;
            this.ViewMatrix = Matrix4.Identity;
            this.ProjectionMatrix = Matrix4.Identity;

            this.UpdateDerivedMatrices();
        }
        #endregion
        #region Properties
        public Matrix4 WorldMatrix
        {
            get;
            private set;
        }

        public Matrix4 ViewMatrix
        {
            get;
            private set;
        }

        public Matrix4 WorldViewProjection
        {
            get;
            private set;
        }

        public Matrix4 ViewProjection
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets a <see cref="Matrix4"/> that is a projection transformation matrix.
        /// </summary>
        public Matrix4 ProjectionMatrix
        {
            get;
            private set;
        }
        #endregion
        #region Methods
        public void PushMatrix()
        {
            this.worldMatrices.Push(this.WorldMatrix);
            this.WorldMatrix *= Matrix4.Identity; // New instance
            this.viewMatrices.Push(this.ViewMatrix);
            this.ViewMatrix *= Matrix4.Identity;
            this.projectionMatrices.Push(this.ProjectionMatrix);
            this.ProjectionMatrix *= Matrix4.Identity;

            this.UpdateDerivedMatrices();
        }

        public void PopMatrix()
        {
            if (this.worldMatrices.Count > 0)
            {
                this.WorldMatrix = this.worldMatrices.Pop();
                this.ViewMatrix = this.viewMatrices.Pop();
                this.ProjectionMatrix = this.projectionMatrices.Pop();
            }

            this.UpdateDerivedMatrices();
        }

        public Vertex3 Transform(Vertex3 input)
        {
            Contract.Requires(input != null);

            return this.WorldViewProjection * input;
        }

        public Vertex3 Transform(Vertex3 input, ReferenceSpace space)
        {
            Contract.Requires(input != null);

            // Allow selective transformation based on the scope of the vertex input.
            if (space == ReferenceSpace.Object)
                return this.WorldViewProjection * input;
            else if (space == ReferenceSpace.World)
                return this.ViewProjection * input;
            else if (space == ReferenceSpace.View)
                return this.ProjectionMatrix * input;
            else
                return input;
        }
        public Vector3 Transform(Vector3 input)
        {
            return this.WorldViewProjection * input;
        }

        public void Translate(Vector3 translation)
        {
            this.WorldMatrix *= Matrix4.CreateTranslationMatrix(translation);

            this.UpdateDerivedMatrices();
        }

        public void Translate(double x, double y, double z)
        {
            this.Translate(new Vector3(x, y, z));
        }

        public void Scale(Vector3 scaling)
        {
            this.WorldMatrix *= Matrix4.CreateScalingMatrix(scaling);

            this.UpdateDerivedMatrices();
        }

        public void Scale(double x, double y, double z)
        {
            this.Scale(new Vector3(x, y, z));
        }

        public void Rotate(Quaternion rotation)
        {
            this.WorldMatrix *= Matrix4.CreateRotationMatrix(rotation);

            this.UpdateDerivedMatrices();
        }

        /// <summary>
        /// Sets the projection matrix for the render state.
        /// </summary>
        /// <param name="projectionMatrix"></param>
        public void SetProjectionMatrix(Matrix4 projectionMatrix)
        {
            this.ProjectionMatrix = projectionMatrix;

            this.UpdateDerivedMatrices();
        }

        private void UpdateDerivedMatrices()
        {
            this.ViewProjection = this.ProjectionMatrix * Matrix4.Identity;
            this.ViewProjection *= this.ViewMatrix;
            this.WorldViewProjection = this.ProjectionMatrix;
            this.WorldViewProjection *= this.ViewMatrix;
            this.WorldViewProjection *= this.WorldMatrix;

        }
        #endregion
    }
}
