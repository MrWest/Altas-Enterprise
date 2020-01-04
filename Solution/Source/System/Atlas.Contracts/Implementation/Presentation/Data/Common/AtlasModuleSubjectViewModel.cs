using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class AtlasModuleSubjectViewModel : AtlasModuleGenericSubjectViewModel<IAtlasModuleSubject, IAtlasModuleSubjectPresenter, IAtlasModuleSubjectManagerApplicationServices>, IAtlasModuleSubjectViewModel
    {
        public IAtlasModuleGenericSubjectPresenter OwnerSubject { get; set; }

        protected override IAtlasModuleSubjectPresenter CreatePresenterFor(IAtlasModuleSubject item)
        {
            var presenter = base.CreatePresenterFor(item);
            presenter.OwnerSubject = OwnerSubject;
            return presenter;
        }

        protected override IAtlasModuleSubjectManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.OwnerSubject = OwnerSubject.ModuleSubject;
            return service;
        }


        protected override INavigable Parent { get { return OwnerSubject; } }
    }
}
