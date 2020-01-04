using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
    public class RelatedConcept: EntityBase, IRelatedConcept
    {
        public ISubjectConcept OwnerSubjectConcept { get; set; }

        public string SubjectConcept { get; set; }
        public string OwnerSubjectConceptId { get; set; }
    }
}
