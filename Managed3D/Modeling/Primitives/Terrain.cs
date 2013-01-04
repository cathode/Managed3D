using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Managed3D.Geometry;

namespace Managed3D.Modeling.Primitives
{
    public class Terrain
    {
        #region Fields
        private Random rng;
        private long seed;
        private Vector2i size;
        #endregion
        public Terrain(int width, int length)
        {
            this.Roughness = 1.0;
            this.FeatureSize = 1.0;
            this.ChunkSize = 16;
            this.Seed = 7924213;

            this.size = new Vector2i(width, length);
        }

        public double Roughness
        {
            get;
            set;
        }

        public double FeatureSize
        {
            get;
            set;
        }

        public int ChunkSize
        {
            get;
            set;
        }

        public long Seed
        {
            get
            {
                return this.seed;
            }
            set
            {
                this.seed = value;
                this.rng = new Random(this.seed.GetHashCode());
            }
        }

        public IEnumerable<Mesh3> Generate()
        {
            for (int y = 0; y < this.size.Y; ++y)
                for (int x = 0; x < this.size.X; ++x)
                {
                    yield return this.GenerateChunk(new Vector2i(x, y));
                }
        }

        protected Mesh3 GenerateChunk(Vector2i position)
        {
            var mesh = new Mesh3();
            var verts = new Vertex3[(this.ChunkSize + 1) * (this.ChunkSize + 1)];
            var quads = new Quad3[this.ChunkSize * this.ChunkSize];

            var stride = this.ChunkSize;
            var sv = stride + 1;
            var r = this.rng;

            var c0 = r.NextDouble() * (4.0 / this.Roughness);
            var c1 = r.NextDouble() * (4.0 / this.Roughness);
            var c2 = r.NextDouble() * (4.0 / this.Roughness);
            var c3 = r.NextDouble() * (4.0 / this.Roughness);

            double n = sv;

            for (int y = 0; y < stride + 1; ++y)
            {
                for (int x = 0; x < stride + 1; ++x)
                {
                    var fx = x / n;
                    // Interpolate in the X direction
                    var xi = ((c0 * fx) + (c3 * (1.0 - fx))) + ((c1 * fx) + (c2 * (1.0 - fx)));

                    // Interpolate in the Y direction
                    var zi = ((c0 * fx) + (c1 * (1.0 - fx))) + ((c3 * fx) + (c2 * (1.0 - fx)));

                    //var z = (c1 / n) * (x + 1) * ((c1x + c2x) / 2.0) + (c2 / n) * (x + 1);
                    //z += (c1 / n) * (y + 1) * ((c1y + c2y) / 2.0) + (c2 / n) * (y + 1);
                    var z = (xi + zi) / 2.0;
                    verts[(y * sv) + x] = new Vertex3((x + (position.X * this.ChunkSize)) * 4, z * 12, (y + (position.Y * this.ChunkSize)) * 4);
                }
            }

            for (int y = 0; y < stride; ++y)
                for (int x = 0; x < stride; ++x)
                    quads[(y * stride) + x] = new Quad3(verts, (y * sv) + x, (y * sv) + (x + 1), ((y + 1) * sv) + (x + 1), ((y + 1) * sv) + x);

            return new Mesh3(quads);
        }

        private double Noise(Vector3i position)
        {
            return 0.0;
        }
    }
}
