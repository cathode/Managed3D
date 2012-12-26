using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using Managed3D.Geometry;

namespace Managed3D.SceneGraph
{
    /// <summary>
    /// Represents a scene graph node that contains sprite data.
    /// </summary>
    public class SpriteNode : Node
    {
        #region Constructors
        public SpriteNode()
        {
            this.OrientationSpace = ReferenceSpace.View;
        }
        #endregion
        #region Properties
        public Vector4f[] Bitmap
        {
            get;
            protected set;
        }

        public Vector2i BitmapSize
        {
            get;
            protected set;
        }
        #endregion
        #region Methods

        public unsafe void LoadBitmap(System.Drawing.Bitmap bitmap)
        {
            var bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            this.BitmapSize = new Vector2i(bmpData.Width, bmpData.Height);
            var vb = new Vector4f[bmpData.Width * bmpData.Height];

            // Copy the temporary bitmap to our internal Vector4f buffer (which is easier to blit to the framebuffer during rendering.
            var s0 = bmpData.Scan0.ToPointer();
            var b0 = (byte*)s0;
            for (int y = 0, n = -1, i = -1; y < bmpData.Height; ++y)
            {
                for (int x = 0; x < bmpData.Width; ++x)
                {
                    var c = new Vector4f((byte)(b0[++i]) / 255.0f, (byte)(b0[++i]) / 255.0f, (byte)(b0[++i]) / 255.0f, (byte)(b0[++i]) / 255f);
                    vb[++n] = c;
                }
            }
            this.Bitmap = vb;
            bitmap.UnlockBits(bmpData);
        }
        #endregion
    }
}
