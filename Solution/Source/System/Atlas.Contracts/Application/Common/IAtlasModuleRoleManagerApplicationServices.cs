using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Application.Common
{
  public  interface IAtlasModuleRoleManagerApplicationServices: IItemManagerApplicationServices<IAtlasModuleRole>
    {
        IAtlasGenericModuleAccess ModuleAccess { get; set; }
    }
}
