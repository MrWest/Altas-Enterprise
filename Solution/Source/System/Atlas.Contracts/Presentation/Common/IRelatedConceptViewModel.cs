using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface IRelatedConceptViewModel:ICrudViewModel<IRelatedConcept, IRelatedConceptPresenter>
    {
        ISubjectConceptPresenter OwnerSubjectConcept { get; set; }
    }
}