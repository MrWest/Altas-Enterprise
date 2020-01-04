using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class AtlasModuleSubjectPresenter : AtlasModuleGenericSubjectPresenter<IAtlasModuleSubject, IAtlasModuleSubjectManagerApplicationServices>, IAtlasModuleSubjectPresenter
    {
        public IAtlasModuleGenericSubjectPresenter OwnerSubject { get; set; }
        public override ICommand DeleteMySelfCommand
        {
            get
            {

                return OwnerSubject.Subjects.DeleteCommand;
            }
        }

        public override INavigable Parent
        {
            get { return OwnerSubject; }
        }

        protected override IAtlasModuleSubjectManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.OwnerSubject = OwnerSubject.ModuleSubject;
            return service;
        }
    }
}
