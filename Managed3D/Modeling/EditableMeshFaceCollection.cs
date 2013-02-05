/******************************************************************************
 * Managed3D: A 3D Graphics API for .NET and Mono - http://gearedstudios.com/ *
 * Copyright © 2009-2012 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT/X11    *
 * license. See the 'license.txt' file for details.                           *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Managed3D.Modeling
{
    public sealed class EditableMeshFaceCollection : KeyedCollection<int, EditableMeshFace>
    {
        protected override int GetKeyForItem(EditableMeshFace item)
        {
            return item.Id;
        }
    }
}
