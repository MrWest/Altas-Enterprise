using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class RelatedConceptViewModel:CrudViewModelBase<IRelatedConcept, IRelatedConceptPresenter, IRelatedConceptManagerApplicationServices>, IRelatedConceptViewModel
    {
        public ISubjectConceptPresenter OwnerSubjectConcept { get; set; }
        protected override IRelatedConceptManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.OwnerSubjectConcept = OwnerSubjectConcept.Object;
            return service;
        }

        protected override IRelatedConceptPresenter CreatePresenterFor(IRelatedConcept item)
        {
            var presenter = base.CreatePresenterFor(item);
            presenter.OwnerSubjectConcept = OwnerSubjectConcept;
            return presenter;
        }
    }
}