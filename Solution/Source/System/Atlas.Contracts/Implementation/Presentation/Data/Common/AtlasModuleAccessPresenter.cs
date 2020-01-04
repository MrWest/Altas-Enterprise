using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class AtlasModuleAccessPresenter: AtlasGenericModuleAccessPresenter<IAtlasModuleAccess, IAtlasModuleAccessManagerApplicationServices>, IAtlasModuleAccessPresenter,IView
    {
       
       
        public IAtlasGenericModuleAccessPresenter OwnerModuleAccess { get; set; }

        public override IEnumerable<IPresenter> Collection
        {
            get { return OwnerModuleAccess.Collection; }
           
        }

        //public override int Depth
        //{
        //    get
        //    {
        //        return OwnerModuleAccess.Depth + 1;
        //    }
        //}
        public override INavigable Parent
        {
            get
            {
                return OwnerModuleAccess;
            }
        }
        public override  ICommand DeleteMySelfCommand { get { return OwnerModuleAccess.OwnedAccesses.DeleteCommand; } }

        protected override IAtlasModuleAccessManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.OwnerModuleAccess = (OwnerModuleAccess as IPresenter)?.Object as IAtlasGenericModuleAccess;
            return service;
        }
       
    }
}
