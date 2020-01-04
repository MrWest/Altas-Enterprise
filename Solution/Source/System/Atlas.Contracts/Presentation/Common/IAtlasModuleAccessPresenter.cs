using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
 
    public interface IAtlasModuleAccessPresenter : IAtlasGenericModuleAccessPresenter<IAtlasModuleAccess>
    {
        IAtlasGenericModuleAccessPresenter OwnerModuleAccess { get; set; }

    }
}
