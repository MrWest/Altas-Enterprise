﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
   public interface IAtlasModuleMainSubject : IAtlasModuleGenericSubject
    {
        string AssemblyName { get; set; }
    }
}
