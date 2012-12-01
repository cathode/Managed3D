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
using System.Diagnostics.Contracts;

namespace Managed3D.Modeling
{
    public class ProcedureParameterCollection : KeyedCollection<string, ProcedureParameter>
    {
        protected override string GetKeyForItem(ProcedureParameter item)
        {
            return item.Name;
        }
    }
}
