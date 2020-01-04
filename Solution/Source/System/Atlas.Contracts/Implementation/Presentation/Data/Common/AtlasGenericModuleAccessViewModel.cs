using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public abstract  class AtlasGenericModuleAccessViewModel<TEntity>: CrudViewModelBase<IAtlasModuleAccess, IAtlasGenericModuleAccessPresenter<TEntity>, IAtlasModuleAccessManagerApplicationServices>, IAtlasGenericModuleAccessViewModel<TEntity>
          where  TEntity:class, IAtlasModuleAccess        

    {
        public IAtlasCommonModuleAccessPresenter<TEntity> OwnerModuleAccess { get; set; }
       

        protected override void OnAddedItem(object sender, EventArgs e)
        {
            base.OnAddedItem(sender, e);
            (OwnerModuleAccess as INavigable).IsExpanded = true;
        }

       
        protected override IAtlasModuleAccessManagerApplicationServices CreateServices()
        {
            var service =  base.CreateServices();
            service.OwnerModuleAccess = OwnerModuleAccess.Object;
            return service;
        }
        public override bool CanDelete(IAtlasGenericModuleAccessPresenter<TEntity> presenter)
        {
            return true;
        }
    }
}
