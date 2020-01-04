using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Modularity;

namespace CompanyName.Atlas.Contracts.Domain.Common
{

    public interface IAtlasMainModuleAccess : IAtlasGenericModuleAccess
    {
        String AssemblyName { get; set; }


    }
}
