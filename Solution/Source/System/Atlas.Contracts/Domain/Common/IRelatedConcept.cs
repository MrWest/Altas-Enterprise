using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    public interface IRelatedConcept : IEntity
    {
        ISubjectConcept OwnerSubjectConcept { get; set; }
        string OwnerSubjectConceptId { get; set; }

        string SubjectConcept { get; set; }
    }
}
