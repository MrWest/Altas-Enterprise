using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class AtlasMainModuleAccessPresenter : AtlasGenericModuleAccessPresenter<IAtlasMainModuleAccess, IAtlasMainModuleAccessManagerApplicationServices>, IAtlasMainModuleAccessPresenter
    {
       // private IAtlasGenericModuleAccessViewModel<IAtlasMainModuleAccess> _ownedAccesses;


        public String AssemblyName {

            get
            {
                return Object.AssemblyName;
            }
            set
            {
                SetProperty(v => Object.AssemblyName = v, value);
                OnPropertyChanged(() => AssemblyName);
            }
        }

       

        public override ICommand DeleteMySelfCommand {
                get {
                    var viewmodel = ServiceLocator.Current.GetInstance<IAtlasMainModuleAccessViewModel>();
               // viewmodel.Raised -= OnInteractionRequested;
                viewmodel.Raised += OnInteractionRequested;
                    viewmodel.AssemblyName = AssemblyName;
                    return  viewmodel.DeleteCommand;
                }
        }

        protected override IAtlasMainModuleAccessManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.AssemblyName = AssemblyName;
            return service;
        }
    }
}
