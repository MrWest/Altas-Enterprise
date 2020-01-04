using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Modularity;

namespace CompanyName.Atlas.Contracts.Domain.Common
{

    public interface IAtlasModuleAccess: IAtlasGenericModuleAccess
    {

        IAtlasGenericModuleAccess OwnerModuleAccess { get; set; }
      
    }
}
