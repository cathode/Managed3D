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
            this.ChunkSize = 4;
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
            var r = new Random(this.seed.GetHashCode() | position.X | position.Y);

            var c1 = r.NextDouble();
            var c1x = r.NextDouble();
            var c1y = r.NextDouble();
            var c2 = r.NextDouble();
            var c2x = r.NextDouble();
            var c2y = r.NextDouble();
            var c3 = r.NextDouble();
            var c3x = r.NextDouble();
            var c3y = r.NextDouble();
            var c4 = r.NextDouble();
            var c4x = r.NextDouble();
            var c4y = r.NextDouble();

            double n = this.ChunkSize;

            for (int y = 0; y < stride + 1; ++y)
            {
                for (int x = 0; x < stride + 1; ++x)
                {
                    var z = (c1 / n) * (x + 1) * ((c1x + c2x) / 2.0) + (c2 / n) * (x + 1);
                    z += (c1 / n) * (y + 1) * ((c1y + c2y) / 2.0) + (c2 / n) * (y + 1);
                    z *= 2;
                    verts[(y * sv) + x] = new Vertex3((x + (position.X * this.ChunkSize)) * 4, z, (y + (position.Y * this.ChunkSize)) * 4);
                }
            }

            for (int y = 0; y < stride; ++y)
                for (int x = 0; x < stride; ++x)
                    quads[(y * stride) + x] = new Quad3(verts, (y * sv) + x, (y * sv) + (x + 1), ((y + 1) * sv) + (x + 1), ((y + 1) * sv) + x);

            return new Mesh3(quads);
        }
    }
}
