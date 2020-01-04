using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Application.Common
{
    public interface IRelatedConceptManagerApplicationServices:IItemManagerApplicationServices<IRelatedConcept>
    {
        ISubjectConcept OwnerSubjectConcept { get; set; }
    }
}