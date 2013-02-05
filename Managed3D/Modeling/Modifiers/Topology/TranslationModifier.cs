using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using Managed3D.Geometry;

namespace Managed3D.Modeling.Modifiers.Topology
{
    public class TranslationModifier : TopologyModifier
    {
        #region Fields

        #endregion
        #region Constructors

        #endregion
        #region Properties
        public Vector3 Translation { get; set; }
        #endregion
        #region Methods
        public override void Apply(Model model)
        {
            foreach (var v in model.Selection.GetItems<IVertex>(SelectionTarget.Vertex))
            {
                v.Position += this.Translation;
            }
        }
        #endregion
    }
}
