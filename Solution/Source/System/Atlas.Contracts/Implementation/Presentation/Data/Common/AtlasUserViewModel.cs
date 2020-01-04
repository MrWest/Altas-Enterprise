using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.Prism.Mvvm;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    /// <summary>
    /// crud operation for a <see cref="IAtlasUser"/>
    /// </summary>
    public class AtlasUserViewModel : CrudViewModelBase<IAtlasUser, IAtlasUserPresenter, IAtlasUserMangerApplicationService>, IAtlasUserViewModel, IView
    {
        public object DataContext { get; set; }
    }


    /// <summary>
    /// crud operation for a <see cref="IAtlasUser"/>
    /// </summary>
    public class AtlasModuleInfoViewModel : CrudViewModelBase<IAtlasModuleInfo, IAtlasModuleInfoPresenter, IAtlasModuleInfoMangerApplicationService>, IAtlasModuleInfoViewModel
    {
        public IAtlasUserPresenter AtlasUser { get; set; }

        protected override IAtlasModuleInfoMangerApplicationService CreateServices()
        {
            var service = base.CreateServices();
            service.AtlasUser = AtlasUser.Object;
            return service;
        }

        protected override IAtlasModuleInfoPresenter CreatePresenterFor(IAtlasModuleInfo item)
        {
            var presenter = base.CreatePresenterFor(item);
            presenter.AtlasUser = AtlasUser;
            return presenter;
        }

        public void AddFromScratch(IAtlasModuleInfo moduleInfo)
        {
            var presenter = CreatePresenterFor(moduleInfo);

            Add(presenter);
        }
    }
}
