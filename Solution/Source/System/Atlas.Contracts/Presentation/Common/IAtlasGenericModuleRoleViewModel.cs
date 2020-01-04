using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface IAtlasGenericModuleRoleViewModel<TEntity>:ICrudViewModel<IAtlasModuleRole, IAtlasModuleRolePresenter<TEntity>>
          where TEntity : class, IAtlasModuleAccess
       
    {
        IAtlasCommonModuleAccessPresenter<TEntity> OwnerModuleAccess { get; set; }
    }
}
