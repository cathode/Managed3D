using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Managed3D.Geometry;
using System.Drawing.Imaging;
using System.Drawing;

namespace Managed3D.SceneGraph
{
    /// <summary>
    /// Represents a node that contains a text label which is rendered directly on the framebuffer.
    /// </summary>
    public class LabelNode : Node
    {
        #region Fields
        private string text;
        private Vector4f[] bitmap;
        private Vector2i bitmapSize;
        #endregion
        #region Constructors
        public LabelNode(string text)
        {
            this.FontSize = 18;
            this.Text = text;

        }
        #endregion
        #region Properties
        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                this.RegenerateBitmap();
            }
        }

        public double FontSize
        {
            get;
            set;
        }

        public Vector4f[] Bitmap
        {
            get
            {
                return this.bitmap;
            }
        }

        public Vector2i BitmapSize
        {
            get
            {
                return this.bitmapSize;
            }
        }

        public Vector4f TextColor
        {
            get;
            set;
        }
        #endregion
        #region Methods
        private unsafe void RegenerateBitmap()
        {
            var font = new Font("Consolas", (float)this.FontSize);
            var h = font.GetHeight(128f);
            var maxSize = this.text.Length * this.FontSize;

            var bmp = new Bitmap((int)maxSize, (int)h);
            var g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawString(this.text, font, new SolidBrush(Color.FromArgb(255, (byte)(this.TextColor.X * 255), (byte)(this.TextColor.Y * 255), (byte)(this.TextColor.Z * 255))), 0, 0);

            var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            this.bitmapSize = new Vector2i(bmpData.Width, bmpData.Height);
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
            this.bitmap = vb;
        }
        #endregion
    }
}
