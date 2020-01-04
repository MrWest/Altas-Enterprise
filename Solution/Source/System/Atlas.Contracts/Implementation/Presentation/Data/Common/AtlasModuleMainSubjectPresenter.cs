using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class AtlasModuleMainSubjectPresenter : AtlasModuleGenericSubjectPresenter<IAtlasModuleMainSubject, IAtlasModuleMainSubjectManagerApplicationServices>, IAtlasModuleMainSubjectPresenter
    {
        public String AssemblyName
        {

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
        public override ICommand DeleteMySelfCommand
        {
            get
            {
                
                var viewmodel = ServiceLocator.Current.GetInstance<IAtlasModuleMainSubjectViewModel>();
                ////viewmodel.Raised += OnInteractionRequested;
                viewmodel.AssemblyName = AssemblyName;
                return viewmodel.DeleteCommand;
            }
        }

        protected override IAtlasModuleMainSubjectManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.AssemblyName = AssemblyName;
            return service;
        }
    }
}
