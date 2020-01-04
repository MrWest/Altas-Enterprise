using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
   public interface IAtlasCommonModuleAccessPresenter<TEntity> :  IPresenter<TEntity>,INavigable
          where TEntity : class, IAtlasModuleAccess
       
    {
        IAtlasModuleRoleViewModel<TEntity> Rols { get; }
        IAtlasGenericModuleAccessViewModel<TEntity> OwnedAccesses { get; }
        IEnumerable<IPresenter> Collection { get; set; }
    }
}
