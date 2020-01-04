using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class AtlasModuleMainSubjectViewModel : AtlasModuleGenericSubjectViewModel<IAtlasModuleMainSubject, IAtlasModuleMainSubjectPresenter, IAtlasModuleMainSubjectManagerApplicationServices>, IAtlasModuleMainSubjectViewModel
    {
        public string AssemblyName { get; set; }

        public override bool CanDelete(IAtlasModuleMainSubjectPresenter presenter)
        {
            return true;
        }

        protected override IAtlasModuleMainSubjectPresenter CreatePresenterFor(IAtlasModuleMainSubject item)
        {
            var presenter = base.CreatePresenterFor(item);
            presenter.AssemblyName = AssemblyName;
            return presenter;
        }

        protected override IAtlasModuleMainSubjectManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.AssemblyName = AssemblyName;
            return service;
        }
    }
}
