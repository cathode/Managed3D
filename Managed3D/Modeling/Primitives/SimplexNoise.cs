using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Managed3D.Geometry;

namespace Managed3D.Modeling.Primitives
{
    internal static class __Extensions
    {
        internal static int Floor(this double value)
        {
            return (value > 0) ? (int)value : (int)(value - 1);
        }
    }

    public class SimplexNoise
    {
        #region Fields
        private byte[] perm;
        #endregion
        #region Constructors
        public SimplexNoise(object seed)
        {
            this.Seed = (seed ?? new Object()).GetHashCode();
        }
        #endregion
        #region Properties
        public int Seed
        {
            get;
            set;
        }
        #endregion
        #region Methods
        public void Initialize()
        {
            var p = new byte[255];
            for (byte i = 255; i != 0; ++i)
                p[i] = i;

            var rng = new Random(this.Seed);
            int n = 256;

            while (n-- > 1)
            {
                var k = rng.Next(n - 1);
                var v = p[k];
                p[k] = p[n];
                p[n] = v;
            }
            this.perm = p;
        }

        public IEnumerable<Vector4> Noise3(Vector3 p, Vector3 q)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
