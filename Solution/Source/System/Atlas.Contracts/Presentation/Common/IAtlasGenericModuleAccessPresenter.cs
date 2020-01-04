using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface IAtlasGenericModuleAccessPresenter<TEntity> :IPresenter<TEntity>, IAtlasGenericModuleAccessPresenter
        where TEntity : class, IAtlasGenericModuleAccess

    {
        //IAtlasCommonModuleAccessPresenter<TOwner> OwnerModuleAccess { get; set; }
        //IEnumerable<IPresenter> Collection { get; }
    }

    public interface IAtlasGenericModuleAccessPresenter : INavigable
    {
        //IAtlasCommonModuleAccessPresenter<TOwner> OwnerModuleAccess { get; set; }
        IEnumerable<IPresenter> Collection { get; set; }
        IAtlasModuleAccessViewModel OwnedAccesses { get; }
        IAtlasModuleRoleViewModel Rols { get; }
        ICommand UpdateUser { get; }
    }
}
