using System;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface IRelatedConceptPresenter:IPresenter<IRelatedConcept>
    {
        ISubjectConceptPresenter OwnerSubjectConcept { get; set; }
        string SubjectConcept { get; set; }
        ISubjectConceptPresenter SubjectConceptPresenter { get; }
    }
}