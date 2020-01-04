using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
   public interface IAtlasGenericModuleAccessViewModel<TEntity> : ICrudViewModel<IAtlasModuleAccess, IAtlasGenericModuleAccessPresenter<TEntity>>
       where TEntity :class, IAtlasModuleAccess
         
    {
        IAtlasCommonModuleAccessPresenter<TEntity> OwnerModuleAccess { get; set; }
      

    }
}
