using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Domain
{
    public interface IRelatedConceptDomainServices:IDomainServices<IRelatedConcept>
    {
        ISubjectConcept OwnerSubjectConcept { get; set; }
    }
}