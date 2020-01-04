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
    public class SubjectConceptViewModel:CrudViewModelBase<ISubjectConcept, ISubjectConceptPresenter, ISubjectConceptManagerApplicationServices>, ISubjectConceptViewModel
    {
        public IAtlasModuleGenericSubjectPresenter ModuleSubject { get; set; }

        protected override ISubjectConceptPresenter CreatePresenterFor(ISubjectConcept item)
        {
            var presenter = base.CreatePresenterFor(item);
            presenter.ModuleSubject = ModuleSubject;
            return presenter;
        }

        protected override ISubjectConceptManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.ModuleSubject = (ModuleSubject as IPresenter)?.Object as IAtlasModuleGenericSubject;
            return service;
        }
    }
}
