using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
    public class RelatedConceptDomainServices:DomainServicesBase<IRelatedConcept>, IRelatedConceptDomainServices
    {
        public ISubjectConcept OwnerSubjectConcept { get; set; }

        public override IRelatedConcept Create()
        {
            var relConcept = base.Create();
            relConcept.OwnerSubjectConcept = OwnerSubjectConcept;
            return relConcept;
        }
    }
}