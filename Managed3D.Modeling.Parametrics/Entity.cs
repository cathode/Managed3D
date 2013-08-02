using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Managed3D.Modeling.Parametrics
{
    /// <summary>
    /// Represents an element of a parametric model.
    /// </summary>
    public class Entity
    {

        private List<Entity> children;

        /// <summary>
        /// Gets or sets a parent entity of the current entity.
        /// </summary>
        public Entity Parent { get; set; }


        public long Id { get; set; }


        public int GetLevel()
        {
            if (this.Parent != null)
                return this.Parent.GetLevel() + 1;

            return 0;
        }

        public bool AddChild(Entity value)
        {
            //sif (value.GetLevel() < this.GetLevel())

            throw new NotImplementedException();
        }
    }
}
