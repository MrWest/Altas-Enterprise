using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Application.Common
{
    public class RelatedConceptManagerApplicationServices:ItemManagerApplicationServicesBase<IRelatedConcept, IRelatedConceptRepository, IRelatedConceptDomainServices>, IRelatedConceptManagerApplicationServices
    {
        public ISubjectConcept OwnerSubjectConcept { get; set; }

        protected override IRelatedConceptRepository Repository
        {
            get
            {
                var repo = base.Repository;
                repo.OwnerSubjectConcept = OwnerSubjectConcept;
                return repo;
            } 
        }

        protected override IRelatedConceptDomainServices DomainServices
        {
            get
            {
                var domain = base.DomainServices;
                domain.OwnerSubjectConcept = OwnerSubjectConcept;
                return domain;
            }
        }
    }
}